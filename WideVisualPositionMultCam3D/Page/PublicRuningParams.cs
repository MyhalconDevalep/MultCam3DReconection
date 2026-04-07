using Microsoft.ML.OnnxRuntime;
using Sunny.UI;
using System;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class PublicRuningParams : UIPage
    {
        public PublicRuningParams()
        {
            InitializeComponent();
            try
            {
           
                num_CofThreshold.Value = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                num_PositionTolerance.Value = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance;
                num_Calib_Board_H.Value = GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"定位界面绑定参数异常:{ex.Message}");
            }

        }

    



        private void num_CofThreshold_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig1.Cam1.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig1.Cam2.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig2.Cam0.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig2.Cam1.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig2.Cam2.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig3.Cam0.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig3.Cam1.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig3.Cam2.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig4.Cam0.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig4.Cam1.YoloInferData.conf_threshold = (float)value;
            GlobalStaticData.CameraGroupConfig4.Cam2.YoloInferData.conf_threshold = (float)value;
        }

        private void num_PositionTolerance_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance = value;
            GlobalStaticData.CameraGroupConfig2.findCoorPairsData.PositionTolerance = value;
            GlobalStaticData.CameraGroupConfig3.findCoorPairsData.PositionTolerance = value;
            GlobalStaticData.CameraGroupConfig4.findCoorPairsData.PositionTolerance = value;

        }

        private void num_Calib_Board_H_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight = value;
            GlobalStaticData.CameraGroupConfig2.worldTransformerData.BoardHeight = value;
            GlobalStaticData.CameraGroupConfig3.worldTransformerData.BoardHeight = value;
            GlobalStaticData.CameraGroupConfig4.worldTransformerData.BoardHeight = value;

        }

        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 1)
            {
                DisplayMessageHalper.displayMessageWarning("当前用户没有权限操作");
               // UIMessageTip.ShowWarning("当前用户没有权限操作");
                return;
            }
            try
            {
                GlobalStaticData.OperateConfig.SetValue("PublicPositionConfig", "conf_Score", GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPositionConfig", "PositioningTolerance", GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPositionConfig", "CalibrationBoardHeight​", GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight.ToString());
                GlobalStaticData.PositionRefresh = true;
                GlobalStaticData.CameraGroupConfig1.Version++;
                GlobalStaticData.CameraGroupConfig2.Version++;
                GlobalStaticData.CameraGroupConfig3.Version++;
                GlobalStaticData.CameraGroupConfig4.Version++;
                DisplayMessageHalper.displayMessageSuccesses("参数保存成功");
              //  UIMessageTip.ShowOk("参数保存成功");
            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"参数保存失败:{ex.Message}");
               // UIMessageTip.ShowError($"参数保存失败:{ex.Message}");
            }
        }
    }
}
