using Sunny.UI;
using System;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class RuningParamSet2 : UIPage
    {
        private readonly CameraOffsetState _cam3Offset = new CameraOffsetState();
        private readonly CameraOffsetState _cam4Offset = new CameraOffsetState();

        public RuningParamSet2()
        {
            InitializeComponent();
            this.Load += RuningParamSet2_Load;
        }

        private void RuningParamSet2_Load(object sender, EventArgs e)
        {
            try
            {
                RuningParamOffsetHelper.LoadCameraOffset(GlobalStaticData.CameraGroupConfig3.worldTransformerData, CreateCam3Controls(), _cam3Offset);
                RuningParamOffsetHelper.LoadCameraOffset(GlobalStaticData.CameraGroupConfig4.worldTransformerData, CreateCam4Controls(), _cam4Offset);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机3/4组合参数界面绑定数据初始化异常：{ex.Message}");
            }
        }

        private void num_Cam3XOffset_ValueChanged(object sender, double value)
        {
            _cam3Offset.X = value;
        }

        private void num_Cam3YOffset_ValueChanged(object sender, double value)
        {
            _cam3Offset.Y = value;
        }

        private void num_Cam3ZOffset_ValueChanged(object sender, double value)
        {
            _cam3Offset.Z = value;
        }

        private void num_Cam3RzOffset_ValueChanged(object sender, double value)
        {
            _cam3Offset.Rz = value;
        }

        private void num_Cam4XOffset_ValueChanged(object sender, double value)
        {
            _cam4Offset.X = value;
        }

        private void num_Cam4YOffset_ValueChanged(object sender, double value)
        {
            _cam4Offset.Y = value;
        }

        private void num_Cam4ZOffset_ValueChanged(object sender, double value)
        {
            _cam4Offset.Z = value;
        }

        private void num_Cam4RzOffset_ValueChanged(object sender, double value)
        {
            _cam4Offset.Rz = value;
        }

        private void btn_SaveConfig3_Click(object sender, EventArgs e)
        {
            RuningParamOffsetHelper.SaveCameraOffset("Cam3PositionConfig", GlobalStaticData.CameraGroupConfig3, _cam3Offset);
        }

        private void btn_SaveConfig4_Click(object sender, EventArgs e)
        {
            RuningParamOffsetHelper.SaveCameraOffset("Cam4PositionConfig", GlobalStaticData.CameraGroupConfig4, _cam4Offset);
        }

        private CameraOffsetControls CreateCam3Controls()
        {
            return new CameraOffsetControls(num_Cam3XOffset, num_Cam3YOffset, num_Cam3ZOffset, num_Cam3RzOffset);
        }

        private CameraOffsetControls CreateCam4Controls()
        {
            return new CameraOffsetControls(num_Cam4XOffset, num_Cam4YOffset, num_Cam4ZOffset, num_Cam4RzOffset);
        }
    }
}
