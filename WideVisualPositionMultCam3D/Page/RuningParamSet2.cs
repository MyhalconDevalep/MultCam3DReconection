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
    public partial class RuningParamSet2 : UIPage
    {
        private double _cam3_X_Offset = 0;
        private double _cam3_Y_Offset = 0;
        private double _cam3_Z_Offset = 0;
        private double _cam3_Rz_offset = 0;

        private double _cam4_X_Offset = 0;
        private double _cam4_Y_Offset = 0;
        private double _cam4_Z_Offset = 0;
        private double _cam4_Rz_offset = 0;

        public RuningParamSet2()
        {
            InitializeComponent();
            this.Load += RuningParamSet2_Load;
        }

        private void RuningParamSet2_Load(object sender, EventArgs e)
        {
            try
            { 
             num_Cam3XOffset.Value = GlobalStaticData.CameraGroupConfig3.worldTransformerData.X_Offset;
             num_Cam3YOffset.Value = GlobalStaticData.CameraGroupConfig3.worldTransformerData.Y_Offset;
             num_Cam3ZOffset.Value = GlobalStaticData.CameraGroupConfig3.worldTransformerData.Z_Offset;
             num_Cam3RzOffset.Value = GlobalStaticData.CameraGroupConfig3.worldTransformerData.Rz_Offset;
             num_Cam4XOffset.Value = GlobalStaticData.CameraGroupConfig4.worldTransformerData.X_Offset;
             num_Cam4YOffset.Value = GlobalStaticData.CameraGroupConfig4.worldTransformerData.Y_Offset;
             num_Cam4ZOffset.Value = GlobalStaticData.CameraGroupConfig4.worldTransformerData.Z_Offset;
             num_Cam4RzOffset.Value = GlobalStaticData.CameraGroupConfig4.worldTransformerData.Rz_Offset;


             _cam3_X_Offset = GlobalStaticData.CameraGroupConfig3.worldTransformerData.X_Offset;
             _cam3_Y_Offset = GlobalStaticData.CameraGroupConfig3.worldTransformerData.Y_Offset;
             _cam3_Z_Offset= GlobalStaticData.CameraGroupConfig3.worldTransformerData.Z_Offset;
             _cam3_Rz_offset = GlobalStaticData.CameraGroupConfig3.worldTransformerData.Rz_Offset;
             _cam4_X_Offset = GlobalStaticData.CameraGroupConfig4.worldTransformerData.X_Offset;
             _cam4_Y_Offset = GlobalStaticData.CameraGroupConfig4.worldTransformerData.Y_Offset;
             _cam4_Z_Offset = GlobalStaticData.CameraGroupConfig4.worldTransformerData.Z_Offset;
             _cam4_Rz_offset = GlobalStaticData.CameraGroupConfig4.worldTransformerData.Rz_Offset;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机3/4组合参数界面绑定数据初始化异常：{ex.Message}");

            }
        }

        private void num_Cam3XOffset_ValueChanged(object sender, double value)
        {
           _cam3_X_Offset= value;
        }

        private void num_Cam3YOffset_ValueChanged(object sender, double value)
        {
           _cam3_Y_Offset = value;
        }

        private void num_Cam3ZOffset_ValueChanged(object sender, double value)
        {
           _cam3_Z_Offset= value;
        }

        private void num_Cam3RzOffset_ValueChanged(object sender, double value)
        {
            _cam3_Rz_offset = value;
        }


        private void num_Cam4XOffset_ValueChanged(object sender, double value)
        {
           _cam4_X_Offset = value;
        }

        private void num_Cam4YOffset_ValueChanged(object sender, double value)
        {
            _cam4_Y_Offset= value;
        }

        private void num_Cam4ZOffset_ValueChanged(object sender, double value)
        {
            _cam4_Z_Offset= value;
        }

        private void num_Cam4RzOffset_ValueChanged(object sender, double value)
        {
           _cam4_Rz_offset = value;
        }

 

        private void btn_SaveConfig3_Click(object sender, EventArgs e)
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 1)
            {
                DisplayMessageHalper.displayMessageWarning("当前用户没有权限操作");
                // UIMessageTip.ShowWarning("当前用户没有权限操作");
                return;
            }
            try
            {
                GlobalStaticData.OperateConfig.SetValue("Cam3PositionConfig", "XOffset", _cam3_X_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam3PositionConfig", "YOffset", _cam3_Y_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam3PositionConfig", "ZOffset", _cam3_Z_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam3PositionConfig", "RzOffset", _cam3_Rz_offset.ToString());
                GlobalStaticData.CameraGroupConfig3.worldTransformerData.X_Offset= _cam3_X_Offset;
                GlobalStaticData.CameraGroupConfig3.worldTransformerData.Y_Offset= _cam3_Y_Offset;
                GlobalStaticData.CameraGroupConfig3.worldTransformerData.Z_Offset= _cam3_Z_Offset;
                GlobalStaticData.CameraGroupConfig3.worldTransformerData.Rz_Offset= _cam3_Rz_offset;
                // GlobalStaticData.PositionRefresh = true;
                GlobalStaticData.CameraGroupConfig3.Version++;
                DisplayMessageHalper.displayMessageSuccesses("参数保存成功");
               // UIMessageTip.ShowOk("参数保存成功");
            }
            catch (Exception ex)
            {
               // UIMessageTip.ShowError($"参数保存失败:{ex.Message}");
                DisplayMessageHalper.displayMessageErro($"参数保存失败:{ex.Message}");
            }
        }

        private void btn_SaveConfig4_Click(object sender, EventArgs e)
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 1)
            {
                DisplayMessageHalper.displayMessageWarning("当前用户没有权限操作");
                // UIMessageTip.ShowWarning("当前用户没有权限操作");
                return;
            }
            try
            {
                GlobalStaticData.OperateConfig.SetValue("Cam4PositionConfig", "XOffset", _cam4_X_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam4PositionConfig", "YOffset", _cam4_Y_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam4PositionConfig", "ZOffset",_cam4_Z_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam4PositionConfig", "RzOffset", _cam4_Rz_offset.ToString());
                GlobalStaticData.CameraGroupConfig4.worldTransformerData.X_Offset= _cam4_X_Offset;
                GlobalStaticData.CameraGroupConfig4.worldTransformerData.Y_Offset= _cam4_Y_Offset;
                GlobalStaticData.CameraGroupConfig4.worldTransformerData.Z_Offset= _cam4_Z_Offset;
                GlobalStaticData.CameraGroupConfig4.worldTransformerData.Rz_Offset= _cam4_Rz_offset;
                // GlobalStaticData.PositionRefresh = true;
                GlobalStaticData.CameraGroupConfig4.Version++;
                DisplayMessageHalper.displayMessageSuccesses("参数保存成功");
               // UIMessageTip.ShowOk("参数保存成功");
            }
            catch (Exception ex)
            {
               // UIMessageTip.ShowError($"参数保存失败:{ex.Message}");
                DisplayMessageHalper.displayMessageErro($"参数保存失败:{ex.Message}");
            }
        }
    }
}
