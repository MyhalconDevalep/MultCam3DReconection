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

namespace WideVisualPositionMultCam3D.Page
{
    public partial class Calibration2 : UIPage
    {
        public Calibration2()
        {
            InitializeComponent();
            this.Load += Page2_Load;

        }

        private void Page2_Load(object sender, EventArgs e)
        {
            btn_StarCalibrationCams.Enabled = false;
            btn_SaveCalibration.Enabled = false;
            //HOperatorSet.ReadImage(out HObject image, @"D:\\3DModelRebuild\\Images1\\0\\1_1.bmp");
            //GlobalStaticData.displayConvert.SetHalconScalingZoom(image, hWindowControl1.HalconWindow);
            //hWindowControl1.HalconWindow.DispObj(image);
        }

        string dir = "";
        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 2)
            {
                UIMessageTip.ShowWarning("当前用户没有权限操作");
                return;
            }
            if (GlobalStaticData.allReady)
            {
                UIMessageTip.ShowWarning("请确认程序是否处于停止状态");
                return;
            }
            if (DirEx.SelectDirEx("扩展打开文件夹", ref dir))
            {
                UIMessageTip.ShowOk(dir);
                HOperatorSet.ReadImage(out HObject image0, dir + "0\\1_0.bmp");
                HOperatorSet.ReadImage(out HObject image1, dir + "1\\2_0.bmp");
                HOperatorSet.ReadImage(out HObject image2, dir + "2\\3_0.bmp");
                HOperatorSet.GetImageSize(image1, out HTuple width, out HTuple height);
                GlobalStaticData.HalconAlgorithmFunction.Calibration_model_Init(0.006, 0.00000345, 0.00000345, width, height, dir + "caltab_240mm.descr", out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CalibDataID);
                GlobalStaticData.displayConvert.SetHalconScalingZoom(image0, hWindowControl1.HalconWindow);
                GlobalStaticData.displayConvert.SetHalconScalingZoom(image1, hWindowControl2.HalconWindow);
                GlobalStaticData.displayConvert.SetHalconScalingZoom(image2, hWindowControl3.HalconWindow);
                hWindowControl1.HalconWindow.DispObj(image0.Clone());
                hWindowControl2.HalconWindow.DispObj(image1.Clone());
                hWindowControl3.HalconWindow.DispObj(image2.Clone());
                image0.Dispose();
                btn_StarCalibrationCams.Enabled = true;
            }

        }

        HTuple hv_Errors = 0;
        private async void btn_StarCalibrationCams_Click(object sender, EventArgs e)
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 2)
            {
                UIMessageTip.ShowWarning("当前用户没有权限操作");
                return;
            }
            if (GlobalStaticData.allReady)
            {
                UIMessageTip.ShowWarning("请确认程序是否处于停止状态");
                return;
            }
            btn_StarCalibrationCams.Enabled = false;
            btn_SaveCalibration.Enabled = false;
            await Task.Run(() => { GlobalStaticData.HalconAlgorithmFunction.Calibration_mask(dir, ref GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CalibDataID, hWindowControl1.HalconWindow, hWindowControl2.HalconWindow, hWindowControl3.HalconWindow, out hv_Errors); });
            SystemEx.Delay(50);
            if (hv_Errors == null)
            {
                UIMessageTip.ShowOk("标定异常！");
                return;
            }
            lb_CalibrationErr.Text = hv_Errors.ToString();
            btn_SaveCalibration.Enabled = true;
            UIMessageTip.ShowOk("标定完成");
        }

        private void btn_SaveCalibration_Click(object sender, EventArgs e)
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 2)
            {
                UIMessageTip.ShowWarning("当前用户没有权限操作");
                return;
            }
            if (GlobalStaticData.allReady)
            {
                UIMessageTip.ShowWarning("请确认程序是否处于停止状态");
                return;
            }
            if (hv_Errors.D > 0 && hv_Errors.D < 5)
            {
                bool res = GlobalStaticData.HalconAlgorithmFunction.Write_calibration_data(GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CalibDataID, GlobalStaticData.WriteCalibrationPath + @"\Calibration\calibration_data2.cal", out GlobalStaticData.CameraGroupConfig2.worldTransformerData.hv_CamParamData0, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamPose0, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamParamData1, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamPose1, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamParamData2, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CamPose2, out GlobalStaticData.CameraGroupConfig2.worldTransformerData.hv_World2CamMat0, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_InvertToCamMat0, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_InvertToCamMat1, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_InvertToCamMat2, out GlobalStaticData.CameraGroupConfig2.worldTransformerData.hv_PlanePose, out GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_CameraSetupModel, out GlobalStaticData.CameraGroupConfig2.hv_StereoModelIDGroup);
                if (res)
                {
                    GlobalStaticData.CameraGroupConfig2.Version++;
                    btn_StarCalibrationCams.Enabled = false;
                    btn_SaveCalibration.Enabled = false;
                    UIMessageTip.ShowOk("标定保存成功");
                }
                else
                {
                    UIMessageTip.ShowOk("标定保存失败");
                }
            }

        }

 
    }
}
