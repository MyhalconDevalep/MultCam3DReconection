using Microsoft.ML.OnnxRuntime;
using Sunny.UI;
using System;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class PublicRuningParams : UIPage
    {
        private float _conf_threshold = 0.6f;

        private double _positionTolerance = 8;
        private double _calib_Board_H = 12;
        private double _XY_Tolerance = 0.012;
        private double _Z_Tolerance = 0.008;
        public PublicRuningParams()
        {
            InitializeComponent();
            try
            {
           
                num_CofThreshold.Value = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                num_PositionTolerance.Value = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance;
                num_Calib_Board_H.Value = GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight;
                num_XY_Tolerance.Value = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_XYTolerance;
                num_Z_Tolerance.Value = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_ZTolerance;

                _conf_threshold = GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold;
                _positionTolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance;
                _calib_Board_H = GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight;
                _XY_Tolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_XYTolerance;
                _Z_Tolerance = GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_ZTolerance;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"定位界面绑定参数异常:{ex.Message}");
            }

        }

    



        private void num_CofThreshold_ValueChanged(object sender, double value)
        {

            _conf_threshold=(float)value;
           
        }

      
        private void num_PositionTolerance_ValueChanged(object sender, double value)
        {
            _positionTolerance= value;
           

        }
       
        private void num_Calib_Board_H_ValueChanged(object sender, double value)
        {
            _calib_Board_H= value;
           

        }


        private void num_XY_Tolerance_ValueChanged(object sender, double value)
        {
            _XY_Tolerance= value;
        }

        private void num_Z_Tolerance_ValueChanged(object sender, double value)
        {
            _Z_Tolerance= value;
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
                GlobalStaticData.OperateConfig.SetValue("PublicPositionConfig", "conf_Score", _conf_threshold.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPositionConfig", "PositioningTolerance",_positionTolerance.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPositionConfig", "CalibrationBoardHeight​",_calib_Board_H.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPositionConfig", "XYTolerance", _XY_Tolerance.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPositionConfig", "ZToleranceEx​​", _Z_Tolerance.ToString());

                GlobalStaticData.CameraGroupConfig1.Cam0.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig1.Cam1.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig1.Cam2.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig2.Cam0.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig2.Cam1.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig2.Cam2.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig3.Cam0.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig3.Cam1.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig3.Cam2.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig4.Cam0.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig4.Cam1.YoloInferData.conf_threshold = _conf_threshold;
                GlobalStaticData.CameraGroupConfig4.Cam2.YoloInferData.conf_threshold = _conf_threshold;

                GlobalStaticData.CameraGroupConfig1.findCoorPairsData.PositionTolerance = _positionTolerance;
                GlobalStaticData.CameraGroupConfig2.findCoorPairsData.PositionTolerance = _positionTolerance;
                GlobalStaticData.CameraGroupConfig3.findCoorPairsData.PositionTolerance = _positionTolerance;
                GlobalStaticData.CameraGroupConfig4.findCoorPairsData.PositionTolerance = _positionTolerance;

                GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_XYTolerance = _XY_Tolerance;
                GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_XYTolerance = _XY_Tolerance;
                GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_XYTolerance = _XY_Tolerance;
                GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_XYTolerance = _XY_Tolerance;

                GlobalStaticData.CameraGroupConfig1.findCoorPairsData.hv_ZTolerance = _Z_Tolerance;
                GlobalStaticData.CameraGroupConfig2.findCoorPairsData.hv_ZTolerance = _Z_Tolerance;
                GlobalStaticData.CameraGroupConfig3.findCoorPairsData.hv_ZTolerance = _Z_Tolerance;
                GlobalStaticData.CameraGroupConfig4.findCoorPairsData.hv_ZTolerance = _Z_Tolerance;

                GlobalStaticData.CameraGroupConfig1.worldTransformerData.BoardHeight = _calib_Board_H;
                GlobalStaticData.CameraGroupConfig2.worldTransformerData.BoardHeight = _calib_Board_H;
                GlobalStaticData.CameraGroupConfig3.worldTransformerData.BoardHeight = _calib_Board_H;
                GlobalStaticData.CameraGroupConfig4.worldTransformerData.BoardHeight = _calib_Board_H;


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
