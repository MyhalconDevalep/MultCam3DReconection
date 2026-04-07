using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class MultiCameraPairingManager
    {
        private readonly AutoResetEvent[] _cameraEvents;
        private readonly int _cameraCount;
        private readonly int _timeoutMs;
        private readonly ManualResetEventSlim _pairReadyEvent;
        private volatile bool _running = true;

        public ManualResetEventSlim PairReadyHandle => _pairReadyEvent;

        public MultiCameraPairingManager(int cameraCount, int timeoutMs)
        {
            if (cameraCount < 2)
                throw new ArgumentException("Camera count must be >= 2");

            _cameraCount = cameraCount;
            _timeoutMs = timeoutMs;

            _cameraEvents = new AutoResetEvent[cameraCount];
            for (int i = 0; i < cameraCount; i++)
                _cameraEvents[i] = new AutoResetEvent(false);

            _pairReadyEvent = new ManualResetEventSlim(false);
        }

        /// <summary>
        /// 相机采集完成后调用（传入相机索引）
        /// </summary>
        public void SignalCamera(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= _cameraCount)
                    throw new ArgumentOutOfRangeException(nameof(cameraIndex));

            _cameraEvents[cameraIndex].Set();
        }

        /// <summary>
        /// 启动配对线程
        /// </summary>
        public void Start()
        {
            Task.Factory.StartNew(PairingLoop,
                TaskCreationOptions.LongRunning);
        }

        public void Stop()
        {
            _running = false;
        }

        private void PairingLoop()
        {
            var handles = _cameraEvents.Cast<WaitHandle>().ToArray();

            while (_running)
            {
                // 1️⃣ 等待任意一个相机
                int firstIndex = WaitHandle.WaitAny(handles, Timeout.Infinite);
                if (firstIndex == WaitHandle.WaitTimeout)
                    continue;

                // 2️⃣ 等待其余相机
                var remaining = new List<WaitHandle>(handles);
                remaining.RemoveAt(firstIndex);

                bool allArrived = WaitHandle.WaitAll(
                    remaining.ToArray(),
                    _timeoutMs);

                if (!allArrived)
                {
                    LoggerHelper._.Warn(
                        $"Camera pairing timeout ({_cameraCount} cameras), discard this set");
                    continue;
                }

                // 3️⃣ 成功配对
                _pairReadyEvent.Set();
            }
        }
    }

}
