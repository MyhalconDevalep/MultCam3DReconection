using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.Models
{
    public class CalibrationData
    {
        public HTuple hv_CalibDataID ;

        public  HTuple hv_CamParamData0 ;
        public  HTuple hv_CamPose0 ;
        public  HTuple hv_CamParamData1 ;
        public  HTuple hv_CamPose1 ;
        public  HTuple hv_CamParamData2 ;
        public  HTuple hv_CamPose2 ;
        public  HTuple hv_World2CamMat0 ;
        public  HTuple hv_InvertToCamMat0 ;
        public  HTuple hv_InvertToCamMat1 ;
        public  HTuple hv_InvertToCamMat2 ;
        public  HTuple hv_PlanePose ;
        public  HTuple hv_CameraSetupModel ;
        public  HTuple hv_StereoModelID ;
    }
}
