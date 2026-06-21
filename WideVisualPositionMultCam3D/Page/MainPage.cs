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
        private void Page1_Load(object sender, EventArgs e)
        {

            // 1. 初始化控制器
            halconController1 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel4)
            {
                // 配置选项
               // ShowHandCursor = true,
               // EnableClickToRestore = true,
               // MaximizeOnSingleClick = false,  // 单击时最大化
               // MaximizeOnDoubleClick = true,  // 双击时也最大化
               // EnableDoubleClickMaximize = true
            };

            halconController1.Register(hWindowControl1);
            halconController1.Register(hWindowControl2);
            halconController1.Register(hWindowControl3);
            halconController1.Register(hWindowControl4);

            // 2. 注册事件监听
            //halconController.OnWindowClicked += HalconController_OnWindowClicked;
            //halconController.OnWindowDoubleClicked += HalconController_OnWindowDoubleClicked;
            //halconController.OnWindowMaximized += HalconController_OnWindowMaximized;
            //halconController.OnLayoutRestored += HalconController_OnLayoutRestored;

            // 3. 自动注册所有HWindowControl控件
            // halconController1.AutoRegisterWindowsFromTable();

            // 4. 保存原始布局
            // halconController1.SaveCurrentLayout();


            halconController2 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel8);
            halconController2.Register(hWindowControl5);
            halconController2.Register(hWindowControl6);
            halconController2.Register(hWindowControl7);
            halconController2.Register(hWindowControl8);


            halconController3 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel9);

            halconController3.Register(hWindowControl9);
            halconController3.Register(hWindowControl10);
            halconController3.Register(hWindowControl11);
            halconController3.Register(hWindowControl12);

            halconController4 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel7);
            halconController4.Register(hWindowControl13);
            halconController4.Register(hWindowControl14);
            halconController4.Register(hWindowControl15);
            halconController4.Register(hWindowControl16);


            halconController5 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel12);
            halconController5.Register(hWindowControl17);
            halconController5.Register(hWindowControl18);
            halconController5.Register(hWindowControl19);
            halconController5.Register(hWindowControl20);


            halconController6 = new TableLayoutHalconDispZoomHWindow_final(tableLayoutPanel13);
            halconController6.Register(hWindowControl21);
            halconController6.Register(hWindowControl22);
            halconController6.Register(hWindowControl23);
            halconController6.Register(hWindowControl24);


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

            _superSimpleTcp2 = new SuperSimpleTcpHelper(IP2, port2, false);
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

          
            bool resCalib1 = GlobalStaticData.HalconAlgorithmFunction.Load_calibration_data(GlobalStaticData.WriteCalibrationPath + @"\Calibration\calibration_data1.cal", out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_CalibDataID, out GlobalStaticData.CameraGroupConfig1.worldTransformerData.hv_CamParamData0, out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_CamPose0, out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_CamParamData1, out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_CamPose1, out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_CamParamData2, out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_CamPose2, out GlobalStaticData.CameraGroupConfig1.worldTransformerData.hv_World2CamMat0, out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_InvertToCamMat0, out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_InvertToCamMat1, out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_InvertToCamMat2, out GlobalStaticData.CameraGroupConfig1.worldTransformerData.hv_PlanePose, out GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_CameraSetupModel, out GlobalStaticData.CameraGroupConfig1.hv_StereoModelIDGroup);
            if (!resCalib1)
            {
                MessageBox.Show("相机组1标定参数加载失败");
            }

            bool resCalib2 = GlobalStaticData.HalconAlgorithmFunction.Load_calibration_data(GlobalStaticData.WriteCalibrationPath + @"\Calibration\calibration_data2.cal", out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CalibDataID, out GlobalStaticData.CameraGroupConfig2.worldTransformerData.hv_CamParamData0, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamPose0, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamParamData1, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamPose1, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamParamData2, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamPose2, out GlobalStaticData.CameraGroupConfig2.worldTransformerData.hv_World2CamMat0, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_InvertToCamMat0, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_InvertToCamMat1, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_InvertToCamMat2, out GlobalStaticData.CameraGroupConfig2.worldTransformerData.hv_PlanePose, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CameraSetupModel, out GlobalStaticData.CameraGroupConfig2.hv_StereoModelIDGroup);
            if (!resCalib2)
            {
                MessageBox.Show("相机组2标定参数加载失败");
            }


            bool resCalib3 = GlobalStaticData.HalconAlgorithmFunction.Load_calibration_data(GlobalStaticData.WriteCalibrationPath + @"\Calibration\calibration_data3.cal", out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_CalibDataID, out GlobalStaticData.CameraGroupConfig3.worldTransformerData.hv_CamParamData0, out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_CamPose0, out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_CamParamData1, out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_CamPose1, out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_CamParamData2, out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_CamPose2, out GlobalStaticData.CameraGroupConfig3.worldTransformerData.hv_World2CamMat0, out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_InvertToCamMat0, out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_InvertToCamMat1, out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_InvertToCamMat2, out GlobalStaticData.CameraGroupConfig3.worldTransformerData.hv_PlanePose, out GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_CameraSetupModel, out GlobalStaticData.CameraGroupConfig3.hv_StereoModelIDGroup);
            if (!resCalib3)
            {
                MessageBox.Show("相机组3标定参数加载失败");
            }

            bool resCalib4 = GlobalStaticData.HalconAlgorithmFunction.Load_calibration_data(GlobalStaticData.WriteCalibrationPath + @"\Calibration\calibration_data4.cal", out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_CalibDataID, out GlobalStaticData.CameraGroupConfig4.worldTransformerData.hv_CamParamData0, out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_CamPose0, out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_CamParamData1, out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_CamPose1, out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_CamParamData2, out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_CamPose2, out GlobalStaticData.CameraGroupConfig4.worldTransformerData.hv_World2CamMat0, out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_InvertToCamMat0, out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_InvertToCamMat1, out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_InvertToCamMat2, out GlobalStaticData.CameraGroupConfig4.worldTransformerData.hv_PlanePose, out GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_CameraSetupModel, out GlobalStaticData.CameraGroupConfig4.hv_StereoModelIDGroup);
            if (!resCalib4)
            {
                MessageBox.Show("相机组4标定参数加载失败");
            }

            bool resCalib5 = GlobalStaticData.HalconAlgorithmFunction.Load_calibration_data(GlobalStaticData.WriteCalibrationPath + @"\Calibration\calibration_data5.cal", out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_CalibDataID, out GlobalStaticData.CameraGroupConfig5.worldTransformerData.hv_CamParamData0, out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_CamPose0, out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_CamParamData1, out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_CamPose1, out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_CamParamData2, out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_CamPose2, out GlobalStaticData.CameraGroupConfig5.worldTransformerData.hv_World2CamMat0, out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_InvertToCamMat0, out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_InvertToCamMat1, out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_InvertToCamMat2, out GlobalStaticData.CameraGroupConfig5.worldTransformerData.hv_PlanePose, out GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_CameraSetupModel, out GlobalStaticData.CameraGroupConfig5.hv_StereoModelIDGroup);
            if (!resCalib5)
            {
                MessageBox.Show("相机组5标定参数加载失败");
            }

            bool resCalib6 = GlobalStaticData.HalconAlgorithmFunction.Load_calibration_data(GlobalStaticData.WriteCalibrationPath + @"\Calibration\calibration_data6.cal", out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_CalibDataID, out GlobalStaticData.CameraGroupConfig6.worldTransformerData.hv_CamParamData0, out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_CamPose0, out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_CamParamData1, out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_CamPose1, out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_CamParamData2, out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_CamPose2, out GlobalStaticData.CameraGroupConfig6.worldTransformerData.hv_World2CamMat0, out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_InvertToCamMat0, out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_InvertToCamMat1, out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_InvertToCamMat2, out GlobalStaticData.CameraGroupConfig6.worldTransformerData.hv_PlanePose, out GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_CameraSetupModel, out GlobalStaticData.CameraGroupConfig6.hv_StereoModelIDGroup);
            if (!resCalib6)
            {
                MessageBox.Show("相机组6标定参数加载失败");
            }

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

            dispYoloRoiA(hWindowControl1, obj[0]);
            dispYoloRoiA(hWindowControl2, obj[1]);
            dispYoloRoiA(hWindowControl3, obj[2]);

        }


        private void groupBDispYoloRoiEvent(YoloResult[] obj)
        {

            dispYoloRoiB(hWindowControl5, obj[0]);
            dispYoloRoiB(hWindowControl6, obj[1]);
            dispYoloRoiB(hWindowControl7, obj[2]);

        }

        private void groupCDispYoloRoiEvent(YoloResult[] obj)
        {

            dispYoloRoiC(hWindowControl9, obj[0]);
            dispYoloRoiC(hWindowControl10, obj[1]);
            dispYoloRoiC(hWindowControl11, obj[2]);

        }


        private void groupDDispYoloRoiEvent(YoloResult[] obj)
        {

            dispYoloRoiD(hWindowControl13, obj[0]);
            dispYoloRoiD(hWindowControl14, obj[1]);
            dispYoloRoiD(hWindowControl15, obj[2]);
        }

        private void groupEDispYoloRoiEvent(YoloResult[] obj)
        {

            dispYoloRoiD(hWindowControl17, obj[0]);
            dispYoloRoiD(hWindowControl18, obj[1]);
            dispYoloRoiD(hWindowControl19, obj[2]);
        }

        private void groupFDispYoloRoiEvent(YoloResult[] obj)
        {

            dispYoloRoiD(hWindowControl21, obj[0]);
            dispYoloRoiD(hWindowControl22, obj[1]);
            dispYoloRoiD(hWindowControl23, obj[2]);
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



        private void dispYoloRoiA(HWindow_Final hwindow,YoloResult yoloResult)
        {
            // 先判断控件是否有效
            if (hwindow == null || hwindow.hWindowControl == null ||
                hwindow.hWindowControl.Width <= 0 || hwindow.hWindowControl.Height <= 0 ||
                hwindow.hWindowControl.IsDisposed)
                return;


            if (yoloResult._rows.Length > 0)
                {
                    for (int i = 0; i < yoloResult._rows.Length; i++)
                    {
                        int local_i = i;

                        HOperatorSet.GenRectangle2(out HObject rectangle3, yoloResult._rows[local_i], yoloResult._cols[local_i], 0, yoloResult._height[local_i] / 2, yoloResult._width[local_i] / 2);
                        //hwindow.HalconWindow.SetColor("red");
                        //hwindow.HalconWindow.SetDraw("margin");
                        hwindow.DispObj(rectangle3.Clone());
                        hwindow.hWindowControl.HalconWindow.DispText(yoloResult._score[local_i].D.ToString("f2"), "image", (yoloResult._rows[local_i] - yoloResult._height-20).D, (yoloResult._cols[local_i] - (yoloResult._width[local_i] / 2)).D, "green", "box", "false");


                    }
                }
         
        }


        private void dispYoloRoiB(HWindow_Final hwindow, YoloResult yoloResult)
        {
            // 先判断控件是否有效
            if (hwindow == null || hwindow.hWindowControl == null ||
                hwindow.hWindowControl.Width <= 0 || hwindow.hWindowControl.Height <= 0 ||
                hwindow.hWindowControl.IsDisposed)
                return;

            if (yoloResult._rows.Length > 0)
            {
                for (int i = 0; i < yoloResult._rows.Length; i++)
                {
                    int local_i = i;

                    HOperatorSet.GenRectangle2(out HObject rectangle3, yoloResult._rows[local_i], yoloResult._cols[local_i], 0, yoloResult._height[local_i] / 2, yoloResult._width[local_i] / 2);
                    //hwindow.HalconWindow.SetColor("red");
                    //hwindow.HalconWindow.SetDraw("margin");
                    hwindow.DispObj(rectangle3.Clone());
                    hwindow.hWindowControl.HalconWindow.DispText(yoloResult._score[local_i].D.ToString("f2"), "image", (yoloResult._rows[local_i] - yoloResult._height - 20).D, (yoloResult._cols[local_i] - (yoloResult._width[local_i] / 2)).D, "green", "box", "false");


                }
            }

        }

        private void dispYoloRoiC(HWindow_Final hwindow, YoloResult yoloResult)
        {
            // 先判断控件是否有效
            if (hwindow == null || hwindow.hWindowControl == null ||
                hwindow.hWindowControl.Width <= 0 || hwindow.hWindowControl.Height <= 0 ||
                hwindow.hWindowControl.IsDisposed)
                return;

            if (yoloResult._rows.Length > 0)
            {
             
                for (int i = 0; i < yoloResult._rows.Length; i++)
                {
                    int local_i = i;

                    HOperatorSet.GenRectangle2(out HObject rectangle3, yoloResult._rows[local_i], yoloResult._cols[local_i], 0, yoloResult._height[local_i] / 2, yoloResult._width[local_i] / 2);
                    //hwindow.HalconWindow.SetColor("red");
                    //hwindow.HalconWindow.SetDraw("margin");
                    hwindow.DispObj(rectangle3.Clone());
                    hwindow.hWindowControl.HalconWindow.DispText(yoloResult._score[local_i].D.ToString("f2"), "image", (yoloResult._rows[local_i] - yoloResult._height - 20).D, (yoloResult._cols[local_i] - (yoloResult._width[local_i] / 2)).D, "green", "box", "false");

                }

               
            }

        }


        private void dispYoloRoiD(HWindow_Final hwindow, YoloResult yoloResult)
        {
            // 先判断控件是否有效
            if (hwindow == null || hwindow.hWindowControl == null ||
                hwindow.hWindowControl.Width <= 0 || hwindow.hWindowControl.Height <= 0 ||
                hwindow.hWindowControl.IsDisposed)
                return;

            if (yoloResult._rows.Length > 0)
            {
                for (int i = 0; i < yoloResult._rows.Length; i++)
                {
                    int local_i = i;

                    HOperatorSet.GenRectangle2(out HObject rectangle3, yoloResult._rows[local_i], yoloResult._cols[local_i], 0, yoloResult._height[local_i] / 2, yoloResult._width[local_i] / 2);
                    //hwindow.HalconWindow.SetColor("red");
                    //hwindow.HalconWindow.SetDraw("margin");
                    
                    hwindow.DispObj(rectangle3.Clone());
                    hwindow.hWindowControl.HalconWindow.DispText(yoloResult._score[local_i].D.ToString("f2"), "image", (yoloResult._rows[local_i] - yoloResult._height - 20).D, (yoloResult._cols[local_i] - (yoloResult._width[local_i] / 2)).D, "green", "box", "false");


                }
            }

        }


        private FindCoorData robot1 = null;
        private void SendCoorMsg()
        {
            while (true)
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
                    BeginInvoke(new Action(() =>
                    {
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
            int encoderObj = BitConverter.ToInt32(obj, 0);
            if (!IsDisposed && IsHandleCreated)
            {
                BeginInvoke(new Action(() =>
                {
                    if (!IsDisposed)
                    {
                        GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData1 = BitConverter.ToInt32(obj, 4);
                        GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData2= BitConverter.ToInt32(obj, 8);
                        GlobalStaticData.UpdataBingdingDisplayMsgq.RobotUseData3= BitConverter.ToInt32(obj, 12);
                    }
                }));
            }
     
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
        // int _EncoderTotal = 0;
        private int? _lastValue; // 用于存储上一次的编码器值

        public void ProcessEncoderValue(int encoderValue)
        {

            if (_lastValue.HasValue)
            {
                // 计算当前值与上次值之间的差值
                int delta = Math.Abs(encoderValue - _lastValue.Value);

                // 如果差值是负数，说明编码器已经归零
                if (delta > 79999000)
                {
                    // 补偿800000
                    _CurrenDisp = encoderValue - _lastValue.Value + MaxEncoderValue;
                }
                else
                {
                    // 正常累加
                    _CurrenDisp = encoderValue - _lastValue.Value;
                }
            }
            else
            {
                _CurrenDisp = encoderValue;
            }

            // 更新上次的编码器值
            _lastValue = encoderValue;
            if (!IsDisposed && IsHandleCreated)
            {
                BeginInvoke(new Action(() =>
                {
                    if (!IsDisposed)
                    {
                        GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding = GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding + _CurrenDisp;
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
            
            while (true)
            {
                RefreshCoorSelectConfig(GlobalStaticData.CoorSelectRefresh);

                _pointProcessor.ProcessLoop();
                this.BeginInvoke(new Action(() => {
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
        private async void MainLoopFunction()
        {
            while (true)
            {
                pairingMgr.PairReadyHandle.Wait(); // 等图像全部完成
                pairingMgr.PairReadyHandle.Reset();
                _isProcessing = true;
                if ( GlobalStaticData.allReady)
                {
                    Stopwatch watch=new Stopwatch();
                    watch.Start();
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

                    for (int i = 0; i < groupAResult.Count; i++)
                    {
                        int local_i = i;

                        //这里对应的机械手坐标和图像坐标的X\Y是反的
                        //  double score = CalculateProportionalValue(rowArray[local_i], 0, _Height.D);
                        _pointProcessor.AddPoint(new FindCoorData() { encoding = groupAResult[local_i].encoding, WorldX = groupAResult[local_i].WorldX, WorldY = groupAResult[local_i].WorldY, WorldXScurren = groupAResult[local_i].WorldXScurren, Height = groupAResult[local_i].Height, Score = groupAResult[local_i].Score,Attribute=1, SafeRegionMark = 0, placeCompensation = 0, MouthWidthMm = groupAResult[local_i].MouthWidthMm, MouthHeightMm = groupAResult[local_i].MouthHeightMm, MouthAverageDiameterMm = groupAResult[local_i].MouthAverageDiameterMm });
                        // GlobalStaticData.blockingCollectiontest.Add(new FindCoorData() { encoding = _localEncode, WorldX = hv_y_mm[local_i] +cam1_X_Offset, WorldY = hv_x_mm[local_i] +cam1_Y_Offset,WorldXScurren = _localEncode - hv_y_mm[local_i], Height = hv_z_mm[local_i] +cam1_Z_Offset,Score=score,SafeRegionMark=0 });
                        HOperatorSet.GenCrossContourXld(out HObject cross_result, groupAResult[local_i].pixelRow, groupAResult[local_i].pixelCol, 20, 0);
                        hWindowControl4.DispObj(cross_result.Clone());
                        hWindowControl4.hWindowControl.HalconWindow.DispText($" x:{groupAResult[local_i].WorldX.D.ToString("f2")} y:{groupAResult[local_i].WorldY.D.ToString("f2")} z:{groupAResult[local_i].Height.D.ToString("f2")} Mouth:{groupAResult[local_i].MouthAverageDiameterMm.ToString("f2")}", "image", groupAResult[local_i].pixelRow.D, groupAResult[local_i].pixelCol.D, "green", "box", "false");
                    }

                  
                    for (int i = 0; i < groupBResult.Count; i++)
                    {
                        int local_i = i;

                        //这里对应的机械手坐标和图像坐标的X\Y是反的
                        //  double score = CalculateProportionalValue(rowArray[local_i], 0, _Height.D);
                        _pointProcessor.AddPoint(new FindCoorData() { encoding = groupBResult[local_i].encoding, WorldX = groupBResult[local_i].WorldX, WorldY = groupBResult[local_i].WorldY, WorldXScurren = groupBResult[local_i].WorldXScurren, Height = groupBResult[local_i].Height, Score = groupBResult[local_i].Score, Attribute = 1, SafeRegionMark = 0, placeCompensation = 0, MouthWidthMm = groupBResult[local_i].MouthWidthMm, MouthHeightMm = groupBResult[local_i].MouthHeightMm, MouthAverageDiameterMm = groupBResult[local_i].MouthAverageDiameterMm });
                        // GlobalStaticData.blockingCollectiontest.Add(new FindCoorData() { encoding = _localEncode, WorldX = hv_y_mm[local_i] +cam1_X_Offset, WorldY = hv_x_mm[local_i] +cam1_Y_Offset,WorldXScurren = _localEncode - hv_y_mm[local_i], Height = hv_z_mm[local_i] +cam1_Z_Offset,Score=score,SafeRegionMark=0 });
                        HOperatorSet.GenCrossContourXld(out HObject cross_result, groupBResult[local_i].pixelRow, groupBResult[local_i].pixelCol, 20, 0);
                        hWindowControl8.DispObj(cross_result.Clone());
                        hWindowControl8.hWindowControl.HalconWindow. DispText($" x:{groupBResult[local_i].WorldX.D.ToString("f2")} y:{groupBResult[local_i].WorldY.D.ToString("f2")} z:{groupBResult[local_i].Height.D.ToString("f2")} Mouth:{groupBResult[local_i].MouthAverageDiameterMm.ToString("f2")}", "image", groupBResult[local_i].pixelRow.D, groupBResult[local_i].pixelCol.D, "green", "box", "false");
                    }


                   
                    for (int i = 0; i < groupCResult.Count; i++)
                    {
                        int local_i = i;

                        //这里对应的机械手坐标和图像坐标的X\Y是反的
                        //  double score = CalculateProportionalValue(rowArray[local_i], 0, _Height.D);
                        _pointProcessor.AddPoint(new FindCoorData() { encoding = groupCResult[local_i].encoding, WorldX = groupCResult[local_i].WorldX, WorldY = groupCResult[local_i].WorldY, WorldXScurren = groupCResult[local_i].WorldXScurren, Height = groupCResult[local_i].Height, Score = groupCResult[local_i].Score, Attribute = 1, SafeRegionMark = 0, placeCompensation = 0, MouthWidthMm = groupCResult[local_i].MouthWidthMm, MouthHeightMm = groupCResult[local_i].MouthHeightMm, MouthAverageDiameterMm = groupCResult[local_i].MouthAverageDiameterMm });
                        // GlobalStaticData.blockingCollectiontest.Add(new FindCoorData() { encoding = _localEncode, WorldX = hv_y_mm[local_i] +cam1_X_Offset, WorldY = hv_x_mm[local_i] +cam1_Y_Offset,WorldXScurren = _localEncode - hv_y_mm[local_i], Height = hv_z_mm[local_i] +cam1_Z_Offset,Score=score,SafeRegionMark=0 });
                        HOperatorSet.GenCrossContourXld(out HObject cross_result, groupCResult[local_i].pixelRow, groupCResult[local_i].pixelCol, 20, 0);
                        hWindowControl12.DispObj(cross_result.Clone());
                        hWindowControl12.hWindowControl.HalconWindow.DispText($" x:{groupCResult[local_i].WorldX.D.ToString("f2")} y:{groupCResult[local_i].WorldY.D.ToString("f2")} z:{groupCResult[local_i].Height.D.ToString("f2")} Mouth:{groupCResult[local_i].MouthAverageDiameterMm.ToString("f2")}", "image", groupCResult[local_i].pixelRow.D, groupCResult[local_i].pixelCol.D, "green", "box", "false");
                    }

                 
                    for (int i = 0; i < groupDResult.Count; i++)
                    {
                        int local_i = i;

                        //这里对应的机械手坐标和图像坐标的X\Y是反的
                        //  double score = CalculateProportionalValue(rowArray[local_i], 0, _Height.D);
                        _pointProcessor.AddPoint(new FindCoorData() { encoding = groupDResult[local_i].encoding, WorldX = groupDResult[local_i].WorldX, WorldY = groupDResult[local_i].WorldY, WorldXScurren = groupDResult[local_i].WorldXScurren, Height = groupDResult[local_i].Height, Score = groupDResult[local_i].Score, Attribute = 1, SafeRegionMark = 0, placeCompensation = 0, MouthWidthMm = groupDResult[local_i].MouthWidthMm, MouthHeightMm = groupDResult[local_i].MouthHeightMm, MouthAverageDiameterMm = groupDResult[local_i].MouthAverageDiameterMm });
                        // GlobalStaticData.blockingCollectiontest.Add(new FindCoorData() { encoding = _localEncode, WorldX = hv_y_mm[local_i] +cam1_X_Offset, WorldY = hv_x_mm[local_i] +cam1_Y_Offset,WorldXScurren = _localEncode - hv_y_mm[local_i], Height = hv_z_mm[local_i] +cam1_Z_Offset,Score=score,SafeRegionMark=0 });
                        HOperatorSet.GenCrossContourXld(out HObject cross_result, groupDResult[local_i].pixelRow, groupDResult[local_i].pixelCol, 20, 0);
                        hWindowControl16.DispObj(cross_result.Clone());
                        hWindowControl16.hWindowControl.HalconWindow.DispText($" x:{groupDResult[local_i].WorldX.D.ToString("f2")} y:{groupDResult[local_i].WorldY.D.ToString("f2")} z:{groupDResult[local_i].Height.D.ToString("f2")} Mouth:{groupDResult[local_i].MouthAverageDiameterMm.ToString("f2")}", "image", groupDResult[local_i].pixelRow.D, groupDResult[local_i].pixelCol.D, "green", "box", "false");
                    }

                    for (int i = 0; i < groupEResult.Count; i++)
                    {
                        int local_i = i;

                        //这里对应的机械手坐标和图像坐标的X\Y是反的
                        //  double score = CalculateProportionalValue(rowArray[local_i], 0, _Height.D);
                        _pointProcessor.AddPoint(new FindCoorData() { encoding = groupEResult[local_i].encoding, WorldX = groupEResult[local_i].WorldX, WorldY = groupEResult[local_i].WorldY, WorldXScurren = groupEResult[local_i].WorldXScurren, Height = groupEResult[local_i].Height, Score = groupEResult[local_i].Score, Attribute = 1, SafeRegionMark = 0, placeCompensation = 0, MouthWidthMm = groupEResult[local_i].MouthWidthMm, MouthHeightMm = groupEResult[local_i].MouthHeightMm, MouthAverageDiameterMm = groupEResult[local_i].MouthAverageDiameterMm });
                        // GlobalStaticData.blockingCollectiontest.Add(new FindCoorData() { encoding = _localEncode, WorldX = hv_y_mm[local_i] +cam1_X_Offset, WorldY = hv_x_mm[local_i] +cam1_Y_Offset,WorldXScurren = _localEncode - hv_y_mm[local_i], Height = hv_z_mm[local_i] +cam1_Z_Offset,Score=score,SafeRegionMark=0 });
                        HOperatorSet.GenCrossContourXld(out HObject cross_result, groupEResult[local_i].pixelRow, groupEResult[local_i].pixelCol, 20, 0);
                        hWindowControl20.DispObj(cross_result.Clone());
                        hWindowControl20.hWindowControl.HalconWindow.DispText($" x:{groupEResult[local_i].WorldX.D.ToString("f2")} y:{groupEResult[local_i].WorldY.D.ToString("f2")} z:{groupEResult[local_i].Height.D.ToString("f2")} Mouth:{groupEResult[local_i].MouthAverageDiameterMm.ToString("f2")}", "image", groupEResult[local_i].pixelRow.D, groupEResult[local_i].pixelCol.D, "green", "box", "false");
                    }

                    for (int i = 0; i < groupFResult.Count; i++)
                    {
                        int local_i = i;

                        //这里对应的机械手坐标和图像坐标的X\Y是反的
                        //  double score = CalculateProportionalValue(rowArray[local_i], 0, _Height.D);
                        _pointProcessor.AddPoint(new FindCoorData() { encoding = groupFResult[local_i].encoding, WorldX = groupFResult[local_i].WorldX, WorldY = groupFResult[local_i].WorldY, WorldXScurren = groupFResult[local_i].WorldXScurren, Height = groupFResult[local_i].Height, Score = groupFResult[local_i].Score, Attribute = 1, SafeRegionMark = 0, placeCompensation = 0, MouthWidthMm = groupFResult[local_i].MouthWidthMm, MouthHeightMm = groupFResult[local_i].MouthHeightMm, MouthAverageDiameterMm = groupFResult[local_i].MouthAverageDiameterMm });
                        // GlobalStaticData.blockingCollectiontest.Add(new FindCoorData() { encoding = _localEncode, WorldX = hv_y_mm[local_i] +cam1_X_Offset, WorldY = hv_x_mm[local_i] +cam1_Y_Offset,WorldXScurren = _localEncode - hv_y_mm[local_i], Height = hv_z_mm[local_i] +cam1_Z_Offset,Score=score,SafeRegionMark=0 });
                        HOperatorSet.GenCrossContourXld(out HObject cross_result, groupFResult[local_i].pixelRow, groupFResult[local_i].pixelCol, 20, 0);
                        hWindowControl24.DispObj(cross_result.Clone());
                        hWindowControl24.hWindowControl.HalconWindow.DispText($" x:{groupFResult[local_i].WorldX.D.ToString("f2")} y:{groupFResult[local_i].WorldY.D.ToString("f2")} z:{groupFResult[local_i].Height.D.ToString("f2")} Mouth:{groupFResult[local_i].MouthAverageDiameterMm.ToString("f2")}", "image", groupFResult[local_i].pixelRow.D, groupFResult[local_i].pixelCol.D, "green", "box", "false");
                    }


                    watch.Stop();
                    BeginInvoke(new Action(() => { 
                    GlobalStaticData.UpdataBingdingDisplayMsgq.RunTime=Convert.ToInt32(watch.ElapsedMilliseconds);
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
        private void HIKCamera1_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock1)
                {
                    GlobalStaticData._imageBuffer[0] = Himage.Clone();
                if(GlobalStaticData.WriteGlobalImage)
                 WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "1");
                }

            pairingMgr.SignalCamera(0);
     
        }

        private void HIKCamera2_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            //if (GlobalStaticData.CalibrationMode1)
            //{
            //    lock (_lock2)
            //    {
            //        GlobalStaticData.Cam2DisplayEvent?.Invoke(Himage.Clone());
            //    }
            //}
            //else
            //{
            lock (_lock2)
                {
                    GlobalStaticData._imageBuffer[1] = Himage.Clone();
                //GlobalStaticData.Cam2DisplayEvent?.Invoke(Himage.Clone());
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "2");
            }
            // autoResetEvents[1].Set(); // 通知有新图像
            pairingMgr.SignalCamera(1);
            //}
           // Himage.Dispose();
        }

        private void HIKCamera3_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock3)
                {
                    GlobalStaticData._imageBuffer[2] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "3");
            }
            pairingMgr.SignalCamera(2);
        
        }

        private void HIKCamera4_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock4)
            {
                GlobalStaticData._imageBuffer[3] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "4");
            }
            pairingMgr.SignalCamera(3);
        }


        private void HIKCamera5_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock5)
            {
                GlobalStaticData._imageBuffer[4] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "5");
            }
            pairingMgr.SignalCamera(4);
        }

        private void HIKCamera6_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock6)
            {
                GlobalStaticData._imageBuffer[5] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "6");
            }
            pairingMgr.SignalCamera(5);
        }


        private void HIKCamera7_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock7)
            {
                GlobalStaticData._imageBuffer[6] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "7");
            }
            pairingMgr.SignalCamera(6);
        }

        private void HIKCamera8_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock8)
            {
                GlobalStaticData._imageBuffer[7] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "8");
            }
            pairingMgr.SignalCamera(7);
        }

        private void HIKCamera9_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock9)
            {
                GlobalStaticData._imageBuffer[8] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "9");
            }
            pairingMgr.SignalCamera(8);
        }

        private void HIKCamera10_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock10)
            {
                GlobalStaticData._imageBuffer[9] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "10");
            }
            pairingMgr.SignalCamera(9);
        }

        private void HIKCamera11_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock11)
            {
                GlobalStaticData._imageBuffer[10] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "11");
            }
            pairingMgr.SignalCamera(10);
        }

        private void HIKCamera12_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock12)
            {
                GlobalStaticData._imageBuffer[11] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "12");
            }
            pairingMgr.SignalCamera(11);
        }

        private void HIKCamera13_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock13)
            {
                GlobalStaticData._imageBuffer[12] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "13");
            }
            pairingMgr.SignalCamera(12);
        }

        private void HIKCamera14_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock14)
            {
                GlobalStaticData._imageBuffer[13] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "14");
            }
            pairingMgr.SignalCamera(13);
        }

        private void HIKCamera15_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock15)
            {
                GlobalStaticData._imageBuffer[14] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "15");
            }
            pairingMgr.SignalCamera(14);
        }


        private void HIKCamera16_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock16)
            {
                GlobalStaticData._imageBuffer[15] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "16");
            }
            pairingMgr.SignalCamera(15);
        }

        private void HIKCamera17_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock17)
            {
                GlobalStaticData._imageBuffer[16] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "17");
            }
            pairingMgr.SignalCamera(16);
        }
        private void HIKCamera18_eventRun(HObject Himage)
        {
            if (_isProcessing)
                return;
            lock (_lock18)
            {
                GlobalStaticData._imageBuffer[17] = Himage.Clone();
                if (GlobalStaticData.WriteGlobalImage)
                    WriteImgaesHelper.WriteGlobalAllImages(Himage.Clone(), "18");
            }
            pairingMgr.SignalCamera(17);
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




