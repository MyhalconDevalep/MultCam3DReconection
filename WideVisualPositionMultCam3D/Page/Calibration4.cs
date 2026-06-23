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
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class Calibration4 : UIPage
    {
        public Calibration4()
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
                if (!CalibrationFolderLoader.TryLoad(dir, GlobalStaticData.CameraGroupConfig4, hWindowControl1, hWindowControl2, hWindowControl3, out string warning))
                {
                    UIMessageTip.ShowWarning(warning);
                    btn_StarCalibrationCams.Enabled = false;
                    return;
                }

                UIMessageTip.ShowOk(dir);
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
            await Task.Run(() => { GlobalStaticData.HalconAlgorithmFunction.Calibration_mask(dir, ref GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_CalibDataID, hWindowControl1.HalconWindow, hWindowControl2.HalconWindow, hWindowControl3.HalconWindow, out hv_Errors); });
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
                if (CalibrationFolderLoader.TrySave(GlobalStaticData.CameraGroupConfig4, 4, out string warning))
                {
                    GlobalStaticData.CameraGroupConfig4.Version++;
                    btn_StarCalibrationCams.Enabled = false;
                    btn_SaveCalibration.Enabled = false;
                    UIMessageTip.ShowOk("标定保存成功");
                }
                else
                {
                    UIMessageTip.ShowWarning(warning);
                }
            }

        }
    }
}
