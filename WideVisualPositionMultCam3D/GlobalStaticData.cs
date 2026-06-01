using HalconDotNet;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Concurrent;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.Models;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D
{
   public static class GlobalStaticData
    {
        //图像转换显示功具类
        public static DisplayDataConvertClass displayConvert { get; set; }= new DisplayDataConvertClass();
        public static ConcurrentDictionary<int, HObject> _imageBuffer;
        public static int CameraNum = 3;
        //相机组参数
        public static CameraGroupConfig CameraGroupConfig1 { get; set; }
        public static CameraGroupConfig CameraGroupConfig2 { get; set; }
        public static CameraGroupConfig CameraGroupConfig3 { get; set; }
        public static CameraGroupConfig CameraGroupConfig4 { get; set; }

        public static CameraGroupConfig CameraGroupConfig5 { get; set; }

        public static CameraGroupConfig CameraGroupConfig6 { get; set; }
        public static  CameraGroupConfig GetGroupConfig(int grop)
        {
            switch(grop)
            {
                    case 0:return CameraGroupConfig1;
                    case 1: return CameraGroupConfig2;
                    case 2: return CameraGroupConfig3;
                    case 3: return CameraGroupConfig4;
                    case 4: return CameraGroupConfig5;
                    case 5: return CameraGroupConfig6;
                    default:return CameraGroupConfig1;
            }
        }


        public static InferenceSession onnx_session;
        //配置文件保存类
        public static OperateConfig OperateConfig { get; set; }
        //相机初始类
        public static HIKCameraSDK HIKCamera1 {  get; set; }
        public static HIKCameraSDK HIKCamera2 { get; set; }
        public static HIKCameraSDK HIKCamera3 { get; set; }


        public static HIKCameraSDK HIKCamera4 { get; set; }
        public static HIKCameraSDK HIKCamera5 { get; set; }
        public static HIKCameraSDK HIKCamera6 { get; set; }


        public static HIKCameraSDK HIKCamera7 { get; set; }
        public static HIKCameraSDK HIKCamera8 { get; set; }
        public static HIKCameraSDK HIKCamera9 { get; set; }


        public static HIKCameraSDK HIKCamera10 { get; set; }
        public static HIKCameraSDK HIKCamera11 { get; set; }
        public static HIKCameraSDK HIKCamera12{ get; set; }


        public static HIKCameraSDK HIKCamera13 { get; set; }
        public static HIKCameraSDK HIKCamera14 { get; set; }
        public static HIKCameraSDK HIKCamera15 { get; set; }

        public static HIKCameraSDK HIKCamera16 { get; set; }
        public static HIKCameraSDK HIKCamera17 { get; set; }
        public static HIKCameraSDK HIKCamera18 { get; set; }

        public static bool PositionRefresh { get; set; } = false;
        public static bool CoorSelectRefresh { get; set; } = false;
        public static bool SendRobotCoorRefresh {  get; set; } = false;

        public static Action<HObject> Cam1DisplayEvent { get; set; }
        public static Action<HObject> Cam2DisplayEvent { get; set; }
        public static Action<HObject> Cam3DisplayEvent { get; set; }

        public static UpdataBingdingData UpdataBingdingData { get; set; }

        public static HeightAligmentData HeightAligmentData1 { get; set; }
        public static HeightAligmentData HeightAligmentData2 { get; set; }
        public static HeightAligmentData HeightAligmentData3 { get; set; }
        public static HeightAligmentData HeightAligmentData4 { get; set; }

        public static HeightAligmentData HeightAligmentData5 { get; set; }
        public static HeightAligmentData HeightAligmentData6 { get; set; }
        public static HeightAligmentData HeightAligmentData7 { get; set; }
        public static HeightAligmentData HeightAligmentData8 { get; set; }

        public static PlaceWebBeltSelectData placeWebBeltSelectData1 { get; set; }

        //运行标志
        public static bool allReady { get; set; } = false;
        //相机个数
        public static int CameraCount { get; set; } = 3;
        public static int SendDataState { get; set; } = 0;

        public static int SendRobotNum {  get; set; } = 0;
        public static double SendXOffset { get; set; } = 50;
        //存图权限
        public static bool WriteGlobalImage { get; set; } =false;
        public static UpdataBingdingDisplayMsg UpdataBingdingDisplayMsgq{ get; set; }

       // public static bool CalibrationMode1 = false;
        public static bool CalibrationSaveEnabel1 = false;
        public static int CalibrationIndex1 = 0;

        public static ConcurrentBag<FindCoorData> blockingCollectiontest{get;set;}

        public static HalconAlgorithmFunction HalconAlgorithmFunction { get; set; }= new HalconAlgorithmFunction();
        public static string model_path = Application.StartupPath + "\\onnx\\best.onnx";
        public static string classer_path = Application.StartupPath + "\\onnx\\label.txt";
        public static string WriteCalibrationPath = Application.StartupPath;
        public static CalibrationData CalibrationData1 = new CalibrationData();
   
    }
}
