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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机3/4组合参数界面绑定数据初始化异常：{ex.Message}");

            }
        }

        private void num_Cam3XOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig3.worldTransformerData.X_Offset = value;
        }

        private void num_Cam3YOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig3.worldTransformerData.Y_Offset = value;
        }

        private void num_Cam3ZOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig3.worldTransformerData.Z_Offset = value;
        }

        private void num_Cam3RzOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig3.worldTransformerData.Rz_Offset = value;
        }


        private void num_Cam4XOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig4.worldTransformerData.X_Offset = value;
        }

        private void num_Cam4YOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig4.worldTransformerData.Y_Offset = value;
        }

        private void num_Cam4ZOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig4.worldTransformerData.Z_Offset = value;
        }

        private void num_Cam4RzOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig4.worldTransformerData.Rz_Offset = value;
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
                GlobalStaticData.OperateConfig.SetValue("Cam3PositionConfig", "XOffset", GlobalStaticData.CameraGroupConfig3.worldTransformerData.X_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam3PositionConfig", "YOffset", GlobalStaticData.CameraGroupConfig3.worldTransformerData.Y_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam3PositionConfig", "ZOffset", GlobalStaticData.CameraGroupConfig3.worldTransformerData.Z_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam3PositionConfig", "RzOffset", GlobalStaticData.CameraGroupConfig3.worldTransformerData.Rz_Offset.ToString());
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
                GlobalStaticData.OperateConfig.SetValue("Cam4PositionConfig", "XOffset", GlobalStaticData.CameraGroupConfig4.worldTransformerData.X_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam4PositionConfig", "YOffset", GlobalStaticData.CameraGroupConfig4.worldTransformerData.Y_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam4PositionConfig", "ZOffset", GlobalStaticData.CameraGroupConfig4.worldTransformerData.Z_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam4PositionConfig", "RzOffset", GlobalStaticData.CameraGroupConfig4.worldTransformerData.Rz_Offset.ToString());
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
