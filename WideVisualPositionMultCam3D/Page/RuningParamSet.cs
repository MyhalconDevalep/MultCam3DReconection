using Sunny.UI;
using System;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class RuningParamSet : UIPage
    {
        private readonly CameraOffsetState _cam1Offset = new CameraOffsetState();
        private readonly CameraOffsetState _cam2Offset = new CameraOffsetState();

        public RuningParamSet()
        {
            InitializeComponent();
            this.Load += RuningParamSet_Load;
        }

        private void RuningParamSet_Load(object sender, EventArgs e)
        {
            try
            {
                RuningParamOffsetHelper.LoadCameraOffset(GlobalStaticData.CameraGroupConfig1.worldTransformerData, CreateCam1Controls(), _cam1Offset);
                RuningParamOffsetHelper.LoadCameraOffset(GlobalStaticData.CameraGroupConfig2.worldTransformerData, CreateCam2Controls(), _cam2Offset);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机组合1/2参数界面绑定数据初始化异常：{ex.Message}");
            }
        }

        private void num_Cam1XOffset_ValueChanged(object sender, double value)
        {
            _cam1Offset.X = value;
        }

        private void num_Cam1YOffset_ValueChanged(object sender, double value)
        {
            _cam1Offset.Y = value;
        }

        private void num_Cam1ZOffset_ValueChanged(object sender, double value)
        {
            _cam1Offset.Z = value;
        }

        private void num_Cam1RzOffset_ValueChanged(object sender, double value)
        {
            _cam1Offset.Rz = value;
        }

        private void num_Cam2XOffset_ValueChanged(object sender, double value)
        {
            _cam2Offset.X = value;
        }

        private void num_Cam2YOffset_ValueChanged(object sender, double value)
        {
            _cam2Offset.Y = value;
        }

        private void num_Cam2ZOffset_ValueChanged(object sender, double value)
        {
            _cam2Offset.Z = value;
        }

        private void num_Cam2RzOffset_ValueChanged(object sender, double value)
        {
            _cam2Offset.Rz = value;
        }

        private void btn_SaveConfig1_Click(object sender, EventArgs e)
        {
            RuningParamOffsetHelper.SaveCameraOffset("Cam1PositionConfig", GlobalStaticData.CameraGroupConfig1, _cam1Offset);
        }

        private void btn_SaveConfig2_Click(object sender, EventArgs e)
        {
            RuningParamOffsetHelper.SaveCameraOffset("Cam2PositionConfig", GlobalStaticData.CameraGroupConfig2, _cam2Offset);
        }

        private CameraOffsetControls CreateCam1Controls()
        {
            return new CameraOffsetControls(num_Cam1XOffset, num_Cam1YOffset, num_Cam1ZOffset, num_Cam1RzOffset);
        }

        private CameraOffsetControls CreateCam2Controls()
        {
            return new CameraOffsetControls(num_Cam2XOffset, num_Cam2YOffset, num_Cam2ZOffset, num_Cam2RzOffset);
        }
    }
}
