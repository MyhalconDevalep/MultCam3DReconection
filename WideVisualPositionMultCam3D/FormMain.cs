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
               
                if(GlobalStaticData.CameraCount>0)
                {
                    CameraInitData1.Number = GlobalStaticData.OperateConfig.GetValue("Camera1", "Number");
                    CameraInitData1.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera1", "Gain"));
                    CameraInitData1.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera1", "CamExposureTime"));
                    GlobalStaticData.HIKCamera1 = new HIKCameraSDK(CameraInitData1.Number, 0, 0);
                    GlobalStaticData.HIKCamera1.SetGain(CameraInitData1.Gain);
                    GlobalStaticData.HIKCamera1.SetExposureTime(CameraInitData1.ExpsureTime);

                }
                if (GlobalStaticData.CameraCount > 1)
                {
                    CameraInitData2.Number = GlobalStaticData.OperateConfig.GetValue("Camera2", "Number");
                    CameraInitData2.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera2", "Gain"));
                    CameraInitData2.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera2", "CamExposureTime"));
                    GlobalStaticData.HIKCamera2 = new HIKCameraSDK(CameraInitData2.Number, 0, 0);
                    GlobalStaticData.HIKCamera2.SetGain(CameraInitData2.Gain);
                    GlobalStaticData.HIKCamera2.SetExposureTime(CameraInitData2.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 2)
                {
                    CameraInitData3.Number = GlobalStaticData.OperateConfig.GetValue("Camera3", "Number");
                    CameraInitData3.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera3", "Gain"));
                    CameraInitData3.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera3", "CamExposureTime"));
                    GlobalStaticData.HIKCamera3 = new HIKCameraSDK(CameraInitData3.Number, 0, 0);
                    GlobalStaticData.HIKCamera3.SetGain(CameraInitData3.Gain);
                    GlobalStaticData.HIKCamera3.SetExposureTime(CameraInitData3.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 3)
                {
                    CameraInitData4.Number = GlobalStaticData.OperateConfig.GetValue("Camera4", "Number");
                    CameraInitData4.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera4", "Gain"));
                    CameraInitData4.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera4", "CamExposureTime"));
                    GlobalStaticData.HIKCamera4 = new HIKCameraSDK(CameraInitData4.Number, 0, 0);
                    GlobalStaticData.HIKCamera4.SetGain(CameraInitData4.Gain);
                    GlobalStaticData.HIKCamera4.SetExposureTime(CameraInitData4.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 4)
                {
                    CameraInitData5.Number = GlobalStaticData.OperateConfig.GetValue("Camera5", "Number");
                    CameraInitData5.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera5", "Gain"));
                    CameraInitData5.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera5", "CamExposureTime"));
                    GlobalStaticData.HIKCamera5 = new HIKCameraSDK(CameraInitData5.Number, 0, 0);
                    GlobalStaticData.HIKCamera5.SetGain(CameraInitData5.Gain);
                    GlobalStaticData.HIKCamera5.SetExposureTime(CameraInitData5.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 5)
                {
                    CameraInitData6.Number = GlobalStaticData.OperateConfig.GetValue("Camera6", "Number");
                    CameraInitData6.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera6", "Gain"));
                    CameraInitData6.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera6", "CamExposureTime"));
                    GlobalStaticData.HIKCamera6 = new HIKCameraSDK(CameraInitData6.Number, 0, 0);
                    GlobalStaticData.HIKCamera6.SetGain(CameraInitData6.Gain);
                    GlobalStaticData.HIKCamera6.SetExposureTime(CameraInitData6.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 6)
                {
                    CameraInitData7.Number = GlobalStaticData.OperateConfig.GetValue("Camera7", "Number");
                    CameraInitData7.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera7", "Gain"));
                    CameraInitData7.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera7", "CamExposureTime"));
                    GlobalStaticData.HIKCamera7 = new HIKCameraSDK(CameraInitData7.Number, 0, 0);
                    GlobalStaticData.HIKCamera7.SetGain(CameraInitData7.Gain);
                    GlobalStaticData.HIKCamera7.SetExposureTime(CameraInitData7.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 7)
                {
                    CameraInitData8.Number = GlobalStaticData.OperateConfig.GetValue("Camera8", "Number");
                    CameraInitData8.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera8", "Gain"));
                    CameraInitData8.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera8", "CamExposureTime"));
                    GlobalStaticData.HIKCamera8 = new HIKCameraSDK(CameraInitData8.Number, 0, 0);
                    GlobalStaticData.HIKCamera8.SetGain(CameraInitData8.Gain);
                    GlobalStaticData.HIKCamera8.SetExposureTime(CameraInitData8.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 8)
                {
                    CameraInitData9.Number = GlobalStaticData.OperateConfig.GetValue("Camera9", "Number");
                    CameraInitData9.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera9", "Gain"));
                    CameraInitData9.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera9", "CamExposureTime"));
                    GlobalStaticData.HIKCamera9 = new HIKCameraSDK(CameraInitData9.Number, 0, 0);
                    GlobalStaticData.HIKCamera9.SetGain(CameraInitData9.Gain);
                    GlobalStaticData.HIKCamera9.SetExposureTime(CameraInitData9.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 9)
                {
                    CameraInitData10.Number = GlobalStaticData.OperateConfig.GetValue("Camera10", "Number");
                    CameraInitData10.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera10", "Gain"));
                    CameraInitData10.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera10", "CamExposureTime"));
                    GlobalStaticData.HIKCamera10 = new HIKCameraSDK(CameraInitData10.Number, 0, 0);
                    GlobalStaticData.HIKCamera10.SetGain(CameraInitData10.Gain);
                    GlobalStaticData.HIKCamera10.SetExposureTime(CameraInitData10.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 10)
                {
                    CameraInitData11.Number = GlobalStaticData.OperateConfig.GetValue("Camera11", "Number");
                    CameraInitData11.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera11", "Gain"));
                    CameraInitData11.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera11", "CamExposureTime"));

                    GlobalStaticData.HIKCamera11 = new HIKCameraSDK(CameraInitData11.Number, 0, 0);
                    GlobalStaticData.HIKCamera11.SetGain(CameraInitData11.Gain);
                    GlobalStaticData.HIKCamera11.SetExposureTime(CameraInitData11.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 11)
                {
                    CameraInitData12.Number = GlobalStaticData.OperateConfig.GetValue("Camera12", "Number");
                    CameraInitData12.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera12", "Gain"));
                    CameraInitData12.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera12", "CamExposureTime"));
                    GlobalStaticData.HIKCamera12 = new HIKCameraSDK(CameraInitData12.Number, 0, 0);
                    GlobalStaticData.HIKCamera12.SetGain(CameraInitData12.Gain);
                    GlobalStaticData.HIKCamera12.SetExposureTime(CameraInitData12.ExpsureTime);
                }

                if (GlobalStaticData.CameraCount > 12)
                {
                    CameraInitData12.Number = GlobalStaticData.OperateConfig.GetValue("Camera13", "Number");
                    CameraInitData12.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera13", "Gain"));
                    CameraInitData12.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera13", "CamExposureTime"));
                    GlobalStaticData.HIKCamera12 = new HIKCameraSDK(CameraInitData12.Number, 0, 0);
                    GlobalStaticData.HIKCamera12.SetGain(CameraInitData12.Gain);
                    GlobalStaticData.HIKCamera12.SetExposureTime(CameraInitData12.ExpsureTime);
                }


               


                if (GlobalStaticData.CameraCount > 13)
                {
                    CameraInitData14.Number = GlobalStaticData.OperateConfig.GetValue("Camera14", "Number");
                    CameraInitData14.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera14", "Gain"));
                    CameraInitData14.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera14", "CamExposureTime"));
                    GlobalStaticData.HIKCamera14 = new HIKCameraSDK(CameraInitData14.Number, 0, 0);
                    GlobalStaticData.HIKCamera14.SetGain(CameraInitData14.Gain);
                    GlobalStaticData.HIKCamera14.SetExposureTime(CameraInitData14.ExpsureTime);
                }


                if (GlobalStaticData.CameraCount > 14)
                {
                    CameraInitData15.Number = GlobalStaticData.OperateConfig.GetValue("Camera15", "Number");
                    CameraInitData15.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera15", "Gain"));
                    CameraInitData15.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera15", "CamExposureTime"));
                    GlobalStaticData.HIKCamera15 = new HIKCameraSDK(CameraInitData15.Number, 0, 0);
                    GlobalStaticData.HIKCamera15.SetGain(CameraInitData15.Gain);
                    GlobalStaticData.HIKCamera15.SetExposureTime(CameraInitData15.ExpsureTime);
                }

                if (GlobalStaticData.CameraCount > 15)
                {
                    CameraInitData16.Number = GlobalStaticData.OperateConfig.GetValue("Camera16", "Number");
                    CameraInitData16.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera16", "Gain"));
                    CameraInitData16.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera16", "CamExposureTime"));
                    GlobalStaticData.HIKCamera16 = new HIKCameraSDK(CameraInitData16.Number, 0, 0);
                    GlobalStaticData.HIKCamera16.SetGain(CameraInitData16.Gain);
                    GlobalStaticData.HIKCamera16.SetExposureTime(CameraInitData16.ExpsureTime);
                }
                if (GlobalStaticData.CameraCount > 16)
                {
                    CameraInitData17.Number = GlobalStaticData.OperateConfig.GetValue("Camera17", "Number");
                    CameraInitData17.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera17", "Gain"));
                    CameraInitData17.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera17", "CamExposureTime"));
                    GlobalStaticData.HIKCamera17 = new HIKCameraSDK(CameraInitData17.Number, 0, 0);
                    GlobalStaticData.HIKCamera17.SetGain(CameraInitData17.Gain);
                    GlobalStaticData.HIKCamera17.SetExposureTime(CameraInitData17.ExpsureTime);
                }

                if (GlobalStaticData.CameraCount > 17)
                {
                    CameraInitData18.Number = GlobalStaticData.OperateConfig.GetValue("Camera18", "Number");
                    CameraInitData18.Gain = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera18", "Gain"));
                    CameraInitData18.ExpsureTime = float.Parse(GlobalStaticData.OperateConfig.GetValue("Camera18", "CamExposureTime"));
                    GlobalStaticData.HIKCamera18 = new HIKCameraSDK(CameraInitData18.Number, 0, 0);
                    GlobalStaticData.HIKCamera18.SetGain(CameraInitData18.Gain);
                    GlobalStaticData.HIKCamera18.SetExposureTime(CameraInitData18.ExpsureTime);
                }






            }
            catch (Exception ex)
            {

                MessageBox.Show($"读取相机初始化参数失败：{ex.Message}");
            }

            try
            {

                GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold = float.Parse(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "conf_Score"));
                GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold = float.Parse(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "nms_threshold"));
                GlobalStaticData.CameraGroupConfig1.Cam1.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig1.Cam1.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig1.Cam2.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig1.Cam2.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "PositioningTolerance"));
                GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_XYTolerance = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "XYTolerance"));
                GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_ZTolerance = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "ZToleranceEx​​"));

            


                GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("PublicPositionConfig", "CalibrationBoardHeight​"));
          

                GlobalStaticData.CameraGroupConfig1.worldTransformerData.X_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam1PositionConfig", "XOffset"));
                GlobalStaticData.CameraGroupConfig1.worldTransformerData.Y_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam1PositionConfig", "YOffset"));
                GlobalStaticData.CameraGroupConfig1.worldTransformerData.Z_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam1PositionConfig", "ZOffset"));
                GlobalStaticData.CameraGroupConfig1.worldTransformerData.Rz_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam1PositionConfig", "RzOffset"));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机组1参数读取失败:{ex.Message}");
            }
            try
            {
                GlobalStaticData.CameraGroupConfig2.Cam0.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig2.Cam0.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig2.Cam1.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig2.Cam1.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig2.Cam2.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig2.Cam2.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig2.findCoorPairsData.PositionTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance;
                GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_XYTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_XYTolerance;
                GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_ZTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_ZTolerance;
               GlobalStaticData.CameraGroupConfig2.worldTransformerData.BoardHeight = GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight;

                GlobalStaticData.CameraGroupConfig2.worldTransformerData.X_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam2PositionConfig", "XOffset"));
                GlobalStaticData.CameraGroupConfig2.worldTransformerData.Y_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam2PositionConfig", "YOffset"));
                GlobalStaticData.CameraGroupConfig2.worldTransformerData.Z_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam2PositionConfig", "ZOffset"));
                GlobalStaticData.CameraGroupConfig2.worldTransformerData.Rz_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam2PositionConfig", "RzOffset"));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机组2参数读取失败:{ex.Message}");
            }

            try
            {

                GlobalStaticData.CameraGroupConfig3.Cam0.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig3.Cam0.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig3.Cam1.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig3.Cam1.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig3.Cam2.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig3.Cam2.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig3.findCoorPairsData.PositionTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance;
                GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_XYTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_XYTolerance;
                GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_ZTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_ZTolerance;
                GlobalStaticData.CameraGroupConfig3.worldTransformerData.BoardHeight = GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight;

                GlobalStaticData.CameraGroupConfig3.worldTransformerData.X_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam3PositionConfig", "XOffset"));
                GlobalStaticData.CameraGroupConfig3.worldTransformerData.Y_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam3PositionConfig", "YOffset"));
                GlobalStaticData.CameraGroupConfig3.worldTransformerData.Z_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam3PositionConfig", "ZOffset"));
                GlobalStaticData.CameraGroupConfig3.worldTransformerData.Rz_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam3PositionConfig", "RzOffset"));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机组3参数读取失败:{ex.Message}");
            }

            try
            {

                GlobalStaticData.CameraGroupConfig4.Cam0.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig4.Cam0.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig4.Cam1.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig4.Cam1.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig4.Cam2.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig4.Cam2.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig4.findCoorPairsData.PositionTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance;
                GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_XYTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_XYTolerance;
                GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_ZTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_ZTolerance;
                GlobalStaticData.CameraGroupConfig4.worldTransformerData.BoardHeight = GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight;

                GlobalStaticData.CameraGroupConfig4.worldTransformerData.X_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam4PositionConfig", "XOffset"));
                GlobalStaticData.CameraGroupConfig4.worldTransformerData.Y_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam4PositionConfig", "YOffset"));
                GlobalStaticData.CameraGroupConfig4.worldTransformerData.Z_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam4PositionConfig", "ZOffset"));
                GlobalStaticData.CameraGroupConfig4.worldTransformerData.Rz_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam4PositionConfig", "RzOffset"));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机组4参数读取失败:{ex.Message}");
            }

            try
            {

                GlobalStaticData.CameraGroupConfig5.Cam0.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig5.Cam0.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig5.Cam1.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig5.Cam1.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig5.Cam2.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig5.Cam2.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig5.findCoorPairsData.PositionTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance;
                GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_XYTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_XYTolerance;
                GlobalStaticData.CameraGroupConfig5.findCoorPairsData.hv_ZTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_ZTolerance;
                GlobalStaticData.CameraGroupConfig5.worldTransformerData.BoardHeight = GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight;
                                                  
                GlobalStaticData.CameraGroupConfig5.worldTransformerData.X_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam5PositionConfig", "XOffset"));
                GlobalStaticData.CameraGroupConfig5.worldTransformerData.Y_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam5PositionConfig", "YOffset"));
                GlobalStaticData.CameraGroupConfig5.worldTransformerData.Z_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam5PositionConfig", "ZOffset"));
                GlobalStaticData.CameraGroupConfig5.worldTransformerData.Rz_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam5PositionConfig", "RzOffset"));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机组4参数读取失败:{ex.Message}");
            }


            try
            {

                GlobalStaticData.CameraGroupConfig6.Cam0.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig6.Cam0.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig6.Cam1.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig6.Cam1.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig6.Cam2.YoloInferData.conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                GlobalStaticData.CameraGroupConfig6.Cam2.YoloInferData.nms_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.nms_threshold;
                GlobalStaticData.CameraGroupConfig6.findCoorPairsData.PositionTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance;
                GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_XYTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_XYTolerance;
                GlobalStaticData.CameraGroupConfig6.findCoorPairsData.hv_ZTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_ZTolerance;
                GlobalStaticData.CameraGroupConfig6.worldTransformerData.BoardHeight = GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight;
                                                  
                GlobalStaticData.CameraGroupConfig6.worldTransformerData.X_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam6PositionConfig", "XOffset"));
                GlobalStaticData.CameraGroupConfig6.worldTransformerData.Y_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam6PositionConfig", "YOffset"));
                GlobalStaticData.CameraGroupConfig6.worldTransformerData.Z_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam6PositionConfig", "ZOffset"));
                GlobalStaticData.CameraGroupConfig6.worldTransformerData.Rz_Offset = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Cam6PositionConfig", "RzOffset"));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机组4参数读取失败:{ex.Message}");
            }


            try
            {
                GlobalStaticData.UpdataBingdingData.BottleTolerance = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "BottleTolerance"));
                GlobalStaticData.UpdataBingdingData.XCommandPoint = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "XCommandPoint"));
                GlobalStaticData.UpdataBingdingData.SafetyClearance = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "SafetyClearance"));
                GlobalStaticData.UpdataBingdingData.MinHeight = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "MinHeight​"));
                GlobalStaticData.UpdataBingdingData.MaxHeight = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "MaxHeight​"));
                GlobalStaticData.UpdataBingdingData.Robot1Threshold = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "Robot1Threshold"));
                GlobalStaticData.UpdataBingdingData.Robot2Threshold = Convert.ToInt32(GlobalStaticData.OperateConfig.GetValue("PublicPickConfig", "Robot2Threshold"));
            }
            catch (Exception ex)
            {

                MessageBox.Show($"初始化坐标筛选公共参数失败：{ex.Message}");
            }


            try
            {
                GlobalStaticData.HeightAligmentData1.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue("Bottle1AlignmentCompensation", "IsEnable"));
                GlobalStaticData.HeightAligmentData1.UpCompensation =Convert.ToDouble( GlobalStaticData.OperateConfig.GetValue("Bottle1AlignmentCompensation", "UpCompensation"));
                GlobalStaticData.HeightAligmentData1.DownCompensation=Convert.ToDouble( GlobalStaticData.OperateConfig.GetValue("Bottle1AlignmentCompensation", "DownCompensation"));
                GlobalStaticData.HeightAligmentData1.BaseHeight=Convert.ToDouble( GlobalStaticData.OperateConfig.GetValue("Bottle1AlignmentCompensation", "BaseHeight​"));
                GlobalStaticData.HeightAligmentData1.PlaceAttr=Convert.ToDouble( GlobalStaticData.OperateConfig.GetValue("Bottle1AlignmentCompensation", "PlaceAttr"));
                GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation=Convert.ToDouble( GlobalStaticData.OperateConfig.GetValue("Bottle1AlignmentCompensation", "PlaceCompensation"));

                GlobalStaticData.HeightAligmentData2.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue("Bottle2AlignmentCompensation", "IsEnable"));
                GlobalStaticData.HeightAligmentData2.UpCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle2AlignmentCompensation", "UpCompensation"));
                GlobalStaticData.HeightAligmentData2.DownCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle2AlignmentCompensation", "DownCompensation"));
                GlobalStaticData.HeightAligmentData2.BaseHeight = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle2AlignmentCompensation", "BaseHeight​"));
                GlobalStaticData.HeightAligmentData2.PlaceAttr = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle2AlignmentCompensation", "PlaceAttr"));
                GlobalStaticData.HeightAligmentData2.PlaceHeightCompeensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle2AlignmentCompensation", "PlaceCompensation"));

                GlobalStaticData.HeightAligmentData3.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue("Bottle3AlignmentCompensation", "IsEnable"));
                GlobalStaticData.HeightAligmentData3.UpCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle3AlignmentCompensation", "UpCompensation"));
                GlobalStaticData.HeightAligmentData3.DownCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle3AlignmentCompensation", "DownCompensation"));
                GlobalStaticData.HeightAligmentData3.BaseHeight = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle3AlignmentCompensation", "BaseHeight​"));
                GlobalStaticData.HeightAligmentData3.PlaceAttr = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle3AlignmentCompensation", "PlaceAttr"));
                GlobalStaticData.HeightAligmentData3.PlaceHeightCompeensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle3AlignmentCompensation", "PlaceCompensation"));

                GlobalStaticData.HeightAligmentData4.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue("Bottle4AlignmentCompensation", "IsEnable"));
                GlobalStaticData.HeightAligmentData4.UpCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle4AlignmentCompensation", "UpCompensation"));
                GlobalStaticData.HeightAligmentData4.DownCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle4AlignmentCompensation", "DownCompensation"));
                GlobalStaticData.HeightAligmentData4.BaseHeight = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle4AlignmentCompensation", "BaseHeight​"));
                GlobalStaticData.HeightAligmentData4.PlaceAttr = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle4AlignmentCompensation", "PlaceAttr"));
                GlobalStaticData.HeightAligmentData4.PlaceHeightCompeensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle4AlignmentCompensation", "PlaceCompensation"));

                GlobalStaticData.HeightAligmentData5.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue("Bottle5AlignmentCompensation", "IsEnable"));
                GlobalStaticData.HeightAligmentData5.UpCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle5AlignmentCompensation", "UpCompensation"));
                GlobalStaticData.HeightAligmentData5.DownCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle5AlignmentCompensation", "DownCompensation"));
                GlobalStaticData.HeightAligmentData5.BaseHeight = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle5AlignmentCompensation", "BaseHeight​"));
                GlobalStaticData.HeightAligmentData5.PlaceAttr = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle5AlignmentCompensation", "PlaceAttr"));
                GlobalStaticData.HeightAligmentData5.PlaceHeightCompeensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle5AlignmentCompensation", "PlaceCompensation"));

                GlobalStaticData.HeightAligmentData6.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue("Bottle6AlignmentCompensation", "IsEnable"));
                GlobalStaticData.HeightAligmentData6.UpCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle6AlignmentCompensation", "UpCompensation"));
                GlobalStaticData.HeightAligmentData6.DownCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle6AlignmentCompensation", "DownCompensation"));
                GlobalStaticData.HeightAligmentData6.BaseHeight = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle6AlignmentCompensation", "BaseHeight​"));
                GlobalStaticData.HeightAligmentData6.PlaceAttr = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle6AlignmentCompensation", "PlaceAttr"));
                GlobalStaticData.HeightAligmentData6.PlaceHeightCompeensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle6AlignmentCompensation", "PlaceCompensation"));

                GlobalStaticData.HeightAligmentData7.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue("Bottle7AlignmentCompensation", "IsEnable"));
                GlobalStaticData.HeightAligmentData7.UpCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle7AlignmentCompensation", "UpCompensation"));
                GlobalStaticData.HeightAligmentData7.DownCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle7AlignmentCompensation", "DownCompensation"));
                GlobalStaticData.HeightAligmentData7.BaseHeight = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle7AlignmentCompensation", "BaseHeight​"));
                GlobalStaticData.HeightAligmentData7.PlaceAttr = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle7AlignmentCompensation", "PlaceAttr"));
                GlobalStaticData.HeightAligmentData7.PlaceHeightCompeensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle7AlignmentCompensation", "PlaceCompensation"));

                GlobalStaticData.HeightAligmentData8.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue("Bottle8AlignmentCompensation", "IsEnable"));
                GlobalStaticData.HeightAligmentData8.UpCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle8AlignmentCompensation", "UpCompensation"));
                GlobalStaticData.HeightAligmentData8.DownCompensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle8AlignmentCompensation", "DownCompensation"));
                GlobalStaticData.HeightAligmentData8.BaseHeight = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle8AlignmentCompensation", "BaseHeight​"));
                GlobalStaticData.HeightAligmentData8.PlaceAttr = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle8AlignmentCompensation", "PlaceAttr"));
                GlobalStaticData.HeightAligmentData8.PlaceHeightCompeensation = Convert.ToDouble(GlobalStaticData.OperateConfig.GetValue("Bottle8AlignmentCompensation", "PlaceCompensation"));
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
