using Sunny.UI;
using System;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class RuningParamSet3 : UIPage
    {
        private readonly CameraOffsetState _cam5Offset = new CameraOffsetState();
        private readonly CameraOffsetState _cam6Offset = new CameraOffsetState();

        public RuningParamSet3()
        {
            InitializeComponent();
            this.Load += RuningParamSet3_Load;
        }

        private void RuningParamSet3_Load(object sender, EventArgs e)
        {
            try
            {
                RuningParamOffsetHelper.LoadCameraOffset(GlobalStaticData.CameraGroupConfig5.worldTransformerData, CreateCam5Controls(), _cam5Offset);
                RuningParamOffsetHelper.LoadCameraOffset(GlobalStaticData.CameraGroupConfig6.worldTransformerData, CreateCam6Controls(), _cam6Offset);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机5/6组合参数界面绑定数据初始化异常：{ex.Message}");
            }
        }

        private void num_Cam3XOffset_ValueChanged(object sender, double value)
        {
            _cam5Offset.X = value;
        }

        private void num_Cam3YOffset_ValueChanged(object sender, double value)
        {
            _cam5Offset.Y = value;
        }

        private void num_Cam3ZOffset_ValueChanged(object sender, double value)
        {
            _cam5Offset.Z = value;
        }

        private void num_Cam3RzOffset_ValueChanged(object sender, double value)
        {
            _cam5Offset.Rz = value;
        }

        private void num_Cam4XOffset_ValueChanged(object sender, double value)
        {
            _cam6Offset.X = value;
        }

        private void num_Cam4YOffset_ValueChanged(object sender, double value)
        {
            _cam6Offset.Y = value;
        }

        private void num_Cam4ZOffset_ValueChanged(object sender, double value)
        {
            _cam6Offset.Z = value;
        }

        private void num_Cam4RzOffset_ValueChanged(object sender, double value)
        {
            _cam6Offset.Rz = value;
        }

        private void btn_SaveConfig3_Click(object sender, EventArgs e)
        {
            RuningParamOffsetHelper.SaveCameraOffset("Cam5PositionConfig", GlobalStaticData.CameraGroupConfig5, _cam5Offset);
        }

        private void btn_SaveConfig4_Click(object sender, EventArgs e)
        {
            RuningParamOffsetHelper.SaveCameraOffset("Cam6PositionConfig", GlobalStaticData.CameraGroupConfig6, _cam6Offset);
        }

        private CameraOffsetControls CreateCam5Controls()
        {
            return new CameraOffsetControls(num_Cam3XOffset, num_Cam3YOffset, num_Cam3ZOffset, num_Cam3RzOffset);
        }

        private CameraOffsetControls CreateCam6Controls()
        {
            return new CameraOffsetControls(num_Cam4XOffset, num_Cam4YOffset, num_Cam4ZOffset, num_Cam4RzOffset);
        }
    }
}
