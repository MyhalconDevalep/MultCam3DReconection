using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class PointProcessor2
    {

        private readonly ConcurrentBag<FindCoorData> _bag = new ConcurrentBag<FindCoorData>();
        // private readonly CancellationTokenSource _cts =new CancellationTokenSource();
        public Action<string> _eventMsg=null;
        public double _xyTolerance;
        public double _minXThreshold;
        public int _minHeightThreshold;
        public int _maxHeightThreshold;
        public double _safetyClearance;
        public double _separatorRegionRobot1;
        public double _separatorRegionRobot2;
        public int SentRobotAllNumber { get; set; } = 0;
        public HeightAligmentData _heightAligmentData1;
        public HeightAligmentData _heightAligmentData2;
        public HeightAligmentData _heightAligmentData3;
        public HeightAligmentData _heightAligmentData4;
        public HeightAligmentData _heightAligmentData5;
        public HeightAligmentData _heightAligmentData6;
        public HeightAligmentData _heightAligmentData7;
        public HeightAligmentData _heightAligmentData8;
        public PlaceWebBeltSelectData _placeWebBeltSelectData1;
        private FindCoorData remove;
        public bool ClearData()
        {
            try
            {
                remove = new FindCoorData();
                while (_bag.Count > 0)
                {
                    _bag.TryTake(out remove);
                }
                lock (_lock1)
                {
                    Robot1List.Clear();
                }
                lock (_lock2)
                {
                    Robot2List.Clear();
                }
                lock (_lock3)
                {
                    Robot3List.Clear();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
          
        }
        //发送总和
        public int GetSendAllNumber => SentRobotAllNumber;
        // 机械手数据集合（你需要的）
        public List<FindCoorData> Robot1List { get; private set; } = new List<FindCoorData>();
        public List<FindCoorData> Robot2List { get; private set; } = new List<FindCoorData>();
        public List<FindCoorData> Robot3List { get; private set; } = new List<FindCoorData>();

        private readonly object _lock1 = new object();
        private readonly object _lock2 = new object();
        private readonly object _lock3 = new object();


        public PointProcessor2(double xyTolerance = 50, double minXThreshold = 300,int minHeightThreshold=80,int maxHeightThreshold=400,double safetyClearance=150, double separatorRegionRobot1=700, double separatorRegionRobot2=3000)
        {
            _xyTolerance = xyTolerance;
            _minXThreshold = minXThreshold;
            _minHeightThreshold = minHeightThreshold;
            _maxHeightThreshold = maxHeightThreshold;
            _safetyClearance = safetyClearance;
            _separatorRegionRobot1 = separatorRegionRobot1;
            _separatorRegionRobot2 = separatorRegionRobot2;
            // new Thread(ProcessLoop) { IsBackground = true }.Start();
        }

        public void AddPoint(FindCoorData p) => _bag.Add(p);
        //  public void Stop() => _cts.Cancel();
        public int GetPointCount() => _bag.Count;


        //public  (FindCoorData[] filtered, FindCoorData[] removed) ClusterByProximityWithRemoved( FindCoorData[] data,double range = 5.0,int threshold = 1000)
        //{
        //    if (data == null || data.Length == 0)
        //        return (Array.Empty<FindCoorData>(), Array.Empty<FindCoorData>());

        //    // 过滤无效数据
        //    var validList = new List<FindCoorData>(data.Length);
        //    foreach (var item in data)
        //    {
        //        if (item?.WorldXScurren != null)
        //            validList.Add(item);
        //    }

        //    if (validList.Count <= threshold)
        //        return (validList.ToArray(), Array.Empty<FindCoorData>());

        //    // 排序
        //    var items = validList.ToArray();
        //    Array.Sort(items, (a, b) => a.WorldXScurren.D.CompareTo(b.WorldXScurren.D));

        //    var filtered = new List<FindCoorData>();
        //    var removed = new List<FindCoorData>();

        //    int i = 0;
        //    int n = items.Length;

        //    while (i < n)
        //    {
        //        var clusterCenter = items[i];
        //        double centerValue = clusterCenter.WorldXScurren.D;

        //        filtered.Add(clusterCenter);
        //        i++;

        //        // 收集同一聚类中的其他点
        //        int clusterStart = i;
        //        while (i < n && items[i].WorldXScurren.D <= centerValue + range)
        //        {
        //            removed.Add(items[i]);
        //            i++;
        //        }
        //    }

        //    return (filtered.ToArray(), removed.ToArray());
        //}


        public  (FindCoorData[] filtered, FindCoorData[] removed)ClusterByProximityWithRemoved2D( FindCoorData[] data,double xRange = 5.0,double yRange = 5.0,int threshold = 1000)
        {
            if (data == null || data.Length == 0)
                return (Array.Empty<FindCoorData>(), Array.Empty<FindCoorData>());

            if (data.Length <= threshold)
                return (data, Array.Empty<FindCoorData>());

            // 过滤无效数据
            var validList = new List<FindCoorData>(data.Length);
            foreach (var item in data)
            {
                if (item?.WorldXScurren != null && item?.WorldY != null)
                    validList.Add(item);
            }

            if (validList.Count <= threshold)
                return (validList.ToArray(), Array.Empty<FindCoorData>());

            // 按 X 值排序（主排序条件）
            var items = validList.ToArray();
            Array.Sort(items, (a, b) => a.WorldXScurren.D.CompareTo(b.WorldXScurren.D));

            var filtered = new List<FindCoorData>();
            var removed = new List<FindCoorData>();
            var processed = new bool[items.Length];  // 标记已处理的元素

            for (int i = 0; i < items.Length; i++)
            {
                if (processed[i]) continue;

                var clusterCenter = items[i];
                double centerX = clusterCenter.WorldXScurren.D;
                double centerY = clusterCenter.WorldY.D;

                filtered.Add(clusterCenter);
                processed[i] = true;

                // 查找同一聚类中的其他点
                for (int j = i + 1; j < items.Length; j++)
                {
                    if (processed[j]) continue;

                    var candidate = items[j];
                    double deltaX = Math.Abs(candidate.WorldXScurren.D - centerX);

                    // 如果 X 值超出范围+5，由于已排序，后续的 X 值都会更大，可以提前终止
                    if (deltaX > xRange)
                        break;

                    // 同时检查 Y 值
                    double deltaY = Math.Abs(candidate.WorldY.D - centerY);

                    if (deltaX <= xRange && deltaY <= yRange)
                    {
                        removed.Add(candidate);
                        processed[j] = true;
                    }
                }
            }

          //  Console.WriteLine($"2D聚类过滤: {items.Length} -> 保留 {filtered.Count}, 滤除 {removed.Count}");
            return (filtered.ToArray(), removed.ToArray());
        }

        /// <summary>
        /// 查找集合中X的中值，并返回这个值的对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public  FindCoorData GetWorldXMiddleBy(List<FindCoorData> list)
        {
            if (list == null || list.Count == 0)
                return null;

            // 1 个
            if (list.Count == 1)
                return list[0];

            // 2 个：直接返回第一个
            if (list.Count == 2)
                return list[0];

            // 3 个及以上：按 X 排序，取中间
            var sorted = list
                .OrderBy(p => p.WorldX.D)
                .ToList();

            int midIndex = sorted.Count / 2; // 自动向下取整
            return sorted[midIndex];
        }

        /// <summary>
        /// 查找集合中X的中值，并返回这个值的对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public FindCoorData GetWorldZMiddleBy(List<FindCoorData> list)
        {
            if (list == null || list.Count == 0)
                return null;

            // 1 个
            if (list.Count == 1)
                return list[0];

            // 2 个：直接返回第一个
            if (list.Count == 2)
                return list[0];

            // 3 个及以上：按 X 排序，取中间
            var sorted = list
                .OrderBy(p => p.Height.D)
                .ToList();

            int midIndex = sorted.Count / 2; // 自动向下取整
            return sorted[midIndex];
        }



        public void ProcessLoop()
        {
            //while (!_cts.IsCancellationRequested)
            //{
            //    Thread.Sleep(10);
            // 只有超过1000个时才进行聚类过滤，防止内存爆炸
            if (GetPointCount() > 50000)
            {
                FindCoorData[] snapshot1 = _bag.ToArray();
               (FindCoorData[] result, FindCoorData[] missResult)= ClusterByProximityWithRemoved2D(snapshot1);

                RemovePoints(missResult.ToList());
            }

            FindCoorData[] snapshot = _bag.ToArray();
            if (snapshot.Length == 0) return;
          
          

            var ordered = snapshot.OrderBy(p => (double)p.WorldXScurren).ToList();

            foreach (var minPoint in ordered)
            {
                if (GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding- minPoint.WorldXScurren.D <= _minXThreshold)
                    continue;

                // 聚类
                var group = snapshot.Where(p =>
                    Distance(p, minPoint) < _xyTolerance).ToList();

                //if (group.Count == 0) continue;
                //如果不包函自己就添加
                if (!group.Contains(minPoint))
                    group.Add(minPoint);
                //添加XYZ数据平均
                //double _AverageWorldX = group.Average(p => (double)p.WorldX);
                //double _AverageWorldY = group.Average(p => (double)p.WorldY);

               // double avgH = group.Average(p => (double)p.Height);

                FindCoorData AverageXData= GetWorldXMiddleBy(group);
                FindCoorData MinHeightData= GetWorldZMiddleBy(group);

                if (MinHeightData.Height > _minHeightThreshold && MinHeightData.Height < _maxHeightThreshold)
                {

                    var result = new FindCoorData()
                    {
                     
                        WorldX = AverageXData.WorldX,
                        WorldY = AverageXData.WorldY,
                        Height = MinHeightData.Height,
                        Score = AverageXData.Score,
                        WorldXScurren = AverageXData.WorldXScurren,
                        Attribute = AverageXData.Attribute,
                        encoding = AverageXData.encoding,
                        MouthWidthMm = MinHeightData.MouthWidthMm >= 0 ? MinHeightData.MouthWidthMm : AverageXData.MouthWidthMm,
                        MouthHeightMm = MinHeightData.MouthHeightMm >= 0 ? MinHeightData.MouthHeightMm : AverageXData.MouthHeightMm,
                        MouthAverageDiameterMm = MinHeightData.MouthAverageDiameterMm >= 0 ? MinHeightData.MouthAverageDiameterMm : AverageXData.MouthAverageDiameterMm
                 
                    };


                    // 根据 WorldY 判断属于哪个机器人区域
                    int robotId;

                    if (result.WorldY < _separatorRegionRobot1)
                    {
                        robotId = 1;
                    }
                    else if (result.WorldY < _separatorRegionRobot2)
                    {
                        robotId = 2;
                    }
                    else
                    {
                        robotId = 3;
                    }

                    // 标记安全区状态
                    if (robotId == 1)
                    {
                        lock (_lock1)
                        {
                            result.SafeRegionMark = IsInRobot1SafeRegion(result) ? 1 : 0;

                            Robot1List.Add(result);

                            // 按 X 从大到小排序
                            Robot1List.Sort((a, b) => b.WorldXScurren.D.CompareTo(a.WorldXScurren.D));
                        }
                    }
                    else if (robotId == 2)
                    {
                        lock (_lock2)
                        {
                            result.SafeRegionMark = IsInRobot2SafeRegion(result);

                            Robot2List.Add(result);

                            // 按 X 从大到小排序
                            Robot2List.Sort((a, b) => b.WorldXScurren.D.CompareTo(a.WorldXScurren.D));
                        }
                    }
                    else
                    {
                        lock (_lock3)
                        {
                            result.SafeRegionMark = IsInRobot3SafeRegion(result) ? 2 : 0;

                            Robot3List.Add(result);

                            // 按 X 从大到小排序
                            Robot3List.Sort((a, b) => b.WorldXScurren.D.CompareTo(a.WorldXScurren.D));
                        }
                    }

                    #region  2026.5.27 修改
                    // 归属机械手
                    //int robotId = result.WorldY < _separatorRegionRobot1 ? 1 : 2;

                    //// 标记安全区状态
                    //if (robotId == 1)
                    //{
                    //    lock (_lock1)
                    //    {
                    //        result.SafeRegionMark = IsInRobot1SafeRegion(result) ? 1 : 0;
                    //        Robot1List.Add(result);
                    //        Robot1List.Sort((a, b) => b.WorldXScurren.D.CompareTo(a.WorldXScurren.D));

                    //    }
                    //}
                    //else
                    //{
                    //    lock (_lock2)
                    //    {
                    //        result.SafeRegionMark = IsInRobot2SafeRegion(result) ? 1 : 0;
                    //        Robot2List.Add(result);
                    //        //从大到小进行排序
                    //        Robot2List.Sort((a, b) => b.WorldXScurren.D.CompareTo(a.WorldXScurren.D));

                    //    }
                    //}
                    #endregion
                }
                else
                {
                    LoggerHelper._.Warn($"检测高度超出限定值:{MinHeightData.Height}，已被过滤");
                }

                    // 删除数据点
                    RemovePoints(group);

                break;
            }
            // }
        }


        // ---- 安全区域判断 ----

        private bool IsInRobot1SafeRegion(FindCoorData p)
        {
            double y = p.WorldY;
            return (y >= _separatorRegionRobot1-_safetyClearance && y <= _separatorRegionRobot1);
        }

        private int IsInRobot2SafeRegion(FindCoorData p)
        {
            double y = p.WorldY;
            if(y >= _separatorRegionRobot1 && y <= _separatorRegionRobot1 + _safetyClearance)
            {
                return 1;
            }
            if(y <= _separatorRegionRobot2 && y >= _separatorRegionRobot2 - _safetyClearance)
            {
                return 2;
            }


            return 0;
        }

        private bool IsInRobot3SafeRegion(FindCoorData p)
        {
            double y = p.WorldY;
            return (y >= _separatorRegionRobot2 && y <= _separatorRegionRobot2 + _safetyClearance);
        }


        // ---- 工具方法 ----

        private void RemovePoints(List<FindCoorData> group)
        {
            List<FindCoorData> remain = new List<FindCoorData>();

            while (_bag.TryTake(out var p))
            {
                if (!group.Contains(p))
                    remain.Add(p);
            }

            foreach (var p in remain)
                _bag.Add(p);
        }

        private double Distance(FindCoorData a, FindCoorData b)
        {
             double dx = a.WorldXScurren.D - b.WorldXScurren.D;
           // double dx = a.WorldX.D - b.WorldX.D;
            double dy = a.WorldY.D - b.WorldY.D;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private FindCoorData FindAlternativePoint( List<FindCoorData> list, int safeRegionBlock, double baseX,double baseOffsetX)
        {
            // 从后往前找（偏向最新）
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var p = list[i];

                // 不是冲突区 ＋ X 差异不能超过50
                if (p.SafeRegionMark != safeRegionBlock &&
                    Math.Abs(p.WorldXScurren.D - baseX) <= 50)
                {
                    return p;
                }
            }

            return null;
        }




        private double AdjustHeightIfNeeded(HeightAligmentData heightAligmentData, FindCoorData point,double  height)
        {
  

            // 判断是否在 256 ~ 265 之间
            if(heightAligmentData.IsEnable)
            {
                bool heightMatched = height >= heightAligmentData.DownCompensation && height <= heightAligmentData.UpCompensation;
                bool mouthMatched = point != null &&
                    point.MouthAverageDiameterMm >= heightAligmentData.MouthMinMm &&
                    point.MouthAverageDiameterMm <= heightAligmentData.MouthMaxMm;

                if (heightMatched && mouthMatched)
                {
                    height = heightAligmentData.BaseHeight;
                    point.Attribute =Convert.ToInt32(heightAligmentData.PlaceAttr);
                    point.placeCompensation=Convert.ToInt32(heightAligmentData.PlaceHeightCompeensation);

                    return height;  
                }
            }
            return height; 
        }

        private  int GetAttributeByY( FindCoorData coor, bool enable, double y1Min, double y1Max, int attr1, double y2Min, double y2Max, int attr2,double y3Min, double y3Max, int attr3, double y4Min, double y4Max, int attr4)
        {
            // 使能关闭，直接返回原属性
            if (!enable || coor == null)
                return coor?.Attribute ?? 1;

            double y = coor.WorldY.D;

            if (y >= y1Min && y <= y1Max)
                return attr1;

            if (y >= y2Min && y <= y2Max)
                return attr2;

            if (y >= y3Min && y <= y3Max)
                return attr3;

            if (y >= y4Min && y <= y4Max)
                return attr4;

            // 不在任何范围内，返回原属性（兜底）
            return coor.Attribute;
        }

        private void SendToRobot(int robotId, List<FindCoorData> robotList, object listLock, SuperSimpleTcpHelper tcp, ref FindCoorData currentSendPoint)
        {
            if (robotList.Count == 0) return;

            // 取最后一个点（你原代码）
            var last = robotList[robotList.Count - 1];//1.19
           // var last = robotList.Last();

            // double sendHeight = ;
            //int attribute=last.Attribute;
            //int placeCompensation=last.placeCompensation;

            double sendHeight = AdjustHeightIfNeeded(_heightAligmentData1, last, last.Height.D);
            sendHeight = AdjustHeightIfNeeded(_heightAligmentData2, last, sendHeight);
            sendHeight = AdjustHeightIfNeeded(_heightAligmentData3, last, sendHeight);
            sendHeight = AdjustHeightIfNeeded(_heightAligmentData4, last, sendHeight);
            sendHeight = AdjustHeightIfNeeded(_heightAligmentData5, last, sendHeight);
            sendHeight = AdjustHeightIfNeeded(_heightAligmentData6, last, sendHeight);
            sendHeight = AdjustHeightIfNeeded(_heightAligmentData7, last, sendHeight);
            sendHeight = AdjustHeightIfNeeded(_heightAligmentData8, last, sendHeight);
            //启用网带强制使能，按Y划分，正常属性为1，如果属性为2时放入公共网带
            int attribute = GetAttributeByY(last, _placeWebBeltSelectData1.IsEnable, _placeWebBeltSelectData1.SegmentationThreshold1Down, _placeWebBeltSelectData1.SegmentationThreshold1Up, _placeWebBeltSelectData1.SegmentationAttri1, _placeWebBeltSelectData1.SegmentationThreshold2Down, _placeWebBeltSelectData1.SegmentationThreshold2Up, _placeWebBeltSelectData1.SegmentationAttri2, _placeWebBeltSelectData1.SegmentationThreshold3Down, _placeWebBeltSelectData1.SegmentationThreshold3Up, _placeWebBeltSelectData1.SegmentationAttri3,_placeWebBeltSelectData1.SegmentationThreshold4Down,_placeWebBeltSelectData1.SegmentationThreshold4Up,_placeWebBeltSelectData1.SegmentationAttri4);
            //展机
            //string str = string.Format("Image\r\n[X:{0};Y:{1};Z:{2};ATTR:{3};ID:{4}]\r\nDone", Math.Round(GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding - last.encoding + last.WorldX.D, 2), -Math.Round(last.WorldY.D, 2), Math.Round(sendHeight, 2), attribute, last.placeCompensation);

            //string logStr = string.Format("Image\r\n[X:{0};Y:{1};Z:{2};ATTR:{3};ID:{4};Safe:{5};Rob:{6}]\r\nDone", Math.Round(GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding - last.encoding + last.WorldX.D, 2), -Math.Round(last.WorldY.D, 2), Math.Round(sendHeight, 2), attribute, last.placeCompensation, last.SafeRegionMark, robotId);

            //正常生产
            string str = string.Format("Image\r\n[X:{0};Y:{1};Z:{2};ATTR:{3};ID:{4}]\r\nDone", Math.Round(GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding - last.encoding + last.WorldX.D, 2), Math.Round(last.WorldY.D, 2), Math.Round(-sendHeight, 2), attribute, last.placeCompensation);

            string logStr = string.Format("Image\r\n[X:{0};Y:{1};Z:{2};ATTR:{3};ID:{4};Safe:{5};Rob:{6};Mouth:{7}]\r\nDone", Math.Round(GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding - last.encoding + last.WorldX.D, 2), Math.Round(last.WorldY.D, 2), Math.Round(-sendHeight, 2), attribute, last.placeCompensation, last.SafeRegionMark, robotId, Math.Round(last.MouthAverageDiameterMm, 2));



            tcp.Send(str);
            //临时锁标记
            SetRobotPending(robotId);

            SentRobotAllNumber++;
            _eventMsg?.Invoke(logStr);
            lock (listLock)
            {
                robotList.RemoveAt(robotList.Count - 1);
            }

            currentSendPoint = last;

            //BeginInvoke(new Action(() => { LogWinform(logStr, LogInfoPath); }));
            //BeginInvoke(new Action(() => { binding.AddTotal++; }));
        }


      

        private FindCoorData SendCurrentCoor1 = new FindCoorData();
        private FindCoorData SendCurrentCoor2 = new FindCoorData();
        private FindCoorData SendCurrentCoor3 = new FindCoorData();


        private int SendPending1 = 0;
        private int SendPending2 = 0;
        private int SendPending3 = 0;

        private bool IsRobotCanSend(int robotId, int robotState)
        {
            if (robotState != 0) return false;

            if (robotId == 1 && SendPending1 == 1) return false;
            if (robotId == 2 && SendPending2 == 1) return false;
            if (robotId == 3 && SendPending3 == 1) return false;

            return true;
        }

        private void SetRobotPending(int robotId)
        {
            if (robotId == 1) SendPending1 = 1;
            else if (robotId == 2) SendPending2 = 1;
            else if (robotId == 3) SendPending3 = 1;
        }

        private void RefreshRobotPending()
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1 == 1)
                SendPending1 = 0;

            if (GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2 == 1)
                SendPending2 = 0;

            if (GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData3 == 1)
                SendPending3 = 0;
        }

        private bool IsRobotPending(int robotId)
        {
            if (robotId == 1) return SendPending1 == 1;
            if (robotId == 2) return SendPending2 == 1;
            if (robotId == 3) return SendPending3 == 1;

            return false;
        }

        //先抓X最小的

        public void SendCoorValue3Robot(double baseOffsetX,SuperSimpleTcpHelper superTcp1,SuperSimpleTcpHelper superTcp2,SuperSimpleTcpHelper superTcp3)
        {

            RefreshRobotPending();
            TrySendRobotMinX(
                2,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2,
                baseOffsetX,
                Robot2List,
                _lock2,
                superTcp2,
                ref SendCurrentCoor2,
                1,
                SendCurrentCoor1,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1,
                Robot1List,
                1,
                3,
                SendCurrentCoor3,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData3,
                Robot3List,
                2);

            TrySendRobotMinX(
                1,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1,
                baseOffsetX,
                Robot1List,
                _lock1,
                superTcp1,
                ref SendCurrentCoor1,
                2,
                SendCurrentCoor2,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2,
                Robot2List,
                1,
                0,
                null,
                0,
                null,
                0);

            TrySendRobotMinX(
                3,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData3,
                baseOffsetX,
                Robot3List,
                _lock3,
                superTcp3,
                ref SendCurrentCoor3,
                2,
                SendCurrentCoor2,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2,
                Robot2List,
                2,
                0,
                null,
                0,
                null,
                0);
        }

        private void TrySendRobotMinX(int robotId, int robotState, double baseOffsetX, List<FindCoorData> selfList, object selfLock, SuperSimpleTcpHelper tcp, ref FindCoorData currentSend, int otherRobotId1, FindCoorData otherCurrent1, int otherRobotState1, List<FindCoorData> otherList1, int conflictSafeMark1, int otherRobotId2, FindCoorData otherCurrent2, int otherRobotState2, List<FindCoorData> otherList2, int conflictSafeMark2)
        {
            try
            {
                if (!IsRobotCanSend(robotId, robotState)) return;;

                lock (selfLock)
                {
                    if (selfList.Count == 0) return;

                    var target = FindSafeCandidateMinX(
                        selfList,
                        baseOffsetX,
                        otherRobotId1,
                        otherCurrent1,
                        otherRobotState1,
                        otherList1,
                        conflictSafeMark1,
                        otherRobotId2,
                        otherCurrent2,
                        otherRobotState2,
                        otherList2,
                        conflictSafeMark2);

                    if (target == null) return;

                    List<FindCoorData> temp = new List<FindCoorData>() { target };

                    SendToRobot(robotId, temp, selfLock, tcp, ref currentSend);

                    selfList.Remove(target);
                }
            }
            catch
            {
                // 建议加日志
            }
        }

        private FindCoorData FindSafeCandidateMinX(List<FindCoorData> selfList, double baseOffsetX, int otherRobotId1, FindCoorData otherCurrent1, int otherRobotState1, List<FindCoorData> otherList1, int conflictSafeMark1, int otherRobotId2, FindCoorData otherCurrent2, int otherRobotState2, List<FindCoorData> otherList2, int conflictSafeMark2)
        {
            if (selfList == null || selfList.Count == 0)
                return null;

            double baseWorldX = selfList[selfList.Count - 1].WorldXScurren.D;

            for (int i = selfList.Count - 1; i >= 0; i--)
            {
                FindCoorData candidate = selfList[i];
                double candidateWorldX = candidate.WorldXScurren.D;
                if (candidateWorldX < baseWorldX)
                    continue;

                if (baseOffsetX > 0 && candidateWorldX > baseWorldX + baseOffsetX)
                    continue;

                bool canSend = CanSendBySafeRegion(candidate, conflictSafeMark1, otherRobotId1, otherCurrent1, otherRobotState1, otherList1);

                if (canSend)
                {
                    canSend = CanSendBySafeRegion(candidate, conflictSafeMark2, otherRobotId2, otherCurrent2, otherRobotState2, otherList2);
                }

                if (canSend)
                    return candidate;
            }

            return null;
        }

        //先抓Y最大的
        public void SendCoorValueWorldYMinSort3Robot(double baseOffsetX,SuperSimpleTcpHelper superTcp1,SuperSimpleTcpHelper superTcp2,SuperSimpleTcpHelper superTcp3)
        {
            RefreshRobotPending();
            // 先发中间机器人，因为 Robot2 同时可能和 Robot1、Robot3 冲突
            TrySendRobotForThree(
                2,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2,
                baseOffsetX,
                Robot2List,
                _lock2,
                superTcp2,
                ref SendCurrentCoor2,
                1,
                SendCurrentCoor1,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1,
                Robot1List,
                1,
                3,
                SendCurrentCoor3,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData3,
                Robot3List,
                2,
                p => p.OrderBy(x => x.WorldY.D));

            TrySendRobotForThree(
                1,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1,
                baseOffsetX,
                Robot1List,
                _lock1,
                superTcp1,
                ref SendCurrentCoor1,
                2,
                SendCurrentCoor2,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2,
                Robot2List,
                1,
                0,
                null,
                0,
                null,
                0,
                p => p.OrderBy(x => x.WorldY.D));

            TrySendRobotForThree(
                3,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData3,
                baseOffsetX,
                Robot3List,
                _lock3,
                superTcp3,
                ref SendCurrentCoor3,
                2,
                SendCurrentCoor2,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2,
                Robot2List,
                2,
                0,
                null,
                0,
                null,
                0,
                p => p.OrderBy(x => x.WorldY.D));
        }

        //先抓高度最大的
        public void SendCoorValueHeightMaxSort3Robot(double baseOffsetX, SuperSimpleTcpHelper superTcp1, SuperSimpleTcpHelper superTcp2, SuperSimpleTcpHelper superTcp3)
        {

            RefreshRobotPending();
            // 先发中间机器人，因为 Robot2 同时可能和 Robot1、Robot3 冲突
            TrySendRobotForThree(
                2,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2,
                baseOffsetX,
                Robot2List,
                _lock2,
                superTcp2,
                ref SendCurrentCoor2,
                1,
                SendCurrentCoor1,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1,
                Robot1List,
                1,
                3,
                SendCurrentCoor3,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData3,
                Robot3List,
                2,
                p => p.OrderByDescending(x => x.Height.D));

            TrySendRobotForThree(
                1,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1,
                baseOffsetX,
                Robot1List,
                _lock1,
                superTcp1,
                ref SendCurrentCoor1,
                2,
                SendCurrentCoor2,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2,
                Robot2List,
                1,
                0,
                null,
                0,
                null,
                0,
               p => p.OrderByDescending(x => x.Height.D));

            TrySendRobotForThree(
                3,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData3,
                baseOffsetX,
                Robot3List,
                _lock3,
                superTcp3,
                ref SendCurrentCoor3,
                2,
                SendCurrentCoor2,
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2,
                Robot2List,
                2,
                0,
                null,
                0,
                null,
                0,
                p => p.OrderByDescending(x => x.Height.D));
        }

        private void TrySendRobotForThree(int robotId, int robotState, double baseOffsetX, List<FindCoorData> selfList, object selfLock, SuperSimpleTcpHelper tcp, ref FindCoorData currentSend, int otherRobotId1, FindCoorData otherCurrent1, int otherRobotState1, List<FindCoorData> otherList1, int conflictSafeMark1, int otherRobotId2, FindCoorData otherCurrent2, int otherRobotState2, List<FindCoorData> otherList2, int conflictSafeMark2, Func<IEnumerable<FindCoorData>, IEnumerable<FindCoorData>> sortFunc)
        {
            try
            {
                if (!IsRobotCanSend(robotId, robotState)) return;

                lock (selfLock)
                {
                    if (selfList.Count == 0) return;

                    var basePoint = selfList[selfList.Count - 1];
                    double baseWorldX = basePoint.WorldXScurren.D;

                    var candidates = selfList
                        .Where(p => p.WorldXScurren.D >= baseWorldX)
                        .Where(p => baseOffsetX <= 0 || p.WorldXScurren.D <= baseWorldX + baseOffsetX)
                        .ToList();

                    if (candidates.Count == 0) return;

                    var sorted = sortFunc(candidates).ToList();

                    foreach (var target in sorted)
                    {
                        bool canSend = true;

                        if (!CanSendBySafeRegion(
                            target,
                            conflictSafeMark1,
                            otherRobotId1,
                            otherCurrent1,
                            otherRobotState1,
                            otherList1))
                        {
                            canSend = false;
                        }

                        if (canSend &&
                            !CanSendBySafeRegion(
                                target,
                                conflictSafeMark2,
                                otherRobotId2,
                                otherCurrent2,
                                otherRobotState2,
                                otherList2))
                        {
                            canSend = false;
                        }

                        if (canSend)
                        {
                            List<FindCoorData> temp = new List<FindCoorData>() { target };

                            SendToRobot(robotId, temp, selfLock, tcp, ref currentSend);

                            selfList.Remove(target);

                            return;
                        }
                    }
                }
            }
            catch
            {
                // 建议这里加日志
            }
        }
        //安全去仲裁方法
        private bool CanSendBySafeRegion(FindCoorData target, int conflictSafeMark, int otherRobotId, FindCoorData otherCurrent, int otherRobotState, List<FindCoorData> otherList)
        {
            if (target == null) return false;

            if (conflictSafeMark == 0) return true;

            if (target.SafeRegionMark != conflictSafeMark) return true;

            // 对方机械手正在安全区动作，当前点不能发
            if (otherCurrent != null &&
                otherCurrent.SafeRegionMark == conflictSafeMark &&
                (otherRobotState == 1 || IsRobotPending(otherRobotId)))
            {
                return false;
            }

            if (otherList == null || otherList.Count == 0)
                return true;

            var otherConflictPoint = otherList
                .Where(p => p.SafeRegionMark == conflictSafeMark)
                .OrderBy(p => p.WorldXScurren.D)
                .FirstOrDefault();

            if (otherConflictPoint == null)
                return true;

            // X 小的先发，避免两个机械手同时抢安全区
            return target.WorldXScurren.D < otherConflictPoint.WorldXScurren.D;
        }


        #region 2个机械手********************************************************************************************************

        public void SendCoorValue(double baseOffsetX, SuperSimpleTcpHelper superTcp1, SuperSimpleTcpHelper superTcp2)
        {
            try
            {
                // ======================
                // 机器人 2 发送逻辑
                // ======================
                if (GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2 == 0 && Robot2List.Count > 0)// 先判断机器人2是否有坐标、是否有可发送坐标
                {
                    var r2Point = Robot2List.Last();

                    bool canSendR2 = true;
                    if (SendCurrentCoor1.SafeRegionMark == 1 && GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1 == 1)//判断机器人1是否有坐标且这个坐标是否在安全区
                    {
                        canSendR2 = false;
                    }
                    else//这里比较谁的安全区X坐标最小，最小的先发送
                    {
                        if (Robot1List.Count > 0)
                        {
                            var r1Point = Robot1List.Last();

                            if (r1Point.SafeRegionMark == 1 && r2Point.SafeRegionMark == 1)
                            {
                                // 冲突 → 用 WorldX 仲裁
                                canSendR2 = r2Point.WorldXScurren.D <= r1Point.WorldXScurren.D;
                            }
                        }
                    }

                    if (canSendR2)
                    {
                        // 没限制 ⇒ 直接发最新
                        SendToRobot(2, Robot2List, _lock2, superTcp2, ref SendCurrentCoor2);
                        return;
                    }
                    else
                    {
                        // ---- 冲突情况 ----
                        // 最新点作为基准点
                        double baseX = Robot2List[Robot2List.Count - 1].WorldXScurren.D;

                        // 查找符合安全区 且 X 不超过±50 的点
                        var alternative = FindAlternativePoint(Robot2List, 1, baseX, baseOffsetX);

                        if (alternative != null)
                        {
                            // 手动发送 alternative
                            List<FindCoorData> tempList = new List<FindCoorData>() { alternative };
                            SendToRobot(2, tempList, _lock2, superTcp2, ref SendCurrentCoor2);


                            // 删除该点
                            lock (_lock2)
                            {
                                Robot2List.Remove(alternative);
                            }
                            return;
                        }
                        else
                        {
                            // 无替代点 → 不发（保持安全区避免撞机）
                        }
                    }
                }
            }
            catch
            {
                //LogWinform("发送给机器人2坐标数据异常", LogInfoPath);
            }


            try
            {
                // ======================
                // 机器人 1 发送逻辑
                // ======================
                if (GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1 == 0 && Robot1List.Count > 0)
                {
                    var r1Point = Robot1List.Last();

                    bool canSendR1 = true;
                    if (SendCurrentCoor2.SafeRegionMark == 1 && GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2 == 1)
                    {
                        canSendR1 = false;
                    }
                    else
                    {

                        if (Robot2List.Count > 0)
                        {
                            var r2Point = Robot2List.Last();

                            if (r1Point.SafeRegionMark == 1 && r2Point.SafeRegionMark == 1)
                            {
                                // 冲突 → 用 WorldX 仲裁
                                canSendR1 = r1Point.WorldXScurren.D < r2Point.WorldXScurren.D;
                            }
                        }
                    }

                    if (canSendR1)
                    {
                        // 正常情况 → 直接发
                        SendToRobot(1, Robot1List, _lock1, superTcp1, ref SendCurrentCoor1);

                    }
                    else
                    {
                        // ---- 冲突情况 ----
                        // 最新点作为基准点
                        double baseX = Robot1List[Robot1List.Count - 1].WorldXScurren.D;

                        // 查找符合安全区 且 X 不超过±50 的点
                        var alternative = FindAlternativePoint(Robot1List, 1, baseX, baseOffsetX);

                        if (alternative != null)
                        {
                            // 手动发送 alternative
                            List<FindCoorData> tempList = new List<FindCoorData>() { alternative };
                            SendToRobot(1, tempList, _lock1, superTcp1, ref SendCurrentCoor1);


                            // 删除该点
                            lock (_lock1)
                            {
                                Robot1List.Remove(alternative);
                            }
                        }
                        else
                        {
                            // 无替代点 → 不发（保持安全区避免撞机）
                        }
                    }

                }
            }
            catch
            {
                // LogWinform("发送给机器人1坐标数据异常", LogInfoPath);
            }
        }



        public void SendCoorValueWorldYMinSort(double baseOffsetX,SuperSimpleTcpHelper superTcp1,SuperSimpleTcpHelper superTcp2)
        {
            RefreshRobotPending();

            // ======================
            // Robot2 优先
            // ======================
            TrySendRobot(2,GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2, baseOffsetX,Robot2List, _lock2,superTcp2, ref SendCurrentCoor2,SendCurrentCoor1, GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1, Robot1List );

            // ======================
            // Robot1
            // ======================
       
        
            TrySendRobot(1, GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1, baseOffsetX, Robot1List,_lock1,superTcp1,ref SendCurrentCoor1, SendCurrentCoor2, GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2, Robot2List);
        }


        public void SendCoorValueHeightMaxSort( double baseOffsetX, SuperSimpleTcpHelper superTcp1, SuperSimpleTcpHelper superTcp2)
        {
            RefreshRobotPending();
            // ======================
            // Robot2 优先
            // ======================
            TrySendRobotSortZ(2, GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2, baseOffsetX, Robot2List, _lock2, superTcp2, ref SendCurrentCoor2, SendCurrentCoor1, GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1, Robot1List);
        
            // ======================
            // Robot1
            // ======================
            TrySendRobotSortZ(1, GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1, baseOffsetX, Robot1List, _lock1, superTcp1, ref SendCurrentCoor1, SendCurrentCoor2, GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2, Robot2List );
        }

        //先抓Y小的
        private void TrySendRobot(int robotId,int robotState /* 0=空闲 1=运行*/, double baseOffsetX,  List<FindCoorData> selfList, object selfLock, SuperSimpleTcpHelper tcp, ref FindCoorData currentSend, FindCoorData otherCurrent, int otherRobotState, List<FindCoorData> otherList)
        {
            try
            {

                if (!IsRobotCanSend(robotId, robotState)) return;

                lock (selfLock)
                {
                    if (selfList.Count == 0) return;

                    // =========================
                    // Step1：取最新点（基准点）
                    // =========================
                    var basePoint = selfList[selfList.Count - 1];
                    double baseWorldX = basePoint.WorldXScurren.D;

                    // =========================
                    // Step2：筛选窗口（+50）
                    // =========================
                    var candidates = selfList
                        .Where(p => p.WorldXScurren.D <= baseWorldX + baseOffsetX)
                        .ToList();

                    if (candidates.Count == 0) return;

                    // =========================
                    // Step3：按 Y 排序（最小优先）
                    // =========================
                    var sorted = candidates
                        .OrderBy(p => p.WorldY.D)
                        .ToList();

                    foreach (var target in sorted)
                    {
                        bool canSend = true;

                        // =========================
                        // 🚨 仲裁1：对方正在安全区
                        // =========================
                        if (target.SafeRegionMark == 1 && /*⚠️ 当前点在安全区才限制*/otherCurrent != null && otherCurrent.SafeRegionMark == 1 && otherRobotState == 1)
                        {
                            canSend = false;
                        }
                        else
                        {
                            // =========================
                            // 🚨 仲裁2：双方候选点都在安全区
                            // =========================
                            if (target.SafeRegionMark == 1 && otherList.Count > 0)
                            {
                                var otherLast = otherList[otherList.Count - 1];

                                if (otherLast.SafeRegionMark == 1)
                                {
                                    // WorldX 小的优先
                                    canSend = target.WorldXScurren.D <
                                              otherLast.WorldXScurren.D;
                                }
                            }
                        }

                        // =========================
                        // ✅ 找到可发送点
                        // =========================
                        if (canSend)
                        {
                            List<FindCoorData> temp = new List<FindCoorData>() { target };

                            SendToRobot(robotId, temp, selfLock, tcp, ref currentSend);

                            // 删除已发送
                            selfList.Remove(target);
                          

                            return;
                        }
                    }

                    // ❗ 如果一个都发不了 → 直接返回（避免撞机）
                }
            }
            catch
            {
          
                // 可加日志
            }
        }

        //先抓高，再抓矮
        private void TrySendRobotSortZ(int robotId, int robotState, double baseOffsetX, /* 0=空闲 1=运行*/ List<FindCoorData> selfList, object selfLock, SuperSimpleTcpHelper tcp, ref FindCoorData currentSend, FindCoorData otherCurrent, int otherRobotState, List<FindCoorData> otherList)
        {
            try
            {

                if (!IsRobotCanSend(robotId, robotState)) return;

                lock (selfLock)
                {
                    if (selfList.Count == 0) return;

                    // =========================
                    // Step1：取最新点（基准点）
                    // =========================
                    var basePoint = selfList[selfList.Count - 1];
                    double baseWorldX = basePoint.WorldXScurren.D;

                    // =========================
                    // Step2：筛选窗口（+50）
                    // =========================
                    var candidates = selfList
                        .Where(p => p.WorldXScurren.D <= baseWorldX + baseOffsetX)
                        .ToList();

                    if (candidates.Count == 0) return;

                    // =========================
                    // Step3：按 Z 排序（最高优先）
                    // =========================
                    var sorted = candidates
                        .OrderByDescending(p => p.Height.D)
                        .ToList();

                    foreach (var target in sorted)
                    {
                        bool canSend = true;

                        // =========================
                        // 🚨 仲裁1：对方正在安全区
                        // =========================

                        if (target.SafeRegionMark == 1 && /*⚠️ 当前点在安全区才限制*/otherCurrent != null && otherCurrent.SafeRegionMark == 1 && otherRobotState == 1)
                        {
                            canSend = false;
                        }
                        else
                        {
                            // =========================
                            // 🚨 仲裁2：双方候选点都在安全区
                            // =========================
                            if (target.SafeRegionMark == 1 && otherList.Count > 0)
                            {
                                var otherLast = otherList[otherList.Count - 1];

                                if (otherLast.SafeRegionMark == 1)
                                {
                                    // WorldX 小的优先
                                    canSend = target.WorldXScurren.D <
                                              otherLast.WorldXScurren.D;
                                }
                            }
                        }

                        // =========================
                        // ✅ 找到可发送点
                        // =========================
                        if (canSend)
                        {
                            List<FindCoorData> temp = new List<FindCoorData>() { target };

                            SendToRobot(robotId, temp, selfLock, tcp, ref currentSend);

                            // 删除已发送
                            selfList.Remove(target);
                         
                            return;
                        }
                    }

                    // ❗ 如果一个都发不了 → 直接返回（避免撞机）
                }
            }
            catch
            {
              
                // 可加日志
            }
        }
        #endregion
    }



}
