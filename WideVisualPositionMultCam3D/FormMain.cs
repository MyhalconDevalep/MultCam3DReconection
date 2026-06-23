using HalconDotNet;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.LoginHelperFile;
using WideVisualPositionMultCam3D.Models;
using WideVisualPositionMultCam3D.Page;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D
{
    public partial class FormMain : UIHeaderAsideMainFooterFrame
    {
        private const string PublicPickConfigSection = "PublicPickConfig";
        private const string PublicPositionConfigSection = "PublicPositionConfig";

        public CameraInitData CameraInitData1 { get; set; }
        public CameraInitData CameraInitData2 { get; set; }
        public CameraInitData CameraInitData3 { get; set; }
        public CameraInitData CameraInitData4 { get; set; }
        public CameraInitData CameraInitData5 { get; set; }

        public CameraInitData CameraInitData6 { get; set; }

        public CameraInitData CameraInitData7 { get; set; }
        public CameraInitData CameraInitData8 { get; set; }
        public CameraInitData CameraInitData9 { get; set; }

        public CameraInitData CameraInitData10 { get; set; }
        public CameraInitData CameraInitData11 { get; set; }
        public CameraInitData CameraInitData12 { get; set; }


        public CameraInitData CameraInitData13 { get; set; }
        public CameraInitData CameraInitData14 { get; set; }
        public CameraInitData CameraInitData15 { get; set; }


        public CameraInitData CameraInitData16 { get; set; }
        public CameraInitData CameraInitData17 { get; set; }
        public CameraInitData CameraInitData18 { get; set; }
        public FormMain()
        {

            InitializeComponent();
            LoginSessionMonitor.SessionTimeout += LoginSessionMonitor_SessionTimeout;
            CameraInitData1 = new CameraInitData();
            CameraInitData2 = new CameraInitData();
            CameraInitData3 = new CameraInitData();
            CameraInitData4 = new CameraInitData();
            CameraInitData5 = new CameraInitData();
            CameraInitData6 = new CameraInitData();
            CameraInitData7 = new CameraInitData();
            CameraInitData8 = new CameraInitData();
            CameraInitData9 = new CameraInitData();
            CameraInitData10 = new CameraInitData();
            CameraInitData11 = new CameraInitData();
            CameraInitData12 = new CameraInitData();

            CameraInitData13 = new CameraInitData();
            CameraInitData14 = new CameraInitData();
            CameraInitData15 = new CameraInitData();

            CameraInitData16 = new CameraInitData();
            CameraInitData17 = new CameraInitData();
            CameraInitData18 = new CameraInitData();

            GlobalStaticData.UpdataBingdingData = new Models.UpdataBingdingData();
            GlobalStaticData.UpdataBingdingDisplayMsgq = new Models.UpdataBingdingDisplayMsg();
            GlobalStaticData.HeightAligmentData1 = new Models.HeightAligmentData();
            GlobalStaticData.HeightAligmentData2 = new Models.HeightAligmentData();
            GlobalStaticData.HeightAligmentData3 = new Models.HeightAligmentData();
            GlobalStaticData.HeightAligmentData4 = new Models.HeightAligmentData();
            GlobalStaticData.HeightAligmentData5 = new Models.HeightAligmentData();
            GlobalStaticData.HeightAligmentData6 = new Models.HeightAligmentData();
            GlobalStaticData.HeightAligmentData7 = new Models.HeightAligmentData();
            GlobalStaticData.HeightAligmentData8 = new Models.HeightAligmentData();
            GlobalStaticData.placeWebBeltSelectData1=new PlaceWebBeltSelectData();
            GlobalStaticData.CameraGroupConfig1 = new Models.CameraGroupConfig();
            GlobalStaticData.CameraGroupConfig1.GroupId = 0;
            GlobalStaticData.CameraGroupConfig1.Cam0.CamId = 0;
            GlobalStaticData.CameraGroupConfig1.Cam1.CamId = 1;
            GlobalStaticData.CameraGroupConfig1.Cam2.CamId = 2;
            GlobalStaticData.CameraGroupConfig2 = new Models.CameraGroupConfig();
            GlobalStaticData.CameraGroupConfig2.GroupId = 1;
            GlobalStaticData.CameraGroupConfig2.Cam0.CamId = 3;
            GlobalStaticData.CameraGroupConfig2.Cam1.CamId = 4;
            GlobalStaticData.CameraGroupConfig2.Cam2.CamId = 5;
            GlobalStaticData.CameraGroupConfig3 = new Models.CameraGroupConfig();
            GlobalStaticData.CameraGroupConfig3.GroupId = 2;
            GlobalStaticData.CameraGroupConfig3.Cam0.CamId = 6;
            GlobalStaticData.CameraGroupConfig3.Cam1.CamId = 7;
            GlobalStaticData.CameraGroupConfig3.Cam2.CamId = 8;
            GlobalStaticData.CameraGroupConfig4 = new Models.CameraGroupConfig();
            GlobalStaticData.CameraGroupConfig4.GroupId = 3;
            GlobalStaticData.CameraGroupConfig4.Cam0.CamId = 9;
            GlobalStaticData.CameraGroupConfig4.Cam1.CamId = 10;
            GlobalStaticData.CameraGroupConfig4.Cam2.CamId = 11;

            GlobalStaticData.CameraGroupConfig5 = new Models.CameraGroupConfig();
            GlobalStaticData.CameraGroupConfig5.GroupId = 4;
            GlobalStaticData.CameraGroupConfig5.Cam0.CamId = 12;
            GlobalStaticData.CameraGroupConfig5.Cam1.CamId = 13;
            GlobalStaticData.CameraGroupConfig5.Cam2.CamId = 14;

            GlobalStaticData.CameraGroupConfig6 = new Models.CameraGroupConfig();
            GlobalStaticData.CameraGroupConfig6.GroupId = 5;
            GlobalStaticData.CameraGroupConfig6.Cam0.CamId = 15;
            GlobalStaticData.CameraGroupConfig6.Cam1.CamId = 16;
            GlobalStaticData.CameraGroupConfig6.Cam2.CamId = 17;


            GlobalStaticData.OperateConfig = new OperateConfig();
            GlobalStaticData.OperateConfig.KeyValueFileReader("Config//config.txt");
            //string cam1Number=string.Empty;
            //string cam2Number = string.Empty;
            //string cam3Number = string.Empty;
            //float cam1Gain = 1;
            //float cam2Gain = 1;
            //float cam3Gain = 1;
            //float cam1ExpsureTime = 100;
            //float cam2ExpsureTime = 100;
            //float cam3ExpsureTime = 100;
            
            
            try
            {
                try
                {
                    GlobalStaticData.CameraCount = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("AllOtherParams", "CamNums"));
                    GlobalStaticData.SendDataState = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("AllOtherParams", "SendDataState"));
                    GlobalStaticData.SendXOffset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("AllOtherParams", "SendXOffset"));
                    GlobalStaticData.SendRobotNum = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("AllOtherParams", "RobotNum"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"读取AllOtherParams参数异常：{ex.Message}");
                  
                }
               
                InitializeConfiguredCameras();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"读取相机初始化参数失败：{ex.Message}");
            }

            try
            {
                LoadAllCameraGroupRuntimeConfigs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机组参数读取失败:{ex.Message}");
            }


            try
            {
                GlobalStaticData.UpdataBingdingData.BottleTolerance = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "BottleTolerance"));
                GlobalStaticData.UpdataBingdingData.XCommandPoint = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "XCommandPoint"));
                GlobalStaticData.UpdataBingdingData.SafetyClearance = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "SafetyClearance"));
                GlobalStaticData.UpdataBingdingData.MinHeight = ReadIntConfig(PublicPickConfigSection, 0, "MinHeight", "MinHeight\u200B");
                GlobalStaticData.UpdataBingdingData.MaxHeight = ReadIntConfig(PublicPickConfigSection, 0, "MaxHeight", "MaxHeight\u200B");
                GlobalStaticData.UpdataBingdingData.Robot1Threshold = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "Robot1Threshold"));
                GlobalStaticData.UpdataBingdingData.Robot2Threshold = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "Robot2Threshold"));
            }
            catch (Exception ex)
            {

                MessageBox.Show($"初始化坐标筛选公共参数失败：{ex.Message}");
            }


            try
            {
                LoadHeightAlignmentConfigs();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"高度对齐参数读取失败:{ex.Message}");
            }

            try
            {
                GlobalStaticData.placeWebBeltSelectData1.IsEnable= bool.Parse(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "IsEnable"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold1Down = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationThreshold1Down"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold1Up = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationThreshold1Up"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri1 = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationAttri1"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold2Down = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationThreshold2Down"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold2Up = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationThreshold2Up"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri2 = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationAttri2"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold3Down = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationThreshold3Down"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold3Up = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationThreshold3Up"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri3 = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationAttri3"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold4Down = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationThreshold4Down"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold4Up = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationThreshold4Up"));
                GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri4 = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "SegmentationAttri4"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"网带属性配置参数读取失败:{ex.Message}");
            }


            lbl_encoding.DataBindings.Add("Text", GlobalStaticData.UpdataBingdingDisplayMsgq, "Encoding");
            lbl_ConnectStaut.DataBindings.Add("Text", GlobalStaticData.UpdataBingdingDisplayMsgq, "ConnectStatus");
            lbl_userPower.DataBindings.Add("Text", GlobalStaticData.UpdataBingdingDisplayMsgq, "UserPower");
            lbl_RunTime.DataBindings.Add("Text", GlobalStaticData.UpdataBingdingDisplayMsgq, "RunTime");
            lbl_cacheNum.DataBindings.Add("Text", GlobalStaticData.UpdataBingdingDisplayMsgq, "CacheNum");
            Aside.TabControl = MainTabControl;
            AddPage(new MainPage(), 1001);

            AddPage(new RuningParamSet(), 1004);
            // TreeNode parent = Aside.CreateNode("控件", 61451, 24, pageIndex);
            Aside.CreateNode("主页显示", 61461, 24, 1001);
            TreeNode node = Aside.CreateNode("相机标定", 62025, 24, 2000);
            Aside.CreateChildNode(node, AddPage(new Calibration1(), Guid.NewGuid()));
            Aside.CreateChildNode(node, AddPage(new Calibration2(), Guid.NewGuid()));
            Aside.CreateChildNode(node, AddPage(new Calibration3(), Guid.NewGuid()));
            Aside.CreateChildNode(node, AddPage(new Calibration4(), Guid.NewGuid()));
            Aside.CreateChildNode(node, AddPage(new Calibration5(), Guid.NewGuid()));
            Aside.CreateChildNode(node, AddPage(new Calibration6(), Guid.NewGuid()));
            TreeNode CalibrationNode = Aside.CreateNode("标定采集", 61896, 24, 3000);
            Aside.CreateChildNode(CalibrationNode, AddPage(new CalibrationAcq1(), Guid.NewGuid()));
            Aside.CreateChildNode(CalibrationNode, AddPage(new CalibrationAcq2(), Guid.NewGuid()));
            Aside.CreateChildNode(CalibrationNode, AddPage(new CalibrationAcq3(), Guid.NewGuid()));
            Aside.CreateChildNode(CalibrationNode, AddPage(new CalibrationAcq4(), Guid.NewGuid()));
            Aside.CreateChildNode(CalibrationNode, AddPage(new CalibrationAcq5(), Guid.NewGuid()));
            Aside.CreateChildNode(CalibrationNode, AddPage(new CalibrationAcq6(), Guid.NewGuid()));
            TreeNode ConfigSet = Aside.CreateNode("参数设置", 61459, 24, 4000);
            Aside.CreateChildNode(ConfigSet, AddPage(new PublicRuningParams(), Guid.NewGuid()));
            Aside.CreateChildNode(ConfigSet, AddPage(new RobotRegionParams(), Guid.NewGuid()));
            Aside.CreateChildNode(ConfigSet, AddPage(new RuningParamSet(), Guid.NewGuid()));
            Aside.CreateChildNode(ConfigSet, AddPage(new RuningParamSet2(), Guid.NewGuid()));
            Aside.CreateChildNode(ConfigSet, AddPage(new RuningParamSet3(), Guid.NewGuid()));

            TreeNode UserConfigSet = Aside.CreateNode("用户配置", 62142, 24, 5000);
            Aside.CreateChildNode(UserConfigSet, AddPage(new HeightAlignment(), Guid.NewGuid()));
            Aside.CreateChildNode(UserConfigSet, AddPage(new HeightAlignment2(), Guid.NewGuid()));
            Aside.CreateChildNode(UserConfigSet, AddPage(new PlacementAttributeParams(), Guid.NewGuid()));


            //LoggerHelper._.Info("Right图像在信号为0时触发拍照");
            //LoggerHelper._.Warn("Right图像在信号为0时触发拍照");
            // 显示第1个界面
            Aside.SelectFirst();

       


        }

        private void LoginSessionMonitor_SessionTimeout()
        {
            this.Invoke(new Action(() =>
            {
                //MessageBox.Show("5分钟无操作，已自动退出登录");
                GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower = 0; 
            }));
        }

        private void InitializeConfiguredCameras()
        {
            for (int cameraIndex = 1; cameraIndex <= GlobalStaticData.CameraCount && cameraIndex <= 18; cameraIndex++)
            {
                CameraInitData initData = GetCameraInitData(cameraIndex);
                string section = $"Camera{cameraIndex}";
                initData.Number = GlobalStaticData.OperateConfig.GetValue(section, "Number");
                initData.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue(section, "Gain"));
                initData.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue(section, "CamExposureTime"));

                HIKCameraSDK camera = new HIKCameraSDK(initData.Number, 0, 0);
                camera.SetGain(initData.Gain);
                camera.SetExposureTime(initData.ExpsureTime);
                SetHikCamera(cameraIndex, camera);
            }
        }

        private CameraInitData GetCameraInitData(int cameraIndex)
        {
            switch (cameraIndex)
            {
                case 1: return CameraInitData1;
                case 2: return CameraInitData2;
                case 3: return CameraInitData3;
                case 4: return CameraInitData4;
                case 5: return CameraInitData5;
                case 6: return CameraInitData6;
                case 7: return CameraInitData7;
                case 8: return CameraInitData8;
                case 9: return CameraInitData9;
                case 10: return CameraInitData10;
                case 11: return CameraInitData11;
                case 12: return CameraInitData12;
                case 13: return CameraInitData13;
                case 14: return CameraInitData14;
                case 15: return CameraInitData15;
                case 16: return CameraInitData16;
                case 17: return CameraInitData17;
                case 18: return CameraInitData18;
                default: throw new ArgumentOutOfRangeException(nameof(cameraIndex), cameraIndex, "相机编号超出范围");
            }
        }

        private void SetHikCamera(int cameraIndex, HIKCameraSDK camera)
        {
            switch (cameraIndex)
            {
                case 1: GlobalStaticData.HIKCamera1 = camera; break;
                case 2: GlobalStaticData.HIKCamera2 = camera; break;
                case 3: GlobalStaticData.HIKCamera3 = camera; break;
                case 4: GlobalStaticData.HIKCamera4 = camera; break;
                case 5: GlobalStaticData.HIKCamera5 = camera; break;
                case 6: GlobalStaticData.HIKCamera6 = camera; break;
                case 7: GlobalStaticData.HIKCamera7 = camera; break;
                case 8: GlobalStaticData.HIKCamera8 = camera; break;
                case 9: GlobalStaticData.HIKCamera9 = camera; break;
                case 10: GlobalStaticData.HIKCamera10 = camera; break;
                case 11: GlobalStaticData.HIKCamera11 = camera; break;
                case 12: GlobalStaticData.HIKCamera12 = camera; break;
                case 13: GlobalStaticData.HIKCamera13 = camera; break;
                case 14: GlobalStaticData.HIKCamera14 = camera; break;
                case 15: GlobalStaticData.HIKCamera15 = camera; break;
                case 16: GlobalStaticData.HIKCamera16 = camera; break;
                case 17: GlobalStaticData.HIKCamera17 = camera; break;
                case 18: GlobalStaticData.HIKCamera18 = camera; break;
                default: throw new ArgumentOutOfRangeException(nameof(cameraIndex), cameraIndex, "相机编号超出范围");
            }
        }

        private void LoadAllCameraGroupRuntimeConfigs()
        {
            float confThreshold = float.Parse(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "conf_Score"));
            float nmsThreshold = float.Parse(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "nms_threshold"));
            double positionTolerance = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "PositioningTolerance"));
            double xyTolerance = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "XYTolerance"));
            double zTolerance = ReadDoubleConfig(PublicPositionConfigSection, 0, "ZToleranceEx", "ZToleranceEx\u200B\u200B");
            double boardHeight = ReadDoubleConfig(PublicPositionConfigSection, 0, "CalibrationBoardHeight", "CalibrationBoardHeight\u200B");

            CameraGroupConfig[] cameraGroupConfigs = GetCameraGroupConfigs();
            for (int i = 0; i < cameraGroupConfigs.Length; i++)
            {
                ApplyPublicCameraGroupConfig(cameraGroupConfigs[i], confThreshold, nmsThreshold, positionTolerance, xyTolerance, zTolerance, boardHeight);
                LoadPositionOffsetConfig(cameraGroupConfigs[i], $"Cam{i + 1}PositionConfig");
            }
        }

        private CameraGroupConfig[] GetCameraGroupConfigs()
        {
            return new[]
            {
                GlobalStaticData.CameraGroupConfig1,
                GlobalStaticData.CameraGroupConfig2,
                GlobalStaticData.CameraGroupConfig3,
                GlobalStaticData.CameraGroupConfig4,
                GlobalStaticData.CameraGroupConfig5,
                GlobalStaticData.CameraGroupConfig6
            };
        }

        private void ApplyPublicCameraGroupConfig(
            CameraGroupConfig cameraGroupConfig,
            float confThreshold,
            float nmsThreshold,
            double positionTolerance,
            double xyTolerance,
            double zTolerance,
            double boardHeight)
        {
            cameraGroupConfig.Cam0.YoloInferData.conf_threshold = confThreshold;
            cameraGroupConfig.Cam0.YoloInferData.nms_threshold = nmsThreshold;
            cameraGroupConfig.Cam1.YoloInferData.conf_threshold = confThreshold;
            cameraGroupConfig.Cam1.YoloInferData.nms_threshold = nmsThreshold;
            cameraGroupConfig.Cam2.YoloInferData.conf_threshold = confThreshold;
            cameraGroupConfig.Cam2.YoloInferData.nms_threshold = nmsThreshold;
            cameraGroupConfig.findCoorPairsData.PositionTolerance = positionTolerance;
            cameraGroupConfig.findCoorPairsData.hv_XYTolerance = xyTolerance;
            cameraGroupConfig.findCoorPairsData.hv_ZTolerance = zTolerance;
            cameraGroupConfig.worldTransformerData.BoardHeight = boardHeight;
        }

        private void LoadPositionOffsetConfig(CameraGroupConfig cameraGroupConfig, string section)
        {
            cameraGroupConfig.worldTransformerData.X_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue(section, "XOffset"));
            cameraGroupConfig.worldTransformerData.Y_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue(section, "YOffset"));
            cameraGroupConfig.worldTransformerData.Z_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue(section, "ZOffset"));
            cameraGroupConfig.worldTransformerData.Rz_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue(section, "RzOffset"));
        }

        private void LoadHeightAlignmentConfigs()
        {
            HeightAligmentData[] heightAligmentDatas = GetHeightAlignmentDatas();
            for (int i = 0; i < heightAligmentDatas.Length; i++)
            {
                LoadHeightAlignmentConfig(heightAligmentDatas[i], $"Bottle{i + 1}AlignmentCompensation");
            }
        }

        private HeightAligmentData[] GetHeightAlignmentDatas()
        {
            return new[]
            {
                GlobalStaticData.HeightAligmentData1,
                GlobalStaticData.HeightAligmentData2,
                GlobalStaticData.HeightAligmentData3,
                GlobalStaticData.HeightAligmentData4,
                GlobalStaticData.HeightAligmentData5,
                GlobalStaticData.HeightAligmentData6,
                GlobalStaticData.HeightAligmentData7,
                GlobalStaticData.HeightAligmentData8
            };
        }

        private void LoadHeightAlignmentConfig(HeightAligmentData data, string section)
        {
            data.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue(section, "IsEnable"));
            data.UpCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue(section, "UpCompensation"));
            data.DownCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue(section, "DownCompensation"));
            data.BaseHeight = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue(section, "BaseHeight​"));
            data.PlaceAttr = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue(section, "PlaceAttr"));
            data.PlaceHeightCompeensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue(section, "PlaceCompensation"));
            LoadMouthSizeConfig(data, section);
        }

        private void LoadMouthSizeConfig(HeightAligmentData data, string section)
        {
            data.MouthMinMm = ReadDoubleConfig(section, "MouthMinMm", 0);
            data.MouthMaxMm = ReadDoubleConfig(section, "MouthMaxMm", 9999);
        }

        private double ReadDoubleConfig(string section, string key, double defaultValue)
        {
            string value = GlobalStaticData.OperateConfig.GetValue(section, key);
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            double result;
            return double.TryParse(value, out result) ? result : defaultValue;
        }

        private double ReadDoubleConfig(string section, double defaultValue, params string[] keys)
        {
            string value = ReadConfigValue(section, keys);
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            double result;
            return double.TryParse(value, out result) ? result : defaultValue;
        }

        private int ReadIntConfig(string section, int defaultValue, params string[] keys)
        {
            string value = ReadConfigValue(section, keys);
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            int result;
            return int.TryParse(value, out result) ? result : defaultValue;
        }

        private string ReadConfigValue(string section, params string[] keys)
        {
            foreach (string key in keys)
            {
                string value = GlobalStaticData.OperateConfig.GetValue(section, key);
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
            }

            return string.Empty;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要退出系统吗？", "温馨提示：", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {

                // System.Environment.Exit(0); // 或者 e.Cancel = false;
                //this.Close();
            }
            else
            {
                e.Cancel = true; // 取消关闭事件
            }
        }
        LoginForm loginForm = null;
        private void uiAvatar1_Click(object sender, EventArgs e)
        {
            if (loginForm == null || loginForm.IsDisposed)
            {
                loginForm = new LoginForm();
                loginForm.Show();
            }
            else
            {

                loginForm.Activate();
            }
        }
    }
}
