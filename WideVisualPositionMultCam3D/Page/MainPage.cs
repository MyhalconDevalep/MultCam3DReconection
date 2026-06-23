using ChoiceTech.Halcon.Control;
using Force.DeepCloner;
using HalconDotNet;
using Microsoft.ML.OnnxRuntime;
using OpenCvSharp;
using OpenCvSharp.Flann;
using SevenZip.Compression.LZ;
using Sunny.UI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.Models;
using WideVisualPositionMultCam3D.ToolClass;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WideVisualPositionMultCam3D.Page
{
    public partial class MainPage : UIPage
    {


        private int TimeoutMs = 100;
        // 模拟图像数据（实际项目中替换为 Bitmap 或 Mat）

        private MultiCameraPairingManager pairingMgr;
        private PointProcessor2 _pointProcessor= new PointProcessor2();
        private readonly Stopwatch _mainLoopWatch = new Stopwatch();
        private volatile bool _isClosing = false;
        public MainPage()
        {
            InitializeComponent();
            this.Load += Page1_Load;
        }


        public string[] class_names;
        public int class_num;
        CameraGroup groupA;
        CameraGroup groupB;
        CameraGroup groupC;
        CameraGroup groupD;
        CameraGroup groupE;
        CameraGroup groupF;


        public SuperSimpleTcpHelper _superSimpleTcp1;
        public SuperSimpleTcpHelper _superSimpleTcp2;
        public SuperSimpleTcpHelper _superSimpleTcp3;
        public SuperSimpleTcpHelper _superSimpleTcp4;
        public SuperSimpleTcpHelper _superSimpleTcp5;
        string IP1;
        int port1 = 0;
        string IP2;
        int port2 = 0;
        string IP3;
        int port3 = 0;
        string IP4;
        int port4 = 0;
        string IP5;
        int port5 = 0;
        private TableLayoutHalconDispZoomHWindow_final halconController1;
        private TableLayoutHalconDispZoomHWindow_final halconController2;
        private TableLayoutHalconDispZoomHWindow_final halconController3;
        private TableLayoutHalconDispZoomHWindow_final halconController4;
        private TableLayoutHalconDispZoomHWindow_final halconController5;
        private TableLayoutHalconDispZoomHWindow_final halconController6;
        private void LoadCalibrationData(CameraGroupConfig cameraGroupConfig, int groupIndex)
        {
            if (!CalibrationFolderLoader.TryLoadCalibrationData(cameraGroupConfig, groupIndex, out string warning))
            {
                MessageBox.Show(warning);
            }
        }
        private void Page1_Load(object sender, EventArgs e)
        {

            // 1. 初始化控制器
            halconController1 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel4);
            halconController1.RegisterMany(hWindowControl1, hWindowControl2, hWindowControl3, hWindowControl4);


            halconController2 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel8);
            halconController2.RegisterMany(hWindowControl5, hWindowControl6, hWindowControl7, hWindowControl8);


            halconController3 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel9);
            halconController3.RegisterMany(hWindowControl9, hWindowControl10, hWindowControl11, hWindowControl12);

            halconController4 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel7);
            halconController4.RegisterMany(hWindowControl13, hWindowControl14, hWindowControl15, hWindowControl16);


            halconController5 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel12);
            halconController5.RegisterMany(hWindowControl17, hWindowControl18, hWindowControl19, hWindowControl20);


            halconController6 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel13);
            halconController6.RegisterMany(hWindowControl21, hWindowControl22, hWindowControl23, hWindowControl24);


            try
            {
                IP1 = GlobalStaticData.OperateConfig.GetValue("TcpIp", "IP1");
                port1 = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("TcpIp", "Port1"));
                IP2 = GlobalStaticData.OperateConfig.GetValue("TcpIp", "IP2");
                port2 = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("TcpIp", "Port2"));
                IP3 = GlobalStaticData.OperateConfig.GetValue("TcpIp", "IP3");
                port3 = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("TcpIp", "Port3"));
                IP4 = GlobalStaticData.OperateConfig.GetValue("TcpIp", "IP4");
                port4 = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("TcpIp", "Port4"));
                IP5 = GlobalStaticData.OperateConfig.GetValue("TcpIp", "IP5");
                port5 = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("TcpIp", "Port5"));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"参数加载错误:{ex.Message}");
            }

            _superSimpleTcp1 =new SuperSimpleTcpHelper(IP1, port1,false);
            _superSimpleTcp1.ActionPrintConnectionLog += actionPrintConnectionLog1;

            _superSimpleTcp2 = new SuperSimpleTcpHelper(IP2, port2, true);
            _superSimpleTcp2.ActionPrintConnectionLog += actionPrintConnectionLog2;
            _superSimpleTcp2.ActionReceivedMsg += ReceivedMsgEncoding;

            _superSimpleTcp3 = new SuperSimpleTcpHelper(IP3, port3, false);
            _superSimpleTcp3.ActionPrintConnectionLog += actionPrintConnectionLog3;

            _superSimpleTcp4 = new SuperSimpleTcpHelper(IP4, port4, true);
            _superSimpleTcp4.ActionPrintConnectionLog += actionPrintConnectionLog4;

            _superSimpleTcp5 = new SuperSimpleTcpHelper(IP5, port5, false);
            _superSimpleTcp5.ActionPrintConnectionLog += actionPrintConnectionLog5;


            //给数据处理类绑定一个显示发送消息的信息显示委托
            _pointProcessor._eventMsg = LogWinform;

           // _findCoorData = new List<FindCoorData>();

           GlobalStaticData._imageBuffer = new ConcurrentDictionary<int, HObject>();
            GlobalStaticData.blockingCollectiontest = new ConcurrentBag<FindCoorData>();
            lbl_hasBeenSent.DataBindings.Add("Text", GlobalStaticData.UpdataBingdingDisplayMsgq, "RobotUseData1");
            lbl_hasBeenSent2.DataBindings.Add("Text", GlobalStaticData.UpdataBingdingDisplayMsgq, "RobotUseData2");
            lbl_hasBeenSent3.DataBindings.Add("Text", GlobalStaticData.UpdataBingdingDisplayMsgq, "RobotUseData3");
            lbl_SendDataNum.DataBindings.Add("Text", GlobalStaticData.UpdataBingdingDisplayMsgq, "SendDataNum");


            LoadCalibrationData(GlobalStaticData.CameraGroupConfig1, 1);
            LoadCalibrationData(GlobalStaticData.CameraGroupConfig2, 2);
            LoadCalibrationData(GlobalStaticData.CameraGroupConfig3, 3);
            LoadCalibrationData(GlobalStaticData.CameraGroupConfig4, 4);
            LoadCalibrationData(GlobalStaticData.CameraGroupConfig5, 5);
            LoadCalibrationData(GlobalStaticData.CameraGroupConfig6, 6);
            //for (int i = 0; i < CameraCount; i++)
            //{
            //    autoResetEvents.Add(new AutoResetEvent(false));
            //}
            //_barrier.SignalAndWait();//定义几个相机,就在几个相机里面加上这一句来同步


            //相机取图定位流程
            groupA = new CameraGroup(GlobalStaticData.CameraGroupConfig1);
            groupA.ActionDispImageEvent += groupADispImageEvent;
            groupA.ActionDispYoloRoiEvent += groupADispYoloRoiEvent;

            groupB = new CameraGroup(GlobalStaticData.CameraGroupConfig2);
            groupB.ActionDispImageEvent += groupBDispImageEvent;
            groupB.ActionDispYoloRoiEvent += groupBDispYoloRoiEvent;

            groupC = new CameraGroup(GlobalStaticData.CameraGroupConfig3);
            groupC.ActionDispImageEvent += groupCDispImageEvent;
            groupC.ActionDispYoloRoiEvent += groupCDispYoloRoiEvent;

            groupD = new CameraGroup(GlobalStaticData.CameraGroupConfig4);
            groupD.ActionDispImageEvent += groupDDispImageEvent;
            groupD.ActionDispYoloRoiEvent += groupDDispYoloRoiEvent;

            groupE = new CameraGroup(GlobalStaticData.CameraGroupConfig4);
            groupE.ActionDispImageEvent += groupEDispImageEvent;
            groupE.ActionDispYoloRoiEvent += groupEDispYoloRoiEvent;

            groupF = new CameraGroup(GlobalStaticData.CameraGroupConfig4);
            groupF.ActionDispImageEvent += groupFDispImageEvent;
            groupF.ActionDispYoloRoiEvent += groupFDispYoloRoiEvent;

            //相机同步管理
            pairingMgr = new MultiCameraPairingManager(GlobalStaticData.CameraCount, TimeoutMs);
            pairingMgr.Start();
            //Task.Factory.StartNew(() => { PairingLoop(); },TaskCreationOptions.LongRunning);
            if (GlobalStaticData.CameraCount > 0)
            {
                GlobalStaticData.HIKCamera1.eventRun += HIKCamera1_eventRun;
            }
            if (GlobalStaticData.CameraCount > 1)
            {
                GlobalStaticData.HIKCamera2.eventRun += HIKCamera2_eventRun;
            }
            if (GlobalStaticData.CameraCount > 2)
            {
                GlobalStaticData.HIKCamera3.eventRun += HIKCamera3_eventRun;
            }
            if (GlobalStaticData.CameraCount > 3)
            {
                GlobalStaticData.HIKCamera4.eventRun += HIKCamera4_eventRun;
            }
            if (GlobalStaticData.CameraCount > 4)
            {
                GlobalStaticData.HIKCamera5.eventRun += HIKCamera5_eventRun;
            }
            if (GlobalStaticData.CameraCount > 5)
            {
                GlobalStaticData.HIKCamera6.eventRun += HIKCamera6_eventRun;
            }
            if (GlobalStaticData.CameraCount > 6)
            {
                GlobalStaticData.HIKCamera7.eventRun += HIKCamera7_eventRun;
            }
            if (GlobalStaticData.CameraCount > 7)
            {
                GlobalStaticData.HIKCamera8.eventRun += HIKCamera8_eventRun;
            }
            if (GlobalStaticData.CameraCount > 8)
            {
                GlobalStaticData.HIKCamera9.eventRun += HIKCamera9_eventRun;
            }
            if (GlobalStaticData.CameraCount > 9)
            {
                GlobalStaticData.HIKCamera10.eventRun += HIKCamera10_eventRun;
            }
            if (GlobalStaticData.CameraCount > 10)
            {
                GlobalStaticData.HIKCamera11.eventRun += HIKCamera11_eventRun;
            }
            if (GlobalStaticData.CameraCount > 11)
            {
                GlobalStaticData.HIKCamera12.eventRun += HIKCamera12_eventRun;
            }
            if (GlobalStaticData.CameraCount > 12)
            {
                GlobalStaticData.HIKCamera13.eventRun += HIKCamera13_eventRun;
            }
            if (GlobalStaticData.CameraCount > 13)
            {
                GlobalStaticData.HIKCamera14.eventRun += HIKCamera14_eventRun;
            }
            if (GlobalStaticData.CameraCount > 14)
            {
                GlobalStaticData.HIKCamera15.eventRun += HIKCamera15_eventRun;
            }
            if (GlobalStaticData.CameraCount > 15)
            {
                GlobalStaticData.HIKCamera16.eventRun += HIKCamera16_eventRun;
            }
            if (GlobalStaticData.CameraCount > 16)
            {
                GlobalStaticData.HIKCamera17.eventRun += HIKCamera17_eventRun;
            }
            if (GlobalStaticData.CameraCount > 17)
            {
                GlobalStaticData.HIKCamera18.eventRun += HIKCamera18_eventRun;
            }




            Task.Factory.StartNew(() => { MainLoopFunction(); }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(() => { LoopCalculateAndSendFunction(); }, TaskCreationOptions.LongRunning);

            Task.Factory.StartNew(() => { SendCoorMsg(); }, TaskCreationOptions.LongRunning);

        }



        private void groupADispYoloRoiEvent(YoloResult[] obj)
        {

            DisplayYoloRoi(hWindowControl1, obj[0]);
            DisplayYoloRoi(hWindowControl2, obj[1]);
            DisplayYoloRoi(hWindowControl3, obj[2]);

        }


        private void groupBDispYoloRoiEvent(YoloResult[] obj)
        {

            DisplayYoloRoi(hWindowControl5, obj[0]);
            DisplayYoloRoi(hWindowControl6, obj[1]);
            DisplayYoloRoi(hWindowControl7, obj[2]);

        }

        private void groupCDispYoloRoiEvent(YoloResult[] obj)
        {

            DisplayYoloRoi(hWindowControl9, obj[0]);
            DisplayYoloRoi(hWindowControl10, obj[1]);
            DisplayYoloRoi(hWindowControl11, obj[2]);

        }


        private void groupDDispYoloRoiEvent(YoloResult[] obj)
        {

            DisplayYoloRoi(hWindowControl13, obj[0]);
            DisplayYoloRoi(hWindowControl14, obj[1]);
            DisplayYoloRoi(hWindowControl15, obj[2]);
        }

        private void groupEDispYoloRoiEvent(YoloResult[] obj)
        {

            DisplayYoloRoi(hWindowControl17, obj[0]);
            DisplayYoloRoi(hWindowControl18, obj[1]);
            DisplayYoloRoi(hWindowControl19, obj[2]);
        }

        private void groupFDispYoloRoiEvent(YoloResult[] obj)
        {

            DisplayYoloRoi(hWindowControl21, obj[0]);
            DisplayYoloRoi(hWindowControl22, obj[1]);
            DisplayYoloRoi(hWindowControl23, obj[2]);
        }

        private void groupADispImageEvent(HObject image1, HObject image2, HObject image3)
        {
            if (image1 != null)
            {

                //GlobalStaticData.displayConvert.SetHalconScalingZoom(image1, hWindowControl1.HalconWindow);

                //hWindowControl1.HalconWindow.DispObj(image1);
                hWindowControl1.HobjectToHimage(image1);

               // GlobalStaticData.displayConvert.SetHalconScalingZoom(image1, hWindowControl4.HalconWindow);
                //hWindowControl4.HalconWindow.DispObj(image1);
                //hWindowControl4.HalconWindow.SetColor("red");
                hWindowControl4.HobjectToHimage(image1);

            }
            if (image2 != null)
            {

               // GlobalStaticData.displayConvert.SetHalconScalingZoom(image2, hWindowControl2.HalconWindow);
                // hWindowControl2.HalconWindow.DispObj(image2);
                hWindowControl2.HobjectToHimage(image2);
            }
            if (image3 != null)
            {

                //GlobalStaticData.displayConvert.SetHalconScalingZoom(image3, hWindowControl3.HalconWindow);
                // hWindowControl3.HalconWindow.DispObj(image3);
                hWindowControl3.HobjectToHimage(image3);
            }
        }

        private void groupBDispImageEvent(HObject image1, HObject image2, HObject image3)
        {
            if (image1 != null)
            {


                hWindowControl5.HobjectToHimage(image1);


                hWindowControl8.HobjectToHimage(image1);
                //hWindowControl8.HalconWindow.SetColor("red");
            }
            if (image2 != null)
            {


                hWindowControl6.HobjectToHimage(image2);
            }
            if (image3 != null)
            {


                hWindowControl7.HobjectToHimage(image3);
            }
        }

        private void groupCDispImageEvent(HObject image1, HObject image2, HObject image3)
        {
            if (image1 != null)
            {


                hWindowControl9.HobjectToHimage(image1);
            ;

                hWindowControl12.HobjectToHimage(image1);
                //hWindowControl12.HalconWindow.SetColor("red");
            }
            if (image2 != null)
            {


                hWindowControl10.HobjectToHimage(image2);
            }
            if (image3 != null)
            {


                hWindowControl11.HobjectToHimage(image3);
            }
        }


        private void groupDDispImageEvent(HObject image1, HObject image2, HObject image3)
        {
            if (image1 != null)
            {


                hWindowControl13.HobjectToHimage(image1);


                hWindowControl16.HobjectToHimage(image1);
               //hWindowControl15.HalconWindow.SetColor("red");
            }
            if (image2 != null)
            {


                hWindowControl14.HobjectToHimage(image2);
            }
            if (image3 != null)
            {


                hWindowControl15.HobjectToHimage(image3);
            }
        }

        private void groupEDispImageEvent(HObject image1, HObject image2, HObject image3)
        {
            if (image1 != null)
            {


                hWindowControl17.HobjectToHimage(image1);


                hWindowControl20.HobjectToHimage(image1);
                //hWindowControl15.HalconWindow.SetColor("red");
            }
            if (image2 != null)
            {


                hWindowControl18.HobjectToHimage(image2);
            }
            if (image3 != null)
            {


                hWindowControl19.HobjectToHimage(image3);
            }
        }

        private void groupFDispImageEvent(HObject image1, HObject image2, HObject image3)
        {
            if (image1 != null)
            {


                hWindowControl21.HobjectToHimage(image1);


                hWindowControl24.HobjectToHimage(image1);
                //hWindowControl15.HalconWindow.SetColor("red");
            }
            if (image2 != null)
            {


                hWindowControl22.HobjectToHimage(image2);
            }
            if (image3 != null)
            {


                hWindowControl23.HobjectToHimage(image3);
            }
        }



        private void DisplayYoloRoi(HWindow_Final hwindow, YoloResult yoloResult)
        {
            if (!TryGetRenderableHalconWindow(hwindow, out var control) || yoloResult == null)
                return;

            if (control.InvokeRequired)
            {
                try
                {
                    control.BeginInvoke(new Action(() => DisplayYoloRoi(hwindow, yoloResult)));
                }
                catch (InvalidOperationException) { }
                return;
            }

            if (!TryGetRenderableHalconWindow(hwindow, out control))
                return;

            int count = GetYoloRoiCount(yoloResult);
            if (count <= 0)
                return;

            for (int i = 0; i < count; i++)
            {
                HObject rectangle = null;
                try
                {
                    HOperatorSet.GenRectangle2(
                        out rectangle,
                        yoloResult._rows[i],
                        yoloResult._cols[i],
                        0,
                        yoloResult._height[i] / 2,
                        yoloResult._width[i] / 2);

                    hwindow.DispObj(rectangle);
                    control.HalconWindow.DispText(
                        yoloResult._score[i].D.ToString("f2"),
                        "image",
                        (yoloResult._rows[i] - yoloResult._height[i] - 20).D,
                        (yoloResult._cols[i] - (yoloResult._width[i] / 2)).D,
                        "green",
                        "box",
                        "false");
                }
                catch (HalconException) { }
                catch (InvalidOperationException) { }
                finally
                {
                    rectangle?.Dispose();
                }
            }
        }

        private bool TryGetRenderableHalconWindow(HWindow_Final hwindow, out HWindowControl control)
        {
            control = null;

            if (hwindow == null || hwindow.hWindowControl == null)
                return false;

            control = hwindow.hWindowControl;
            return !control.IsDisposed &&
                   control.IsHandleCreated &&
                   control.Width > 0 &&
                   control.Height > 0;
        }

        private int GetYoloRoiCount(YoloResult yoloResult)
        {
            if (yoloResult._rows == null ||
                yoloResult._cols == null ||
                yoloResult._width == null ||
                yoloResult._height == null ||
                yoloResult._score == null)
                return 0;

            return Math.Min(
                Math.Min(yoloResult._rows.Length, yoloResult._cols.Length),
                Math.Min(
                    Math.Min(yoloResult._width.Length, yoloResult._height.Length),
                    yoloResult._score.Length));
        }


        private FindCoorData robot1 = null;

        private bool TryBeginInvoke(Action action)
        {
            if (_isClosing || IsDisposed || !IsHandleCreated)
                return false;

            try
            {
                BeginInvoke(action);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private void SendCoorMsg()
        {
            while (!_isClosing)
            {

                RefreshCoorSendSelect(GlobalStaticData.SendRobotCoorRefresh);
                if (GlobalStaticData.SendRobotNum <= 2)
                {
                    //为发送X最小的
                    if (GlobalStaticData.SendDataState == 0)
                    {
                        _pointProcessor.SendCoorValue(GlobalStaticData.SendXOffset, _superSimpleTcp1, _superSimpleTcp3);
                    }
                    else if (GlobalStaticData.SendDataState == 1)//发送Y坐标最小的
                    {
                        _pointProcessor.SendCoorValueWorldYMinSort(GlobalStaticData.SendXOffset, _superSimpleTcp1, _superSimpleTcp3);
                    }
                    else//发送Height最大的
                    {
                        _pointProcessor.SendCoorValueHeightMaxSort(GlobalStaticData.SendXOffset, _superSimpleTcp1, _superSimpleTcp3);
                    }
                }
                else
                {
                    //为发送X最小的
                    if (GlobalStaticData.SendDataState == 0)
                    {
                        _pointProcessor.SendCoorValue3Robot(GlobalStaticData.SendXOffset, _superSimpleTcp1, _superSimpleTcp3, _superSimpleTcp5);
                    }
                    else if (GlobalStaticData.SendDataState == 1)//发送Y坐标最小的
                    {
                        _pointProcessor.SendCoorValueWorldYMinSort3Robot(GlobalStaticData.SendXOffset, _superSimpleTcp1, _superSimpleTcp3, _superSimpleTcp5);
                    }
                    else//发送Height最大的
                    {
                        _pointProcessor.SendCoorValueHeightMaxSort3Robot(GlobalStaticData.SendXOffset, _superSimpleTcp1, _superSimpleTcp3, _superSimpleTcp5);
                    }
                }
                    TryBeginInvoke(new Action(() =>
                    {
                        if (_isClosing || IsDisposed)
                            return;

                        lbl_SendData.Text = _pointProcessor.Robot1List.Count().ToString();
                        lbl_SendData2.Text = _pointProcessor.Robot2List.Count().ToString();
                        lbl_SendData3.Text = _pointProcessor.Robot3List.Count().ToString();

                    }
                    ));
                Thread.Sleep(50);
            }
        }

        private void ReceivedMsgEncoding(byte[] obj)
        {
            if (_isClosing || obj == null || obj.Length < 16)
                return;

            int encoderObj = BitConverter.ToInt32(obj, 0);
            TryBeginInvoke(new Action(() =>
            {
                if (_isClosing || IsDisposed)
                    return;

                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1 = BitConverter.ToInt32(obj, 4);
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2= BitConverter.ToInt32(obj, 8);
                GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData3= BitConverter.ToInt32(obj, 12);
            }));

            ProcessEncoderValue(encoderObj);
        }




        private void actionPrintConnectionLog1(int arg1, string arg2)
        {
            LogWinform($"prot:{arg1} " + arg2);



        }

        private void actionPrintConnectionLog2(int arg1, string arg2)
        {
            LogWinform($"prot:{arg1} " + arg2);


            this.BeginInvoke(new Action(() =>
            {
                GlobalStaticData.UpdataBingdingDisplayMsgq.ConnectStatus = arg2;
            }));


        }

        private void actionPrintConnectionLog3(int arg1, string arg2)
        {
            LogWinform($"prot:{arg1} " + arg2);


        }

        private void actionPrintConnectionLog4(int arg1, string arg2)
        {
            LogWinform($"prot:{arg1} " + arg2);



        }


        private void actionPrintConnectionLog5(int arg1, string arg2)
        {
            LogWinform($"prot:{arg1} " + arg2);
        }


        private static readonly Mutex Logmutex = new Mutex();
        private void LogWinform(string Info)
        {
            try
            {
                Logmutex.WaitOne();
                BeginInvoke(new Action(() =>
                {
                    if (listView1.Items.Count > 50)
                    {
                        listView1.Items.RemoveAt(listView1.Items.Count - 1);
                    }
                    string time = DateTime.Now.ToString("HH:mm:ss:fff");
                    ListViewItem lst = new ListViewItem(time);
                    lst.SubItems.Add(Info);
                    listView1.Items.Insert(0, lst);
                }));
                LoggerHelper._.Info(Info);
            }
            finally
            {
                Logmutex.ReleaseMutex();
            }

        }

        private int _CurrenDisp = 0;
        private const int MaxEncoderValue = 80000000;
        private const int EncoderResetThreshold = MaxEncoderValue - 1000;
        // int _EncoderTotal = 0;
        private int? _lastValue; // Last raw encoder value

        public void ProcessEncoderValue(int encoderValue)
        {
            if (!_lastValue.HasValue)
            {
                _lastValue = encoderValue;
                _CurrenDisp = 0;
                return;
            }

            int lastValue = _lastValue.Value;
            int currentDisp;

            // Wrap compensation: only treat a large high-to-low jump as encoder reset.
            if (encoderValue < lastValue && lastValue - encoderValue > EncoderResetThreshold)
            {
                currentDisp = encoderValue - lastValue + MaxEncoderValue;
            }
            else
            {
                currentDisp = encoderValue - lastValue;
            }

            _CurrenDisp = currentDisp;

            // Update last raw encoder value
            _lastValue = encoderValue;
            if (!IsDisposed && IsHandleCreated)
            {
                BeginInvoke(new Action(() =>
                {
                    if (!IsDisposed)
                    {
                        GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding = GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding + currentDisp;
                    }
                }));
            }
        }




        void RefreshCoorSendSelect(bool enable)
        {
            if (enable)
            {
                //XCommandPoind = GlobalStaticData.UpdataBingdingData.XCommandPoint;
                _pointProcessor._heightAligmentData1 = GlobalStaticData.HeightAligmentData1;
                _pointProcessor._heightAligmentData2 = GlobalStaticData.HeightAligmentData2;
                _pointProcessor._heightAligmentData3 = GlobalStaticData.HeightAligmentData3;
                _pointProcessor._heightAligmentData4 = GlobalStaticData.HeightAligmentData4;
                _pointProcessor._heightAligmentData5 = GlobalStaticData.HeightAligmentData5;
                _pointProcessor._heightAligmentData6 = GlobalStaticData.HeightAligmentData6;
                _pointProcessor._heightAligmentData7 = GlobalStaticData.HeightAligmentData7;
                _pointProcessor._heightAligmentData8 = GlobalStaticData.HeightAligmentData8;
                _pointProcessor._placeWebBeltSelectData1 = GlobalStaticData.placeWebBeltSelectData1;
                GlobalStaticData.SendRobotCoorRefresh = false;
            }

        }

        void RefreshCoorSelectConfig(bool enable)
        {
            if (enable)
            {
                _pointProcessor._xyTolerance = GlobalStaticData.UpdataBingdingData.BottleTolerance;
                _pointProcessor._minXThreshold = GlobalStaticData.UpdataBingdingData.XCommandPoint;
                _pointProcessor._minHeightThreshold = GlobalStaticData.UpdataBingdingData.MinHeight;
                _pointProcessor._maxHeightThreshold=GlobalStaticData.UpdataBingdingData.MaxHeight;
                _pointProcessor._separatorRegionRobot1 = GlobalStaticData.UpdataBingdingData.Robot1Threshold;
                _pointProcessor._separatorRegionRobot2 = GlobalStaticData.UpdataBingdingData.Robot2Threshold;
                _pointProcessor._safetyClearance =GlobalStaticData.UpdataBingdingData.SafetyClearance;
                GlobalStaticData.CoorSelectRefresh = false;
            }

        }



        private void LoopCalculateAndSendFunction()
        {

            while (!_isClosing)
            {
                RefreshCoorSelectConfig(GlobalStaticData.CoorSelectRefresh);

                _pointProcessor.ProcessLoop();
                TryBeginInvoke(new Action(() => {
                    if (_isClosing || IsDisposed)
                        return;

                    GlobalStaticData.UpdataBingdingDisplayMsgq.CacheNum= _pointProcessor.GetPointCount();
                    GlobalStaticData.UpdataBingdingDisplayMsgq.SendDataNum = _pointProcessor.GetSendAllNumber;
                }));

                Thread.Sleep(30);
            }
        }

        // 机器人任务池（并发可访问），用 ConcurrentDictionary 以方便查询/删除
        //private ConcurrentBag<FindCoorData> Robot1List = new ConcurrentBag<FindCoorData>();
        //private ConcurrentBag<FindCoorData> Robot2List = new ConcurrentBag<FindCoorData>();
        //private ConcurrentDictionary<long, FindCoorData> RobotAllList = new ConcurrentDictionary<long, FindCoorData>();
        //private long _autoKey = 0;
        //private readonly object robot1Lock = new object();
        //private readonly object robot2Lock = new object();
        //private readonly object robotAllLock = new object();






        //int _localEncode = 0;
        //HTuple _Width=new HTuple();
        //HTuple _Height=new HTuple();
        private bool _isProcessing = false;

        private void AddAndDisplayGroupResults(List<FindCoorData> results, HWindow_Final displayWindow)
        {
            if (results == null || results.Count == 0)
                return;

            foreach (var result in results)
            {
                _pointProcessor.AddPoint(CreateQueuedPoint(result));
                DisplayFindCoorResult(displayWindow, result);
            }
        }

        private FindCoorData CreateQueuedPoint(FindCoorData source)
        {
            return new FindCoorData()
            {
                encoding = source.encoding,
                WorldX = source.WorldX,
                WorldY = source.WorldY,
                WorldXScurren = source.WorldXScurren,
                Height = source.Height,
                Score = source.Score,
                Attribute = 1,
                SafeRegionMark = 0,
                placeCompensation = 0,
                MouthWidthMm = source.MouthWidthMm,
                MouthHeightMm = source.MouthHeightMm,
                MouthAverageDiameterMm = source.MouthAverageDiameterMm
            };
        }

        private void DisplayFindCoorResult(HWindow_Final hwindow, FindCoorData result)
        {
            TryBeginInvoke(new Action(() =>
            {
                if (_isClosing || !TryGetRenderableHalconWindow(hwindow, out var control))
                    return;

                HObject cross = null;
                try
                {
                    HOperatorSet.GenCrossContourXld(out cross, result.pixelRow, result.pixelCol, 20, 0);
                    hwindow.DispObj(cross);
                    control.HalconWindow.DispText(
                        $" x:{result.WorldX.D.ToString("f2")} y:{result.WorldY.D.ToString("f2")} z:{result.Height.D.ToString("f2")} Mouth:{result.MouthAverageDiameterMm.ToString("f2")}",
                        "image",
                        result.pixelRow.D,
                        result.pixelCol.D,
                        "green",
                        "box",
                        "false");
                }
                catch (HalconException) { }
                catch (InvalidOperationException) { }
                finally
                {
                    cross?.Dispose();
                }
            }));
        }

        private async void MainLoopFunction()
        {
            while (!_isClosing)
            {
                if (!pairingMgr.WaitForReadyBatch(out _))
                {
                    if (_isClosing)
                        break;

                    continue;
                }

                _isProcessing = true;
                if ( GlobalStaticData.allReady)
                {
                    _mainLoopWatch.Restart();
                   int encodings= GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding;


                    var groupAResultTask =  Task.Run(() => groupA.Process(encodings));
                    var groupBResultTask =  Task.Run(() => groupB.Process(encodings));
                    var groupCResultTask =  Task.Run(() => groupC.Process(encodings));
                    var groupDResultTask =  Task.Run(() => groupD.Process(encodings));
                    var groupEResultTask = Task.Run(() => groupE.Process(encodings));
                    var groupFResultTask = Task.Run(() => groupF.Process(encodings));
                    var taskAllResult = await Task.WhenAll(groupAResultTask, groupBResultTask, groupCResultTask, groupDResultTask,groupEResultTask,groupFResultTask);

                    var groupAResult = taskAllResult[0];
                    var groupBResult = taskAllResult[1];
                    var groupCResult = taskAllResult[2];
                    var groupDResult = taskAllResult[3];
                    var groupEResult = taskAllResult[4];
                    var groupFResult = taskAllResult[5];

                    AddAndDisplayGroupResults(groupAResult, hWindowControl4);
                    AddAndDisplayGroupResults(groupBResult, hWindowControl8);
                    AddAndDisplayGroupResults(groupCResult, hWindowControl12);
                    AddAndDisplayGroupResults(groupDResult, hWindowControl16);
                    AddAndDisplayGroupResults(groupEResult, hWindowControl20);
                    AddAndDisplayGroupResults(groupFResult, hWindowControl24);

                    _mainLoopWatch.Stop();
                    BeginInvoke(new Action(() => {
                    GlobalStaticData.UpdataBingdingDisplayMsgq.RunTime=Convert.ToInt32(_mainLoopWatch.ElapsedMilliseconds);
                    }));

                }

                GlobalStaticData._imageBuffer.Clear();
                _isProcessing = false;
            }
        }






        private readonly object _lock1=new object();
        private readonly object _lock2=new object();
        private readonly object _lock3=new object();

        private readonly object _lock4 = new object();
        private readonly object _lock5 = new object();
        private readonly object _lock6 = new object();

        private readonly object _lock7 = new object();
        private readonly object _lock8 = new object();
        private readonly object _lock9 = new object();

        private readonly object _lock10 = new object();
        private readonly object _lock11 = new object();
        private readonly object _lock12 = new object();

        private readonly object _lock13 = new object();
        private readonly object _lock14 = new object();
        private readonly object _lock15 = new object();

        private readonly object _lock16 = new object();
        private readonly object _lock17 = new object();
        private readonly object _lock18 = new object();

        private void HandleHikCameraRun(HObject image, int cameraIndex, object cameraLock)
        {
            if (_isClosing || _isProcessing || image == null)
                return;

            lock (cameraLock)
            {
                if (_isClosing || _isProcessing)
                    return;

                pairingMgr.SignalCamera(cameraIndex, image);

                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(image.Clone(), (cameraIndex + 1).ToString());
            }
        }

        private void HIKCamera1_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 0, _lock1);
        }

        private void HIKCamera2_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 1, _lock2);
        }

        private void HIKCamera3_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 2, _lock3);
        }

        private void HIKCamera4_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 3, _lock4);
        }


        private void HIKCamera5_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 4, _lock5);
        }

        private void HIKCamera6_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 5, _lock6);
        }


        private void HIKCamera7_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 6, _lock7);
        }

        private void HIKCamera8_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 7, _lock8);
        }

        private void HIKCamera9_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 8, _lock9);
        }

        private void HIKCamera10_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 9, _lock10);
        }

        private void HIKCamera11_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 10, _lock11);
        }

        private void HIKCamera12_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 11, _lock12);
        }

        private void HIKCamera13_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 12, _lock13);
        }

        private void HIKCamera14_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 13, _lock14);
        }

        private void HIKCamera15_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 14, _lock15);
        }


        private void HIKCamera16_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 15, _lock16);
        }

        private void HIKCamera17_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 16, _lock17);
        }
        private void HIKCamera18_eventRun(HObject Himage)
        {
            HandleHikCameraRun(Himage, 17, _lock18);
        }





        private void ContinuousSave(CancellationToken cancellationToken)
        {
            try
            {
                GlobalStaticData.HIKCamera1.TriggerCamera(7);
                GlobalStaticData.HIKCamera2.TriggerCamera(7);
                GlobalStaticData.HIKCamera3.TriggerCamera(7);
                GlobalStaticData.HIKCamera4.TriggerCamera(7);
                GlobalStaticData.HIKCamera5.TriggerCamera(7);
                GlobalStaticData.HIKCamera6.TriggerCamera(7);
                GlobalStaticData.HIKCamera7.TriggerCamera(7);
                GlobalStaticData.HIKCamera8.TriggerCamera(7);
                GlobalStaticData.HIKCamera9.TriggerCamera(7);
                GlobalStaticData.HIKCamera10.TriggerCamera(7);
                GlobalStaticData.HIKCamera11.TriggerCamera(7);
                GlobalStaticData.HIKCamera12.TriggerCamera(7);

                while (!cancellationToken.IsCancellationRequested)
                {
                    // 在这里执行你的循环逻辑（例如保存数据）
                    //  Console.WriteLine($"正在保存数据... {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                    GlobalStaticData.HIKCamera1.SoftwareTrigger();
                    GlobalStaticData.HIKCamera2.SoftwareTrigger();
                    GlobalStaticData.HIKCamera3.SoftwareTrigger();
                    GlobalStaticData.HIKCamera4.SoftwareTrigger();
                    GlobalStaticData.HIKCamera5.SoftwareTrigger();
                    GlobalStaticData.HIKCamera6.SoftwareTrigger();
                    GlobalStaticData.HIKCamera7.SoftwareTrigger();
                    GlobalStaticData.HIKCamera8.SoftwareTrigger();
                    GlobalStaticData.HIKCamera9.SoftwareTrigger();
                    GlobalStaticData.HIKCamera10.SoftwareTrigger();
                    GlobalStaticData.HIKCamera11.SoftwareTrigger();
                    GlobalStaticData.HIKCamera12.SoftwareTrigger();
                    // 模拟耗时操作（例如每1秒执行一次）
                    Thread.Sleep(500); // 可调整时间间隔
                }
            }
            catch (OperationCanceledException)
            {
                // 线程被正常取消，不做处理

                this.BeginInvoke(new Action(() => UIMessageTip.ShowWarning("线程已停止")));
            }
            catch (Exception ex)
            {
                // 其他异常处理
                this.BeginInvoke(new Action(() => UIMessageTip.ShowError($"发生错误: {ex.Message}")));
            }
        }


       // private CancellationTokenSource _cancellationTriggleSource;

        private void btn_start_Click(object sender, EventArgs e)
        {
          if(  btn_start.Text=="启动")
            {
                GlobalStaticData.allReady = true;
                GlobalStaticData.PositionRefresh = true;
                GlobalStaticData.CoorSelectRefresh = true;
                GlobalStaticData.SendRobotCoorRefresh = true;
               // _cancellationTriggleSource = new CancellationTokenSource();

                // 启动新线程（使用 Task.Run 避免阻塞 UI）
              //  Task.Run(() => ContinuousSave(_cancellationTriggleSource.Token), _cancellationTriggleSource.Token);
                btn_start.Text = "停止";
                btn_start.FillColor = Color.Red;
                btn_start.FillHoverColor = Color.LightPink;
                lgt_StartStaute.State = UILightState.Blink;
                LogWinform("启动程序");
            }
          else
            {
                GlobalStaticData.allReady = false;
                //_cancellationTriggleSource?.Cancel(); // 发送取消信号
                //_cancellationTriggleSource?.Dispose(); // 释放资源
                btn_start.FillColor = Color.FromArgb(80, 160, 255);
                btn_start.FillHoverColor = Color.FromArgb(115, 179, 255);
                lgt_StartStaute.State = UILightState.Off;
                btn_start.Text = "启动";

                LogWinform("停止程序");

            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {

            GlobalStaticData.allReady = false;
            //_cancellationTriggleSource?.Cancel(); // 发送取消信号
            //_cancellationTriggleSource?.Dispose(); // 释放资源
            btn_start.FillColor = Color.FromArgb(80, 160, 255);
            btn_start.FillHoverColor = Color.FromArgb(115, 179, 255);
            lgt_StartStaute.State = UILightState.Off;
            btn_start.Text = "启动";
            LogWinform("复位程序");
            Thread.Sleep(100);
            bool res = _pointProcessor.ClearData();
            if (res)
            {
                LogWinform("数据清除成功");
            }
            else
            {
                LogWinform("数据清除失败");
            }
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isClosing = true;
            GlobalStaticData.allReady = false;

            try
            {
                _superSimpleTcp2.ActionReceivedMsg -= ReceivedMsgEncoding;
            }
            catch { }

            try
            {
                pairingMgr?.Stop();
            }
            catch { }

            //_session1_1?.Dispose();
            //_session1_2?.Dispose();
            //_session1_3?.Dispose();
            //GlobalStaticData.onnx_session?.Dispose();

        }

        private void ck_AllImageSave_ValueChanged(object sender, bool value)
        {
            if (ck_AllImageSave.Active)
            {
                GlobalStaticData.WriteGlobalImage = true;
                LogWinform("图像保存开启");
            }
            else
            {
                GlobalStaticData.WriteGlobalImage = false;
                LogWinform("图像保存关闭");
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // 弹出确认对话框
            DialogResult result = MessageBox.Show("确定要清空所有内容吗？",
                "确认清空",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                listView1.Items.Clear();
            }
        }
    }
}
