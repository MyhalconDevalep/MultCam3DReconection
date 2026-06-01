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
    public partial class RuningParamSet3 : UIPage
    {
        private double _cam5_X_Offset = 0;
        private double _cam5_Y_Offset = 0;
        private double _cam5_Z_Offset = 0;
        private double _cam5_Rz_offset = 0;

        private double _cam6_X_Offset = 0;
        private double _cam6_Y_Offset = 0;
        private double _cam6_Z_Offset = 0;
        private double _cam6_Rz_offset = 0;

        public RuningParamSet3()
        {
            InitializeComponent();
            this.Load += RuningParamSet2_Load;
        }

        private void RuningParamSet2_Load(object sender, EventArgs e)
        {
            try
            { 
             num_Cam3XOffset.Value = GlobalStaticData.CameraGroupConfig5.worldTransformerData.X_Offset;
             num_Cam3YOffset.Value = GlobalStaticData.CameraGroupConfig5.worldTransformerData.Y_Offset;
             num_Cam3ZOffset.Value = GlobalStaticData.CameraGroupConfig5.worldTransformerData.Z_Offset;
             num_Cam3RzOffset.Value = GlobalStaticData.CameraGroupConfig5.worldTransformerData.Rz_Offset;
             num_Cam4XOffset.Value = GlobalStaticData.CameraGroupConfig6.worldTransformerData.X_Offset;
             num_Cam4YOffset.Value = GlobalStaticData.CameraGroupConfig6.worldTransformerData.Y_Offset;
             num_Cam4ZOffset.Value = GlobalStaticData.CameraGroupConfig6.worldTransformerData.Z_Offset;
             num_Cam4RzOffset.Value = GlobalStaticData.CameraGroupConfig6.worldTransformerData.Rz_Offset;


             _cam5_X_Offset = GlobalStaticData.CameraGroupConfig5.worldTransformerData.X_Offset;
             _cam5_Y_Offset = GlobalStaticData.CameraGroupConfig5.worldTransformerData.Y_Offset;
             _cam5_Z_Offset= GlobalStaticData.CameraGroupConfig5.worldTransformerData.Z_Offset;
             _cam5_Rz_offset = GlobalStaticData.CameraGroupConfig5.worldTransformerData.Rz_Offset;
             _cam6_X_Offset = GlobalStaticData.CameraGroupConfig6.worldTransformerData.X_Offset;
             _cam6_Y_Offset = GlobalStaticData.CameraGroupConfig6.worldTransformerData.Y_Offset;
             _cam6_Z_Offset = GlobalStaticData.CameraGroupConfig6.worldTransformerData.Z_Offset;
             _cam6_Rz_offset = GlobalStaticData.CameraGroupConfig6.worldTransformerData.Rz_Offset;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"相机3/4组合参数界面绑定数据初始化异常：{ex.Message}");

            }
        }

        private void num_Cam3XOffset_ValueChanged(object sender, double value)
        {
           _cam5_X_Offset= value;
        }

        private void num_Cam3YOffset_ValueChanged(object sender, double value)
        {
           _cam5_Y_Offset = value;
        }

        private void num_Cam3ZOffset_ValueChanged(object sender, double value)
        {
           _cam5_Z_Offset= value;
        }

        private void num_Cam3RzOffset_ValueChanged(object sender, double value)
        {
            _cam5_Rz_offset = value;
        }


        private void num_Cam4XOffset_ValueChanged(object sender, double value)
        {
           _cam6_X_Offset = value;
        }

        private void num_Cam4YOffset_ValueChanged(object sender, double value)
        {
            _cam6_Y_Offset= value;
        }

        private void num_Cam4ZOffset_ValueChanged(object sender, double value)
        {
            _cam6_Z_Offset= value;
        }

        private void num_Cam4RzOffset_ValueChanged(object sender, double value)
        {
           _cam6_Rz_offset = value;
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
                GlobalStaticData.OperateConfig.SetValue("Cam5PositionConfig", "XOffset", _cam5_X_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam5PositionConfig", "YOffset", _cam5_Y_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam5PositionConfig", "ZOffset", _cam5_Z_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam5PositionConfig", "RzOffset", _cam5_Rz_offset.ToString());
                GlobalStaticData.CameraGroupConfig5.worldTransformerData.X_Offset= _cam5_X_Offset;
                GlobalStaticData.CameraGroupConfig5.worldTransformerData.Y_Offset= _cam5_Y_Offset;
                GlobalStaticData.CameraGroupConfig5.worldTransformerData.Z_Offset= _cam5_Z_Offset;
                GlobalStaticData.CameraGroupConfig5.worldTransformerData.Rz_Offset= _cam5_Rz_offset;
                // GlobalStaticData.PositionRefresh = true;
                GlobalStaticData.CameraGroupConfig5.Version++;
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
                GlobalStaticData.OperateConfig.SetValue("Cam6PositionConfig", "XOffset", _cam6_X_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam6PositionConfig", "YOffset", _cam6_Y_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam6PositionConfig", "ZOffset",_cam6_Z_Offset.ToString());
                GlobalStaticData.OperateConfig.SetValue("Cam6PositionConfig", "RzOffset", _cam6_Rz_offset.ToString());
                GlobalStaticData.CameraGroupConfig6.worldTransformerData.X_Offset= _cam6_X_Offset;
                GlobalStaticData.CameraGroupConfig6.worldTransformerData.Y_Offset= _cam6_Y_Offset;
                GlobalStaticData.CameraGroupConfig6.worldTransformerData.Z_Offset= _cam6_Z_Offset;
                GlobalStaticData.CameraGroupConfig6.worldTransformerData.Rz_Offset= _cam6_Rz_offset;
                // GlobalStaticData.PositionRefresh = true;
                GlobalStaticData.CameraGroupConfig6.Version++;
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
