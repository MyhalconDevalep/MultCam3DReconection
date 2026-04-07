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
    public partial class RuningParamSet : UIPage
    {
        public RuningParamSet()
        {
            InitializeComponent();
            this.Load += RuningParamSet_Load; ;
          
        }

        private void RuningParamSet_Load(object sender, EventArgs e)
        {
            try
            {
               
                num_Cam1XOffset.Value=GlobalStaticData.CameraGroupConfig1.worldTransformerData.X_Offset;
                num_Cam1YOffset.Value = GlobalStaticData.CameraGroupConfig1.worldTransformerData.Y_Offset;
                num_Cam1ZOffset.Value=GlobalStaticData.CameraGroupConfig1.worldTransformerData.Z_Offset;
                num_Cam1RzOffset.Value = GlobalStaticData.CameraGroupConfig1.worldTransformerData.Rz_Offset;
                num_Cam2XOffset.Value = GlobalStaticData.CameraGroupConfig2.worldTransformerData.X_Offset;
                num_Cam2YOffset.Value = GlobalStaticData.CameraGroupConfig2.worldTransformerData.Y_Offset;
                num_Cam2ZOffset.Value = GlobalStaticData.CameraGroupConfig2.worldTransformerData.Z_Offset;
                num_Cam2RzOffset.Value = GlobalStaticData.CameraGroupConfig2.worldTransformerData.Rz_Offset;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机组合1/2参数界面绑定数据初始化异常：{ex.Message}");
              
            }
          
        }

      

        private void num_Cam1XOffset_ValueChanged(object sender, double value)
        {
             GlobalStaticData.CameraGroupConfig1.worldTransformerData.X_Offset = value;
        }

        private void num_Cam1YOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig1.worldTransformerData.Y_Offset =value;
        }

        private void num_Cam1ZOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig1.worldTransformerData.Z_Offset = value;
        }

        private void num_Cam1RzOffset_ValueChanged(object sender, double value)
        {
             GlobalStaticData.CameraGroupConfig1.worldTransformerData.Rz_Offset = value;
        }

      

        private void num_Cam2XOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig2.worldTransformerData.X_Offset = value;
        }

        private void num_Cam2YOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig2.worldTransformerData.Y_Offset = value;
        }

        private void num_Cam2ZOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig2.worldTransformerData.Z_Offset = value;
        }

        private void num_Cam2RzOffset_ValueChanged(object sender, double value)
        {
            GlobalStaticData.CameraGroupConfig2.worldTransformerData.Rz_Offset = value;
        }

        private void btn_SaveConfig1_Click(object sender, EventArgs e)
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 1)
            {
                DisplayMessageHalper.displayMessageWarning("当前用户没有权限操作");
                // UIMessageTip.ShowWarning("当前用户没有权限操作");
                return;
            }
            try
            {
                GlobalStaticData.OperateConfig.SetValue("Cam1PositionConfig", "XOffset", GlobalStaticData.CameraGroupConfig1.worldTransformerData.X_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam1PositionConfig", "YOffset", GlobalStaticData.CameraGroupConfig1.worldTransformerData.Y_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam1PositionConfig", "ZOffset", GlobalStaticData.CameraGroupConfig1.worldTransformerData.Z_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam1PositionConfig", "RzOffset", GlobalStaticData.CameraGroupConfig1.worldTransformerData.Rz_Offset.ToString());
                // GlobalStaticData.PositionRefresh = true;
                GlobalStaticData.CameraGroupConfig1.Version++;
                DisplayMessageHalper.displayMessageSuccesses("参数保存成功");
               // UIMessageTip.ShowOk("参数保存成功");
            }
            catch (Exception ex)
            {
               // UIMessageTip.ShowError($"参数保存失败:{ex.Message}");
                DisplayMessageHalper.displayMessageErro($"参数保存失败:{ex.Message}");
            }
        }

        private void btn_SaveConfig2_Click(object sender, EventArgs e)
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 1)
            {
                DisplayMessageHalper.displayMessageWarning("当前用户没有权限操作");
                // UIMessageTip.ShowWarning("当前用户没有权限操作");
                return;
            }
            try
            {
                GlobalStaticData.OperateConfig.SetValue("Cam2PositionConfig", "XOffset", GlobalStaticData.CameraGroupConfig2.worldTransformerData.X_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam2PositionConfig", "YOffset", GlobalStaticData.CameraGroupConfig2.worldTransformerData.Y_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam2PositionConfig", "ZOffset", GlobalStaticData.CameraGroupConfig2.worldTransformerData.Z_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam2PositionConfig", "RzOffset", GlobalStaticData.CameraGroupConfig2.worldTransformerData.Rz_Offset.ToString());
                // GlobalStaticData.PositionRefresh = true;
                GlobalStaticData.CameraGroupConfig2.Version++;
                DisplayMessageHalper.displayMessageSuccesses("参数保存成功");
               // UIMessageTip.ShowOk("参数保存成功");
            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"参数保存失败:{ex.Message}");
               // UIMessageTip.ShowError($"参数保存失败:{ex.Message}");
            }
        }
    }
}
