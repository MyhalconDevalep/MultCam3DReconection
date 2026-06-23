using HalconDotNet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class MultiCameraPairingManager : IDisposable
    {
        private readonly object _syncRoot = new object();
        private readonly int _cameraCount;
        private readonly int _timeoutMs;
        private readonly ManualResetEventSlim _pairReadyEvent = new ManualResetEventSlim(false);
        private readonly AutoResetEvent _readySignal = new AutoResetEvent(false);
        private readonly ConcurrentQueue<ReadyBatchInfo> _readyBatches = new ConcurrentQueue<ReadyBatchInfo>();
        private readonly Timer _timeoutTimer;

        private bool _running;
        private bool _hasActiveBatch;
        private long _currentBatchId;
        private DateTime _lastTimeoutUtc = DateTime.MinValue;
        private bool[] _arrived;
        private Dictionary<int, HObject> _batchImages;

        private long _readyBatchCount;
        private long _timeoutBatchCount;
        private long _lateFrameCount;
        private long _duplicateFrameCount;

        public ManualResetEventSlim PairReadyHandle => _pairReadyEvent;
        public long CurrentBatchId => Interlocked.Read(ref _currentBatchId);
        public long ReadyBatchCount => Interlocked.Read(ref _readyBatchCount);
        public long TimeoutBatchCount => Interlocked.Read(ref _timeoutBatchCount);
        public long LateFrameCount => Interlocked.Read(ref _lateFrameCount);
        public long DuplicateFrameCount => Interlocked.Read(ref _duplicateFrameCount);

        public MultiCameraPairingManager(int cameraCount, int timeoutMs)
        {
            if (cameraCount < 2)
                throw new ArgumentException("Camera count must be >= 2");

            if (timeoutMs <= 0)
                throw new ArgumentOutOfRangeException(nameof(timeoutMs));

            _cameraCount = cameraCount;
            _timeoutMs = timeoutMs;
            _timeoutTimer = new Timer(OnBatchTimeout, null, Timeout.Infinite, Timeout.Infinite);
            ResetBatchState();
        }

        public void Start()
        {
            lock (_syncRoot)
            {
                if (_running)
                    return;

                _running = true;
            }
        }

        public void Stop()
        {
            lock (_syncRoot)
            {
                if (!_running)
                    return;

                _running = false;
                _timeoutTimer.Change(Timeout.Infinite, Timeout.Infinite);
                DiscardActiveBatchLocked();
            }

            _pairReadyEvent.Set();
            _readySignal.Set();
        }

        public long BeginBatch()
        {
            lock (_syncRoot)
            {
                if (!_running)
                    _running = true;

                if (_hasActiveBatch)
                {
                    LoggerHelper._.Warn($"Batch {_currentBatchId} replaced by a new batch, discard unfinished images");
                    Interlocked.Increment(ref _timeoutBatchCount);
                    DiscardActiveBatchLocked();
                }

                StartNewBatchLocked();
                return _currentBatchId;
            }
        }

        public void SignalCamera(int cameraIndex)
        {
            SignalCamera(cameraIndex, null);
        }

        public void SignalCamera(int cameraIndex, HObject image)
        {
            long batchId;
            lock (_syncRoot)
            {
                if (!_running)
                    return;

                if (!_hasActiveBatch)
                {
                    if (IsDrainingLateFramesLocked())
                    {
                        Interlocked.Increment(ref _lateFrameCount);
                        LoggerHelper._.Warn($"Camera frame ignored during timeout drain, camera:{cameraIndex}, current:{_currentBatchId}");
                        return;
                    }

                    StartNewBatchLocked();
                }

                batchId = _currentBatchId;
            }

            SignalCamera(cameraIndex, batchId, image);
        }

        public void SignalCamera(int cameraIndex, long batchId, HObject image)
        {
            ValidateCameraIndex(cameraIndex);

            HObject clonedImage = null;
            if (image != null && image.IsInitialized())
                clonedImage = image.Clone();

            ReadyBatchInfo readyBatch = null;

            lock (_syncRoot)
            {
                if (!_running)
                {
                    clonedImage?.Dispose();
                    Interlocked.Increment(ref _lateFrameCount);
                    LoggerHelper._.Warn($"Camera frame ignored after pairing manager stopped, camera:{cameraIndex}, batch:{batchId}");
                    return;
                }

                if (!_hasActiveBatch || batchId != _currentBatchId)
                {
                    clonedImage?.Dispose();
                    Interlocked.Increment(ref _lateFrameCount);
                    LoggerHelper._.Warn($"Late camera frame ignored, camera:{cameraIndex}, batch:{batchId}, current:{_currentBatchId}");
                    return;
                }

                if (_arrived[cameraIndex])
                {
                    clonedImage?.Dispose();
                    Interlocked.Increment(ref _duplicateFrameCount);
                    LoggerHelper._.Warn($"Duplicate camera frame ignored, camera:{cameraIndex}, batch:{batchId}");
                    return;
                }

                _arrived[cameraIndex] = true;
                if (clonedImage != null)
                    _batchImages[cameraIndex] = clonedImage;

                if (AllArrivedLocked())
                    readyBatch = CompleteBatchLocked();
            }

            if (readyBatch != null)
                PublishReadyBatch(readyBatch);
        }

        public bool WaitForReadyBatch(out ReadyBatchInfo readyBatch)
        {
            return WaitForReadyBatch(out readyBatch, Timeout.Infinite);
        }

        public bool WaitForReadyBatch(out ReadyBatchInfo readyBatch, int timeoutMs)
        {
            readyBatch = null;

            while (true)
            {
                if (_readyBatches.TryDequeue(out readyBatch))
                {
                    if (_readyBatches.IsEmpty)
                        _pairReadyEvent.Reset();

                    return true;
                }

                if (!Volatile.Read(ref _running))
                    return false;

                if (!_readySignal.WaitOne(timeoutMs))
                    return false;
            }
        }

        private void OnBatchTimeout(object state)
        {
            long batchId;
            List<int> missing;

            lock (_syncRoot)
            {
                if (!_running || !_hasActiveBatch)
                    return;

                batchId = _currentBatchId;
                missing = GetMissingCamerasLocked();
                Interlocked.Increment(ref _timeoutBatchCount);
                DiscardActiveBatchLocked();
                _lastTimeoutUtc = DateTime.UtcNow;
            }

            LoggerHelper._.Warn($"Batch {batchId} timeout, missing cameras: {string.Join(",", missing)}");
        }

        private void StartNewBatchLocked()
        {
            ResetBatchState();
            _currentBatchId++;
            _hasActiveBatch = true;
            _lastTimeoutUtc = DateTime.MinValue;
            _timeoutTimer.Change(_timeoutMs, Timeout.Infinite);
        }

        private ReadyBatchInfo CompleteBatchLocked()
        {
            _timeoutTimer.Change(Timeout.Infinite, Timeout.Infinite);

            var images = _batchImages;
            var readyBatch = new ReadyBatchInfo(_currentBatchId, images.Keys.OrderBy(x => x).ToArray());

            CommitImages(images);
            _batchImages = new Dictionary<int, HObject>();
            _hasActiveBatch = false;
            _arrived = new bool[_cameraCount];
            Interlocked.Increment(ref _readyBatchCount);

            return readyBatch;
        }

        private void CommitImages(Dictionary<int, HObject> images)
        {
            if (GlobalStaticData._imageBuffer == null)
                GlobalStaticData._imageBuffer = new ConcurrentDictionary<int, HObject>();

            ClearCommittedImages();
            foreach (var kvp in images)
            {
                GlobalStaticData._imageBuffer[kvp.Key] = kvp.Value;
            }
        }

        public void ClearCommittedImages()
        {
            if (GlobalStaticData._imageBuffer == null)
                return;

            foreach (var image in GlobalStaticData._imageBuffer.Values)
                image?.Dispose();

            GlobalStaticData._imageBuffer.Clear();
        }

        private void PublishReadyBatch(ReadyBatchInfo readyBatch)
        {
            _readyBatches.Enqueue(readyBatch);
            _pairReadyEvent.Set();
            _readySignal.Set();
        }

        private void DiscardActiveBatchLocked()
        {
            _timeoutTimer.Change(Timeout.Infinite, Timeout.Infinite);
            foreach (var image in _batchImages.Values)
                image?.Dispose();

            ResetBatchState();
        }

        private void ResetBatchState()
        {
            _hasActiveBatch = false;
            _arrived = new bool[_cameraCount];
            _batchImages = new Dictionary<int, HObject>();
        }

        private bool AllArrivedLocked()
        {
            for (int i = 0; i < _cameraCount; i++)
            {
                if (!_arrived[i])
                    return false;
            }

            return true;
        }

        private bool IsDrainingLateFramesLocked()
        {
            return _lastTimeoutUtc != DateTime.MinValue &&
                (DateTime.UtcNow - _lastTimeoutUtc).TotalMilliseconds < _timeoutMs;
        }

        private List<int> GetMissingCamerasLocked()
        {
            var missing = new List<int>();
            for (int i = 0; i < _cameraCount; i++)
            {
                if (!_arrived[i])
                    missing.Add(i);
            }

            return missing;
        }

        private void ValidateCameraIndex(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= _cameraCount)
                throw new ArgumentOutOfRangeException(nameof(cameraIndex));
        }
        public void Dispose()
        {
            Stop();
            _timeoutTimer.Dispose();
            _readySignal.Dispose();
            _pairReadyEvent.Dispose();
        }
    }

    public class ReadyBatchInfo
    {
        public ReadyBatchInfo(long batchId, int[] cameraIndices)
        {
            BatchId = batchId;
            CameraIndices = cameraIndices ?? new int[0];
        }

        public long BatchId { get; }
        public int[] CameraIndices { get; }
    }
}
