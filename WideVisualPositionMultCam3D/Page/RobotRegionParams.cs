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
    public partial class RobotRegionParams : UIPage
    {
        public RobotRegionParams()
        {
            InitializeComponent();
            try
            {
                num_BottleTolerance.DataBindings.Add("Value", GlobalStaticData.UpdataBingdingData, "BottleTolerance");
                num_X_CommandPoint.DataBindings.Add("Value", GlobalStaticData.UpdataBingdingData, "XCommandPoint");
                num_SafelyDisp.DataBindings.Add("Value", GlobalStaticData.UpdataBingdingData, "SafetyClearance");
                num_MinHeight.DataBindings.Add("Value", GlobalStaticData.UpdataBingdingData, "MinHeight");
                num_MaxHeight.DataBindings.Add("Value", GlobalStaticData.UpdataBingdingData, "MaxHeight");
                num_Robot1Threshold.DataBindings.Add("Value", GlobalStaticData.UpdataBingdingData, "Robot1Threshold");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"坐标分配界面绑定参数异常:{ex.Message}");
            }
          
        }

      

        private void num_BottleTolerance_ValueChanged(object sender, double value)
        {
            GlobalStaticData.UpdataBingdingData.BottleTolerance = Convert.ToInt32(value);
        }

        private void num_MinHeight_ValueChanged(object sender, double value)
        {
            GlobalStaticData.UpdataBingdingData.MinHeight = Convert.ToInt32(value);
        }

        private void num_MaxHeight_ValueChanged(object sender, double value)
        {
            GlobalStaticData.UpdataBingdingData.MaxHeight = Convert.ToInt32(value);
        }

        private void num_X_CommandPoint_ValueChanged(object sender, double value)
        {
            GlobalStaticData.UpdataBingdingData.XCommandPoint = Convert.ToInt32(value);
        }

        private void num_SafelyDisp_ValueChanged(object sender, double value)
        {
            GlobalStaticData.UpdataBingdingData.SafetyClearance = Convert.ToInt32(value);
        }

        private void num_Robot1Threshold_ValueChanged(object sender, double value)
        {
            GlobalStaticData.UpdataBingdingData.Robot1Threshold = Convert.ToInt32(value);
        }

        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 1)
                {
                   // UIMessageTip.ShowWarning("当前用户没有权限操作");
                    DisplayMessageHalper.displayMessageWarning("当前用户没有权限操作");
                    return;
                }

                GlobalStaticData.OperateConfig.SetValue("PublicPickConfig", "BottleTolerance", GlobalStaticData.UpdataBingdingData.BottleTolerance.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPickConfig", "XCommandPoint", GlobalStaticData.UpdataBingdingData.XCommandPoint.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPickConfig", "SafetyClearance", GlobalStaticData.UpdataBingdingData.SafetyClearance.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPickConfig", "MinHeight​", GlobalStaticData.UpdataBingdingData.MinHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPickConfig", "MaxHeight​", GlobalStaticData.UpdataBingdingData.MaxHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("PublicPickConfig", "Robot1Threshold", GlobalStaticData.UpdataBingdingData.Robot1Threshold.ToString());
                GlobalStaticData.CoorSelectRefresh = true;
                GlobalStaticData.SendRobotCoorRefresh = true;

               // UIMessageTip.ShowOk("参数保存成功");
                DisplayMessageHalper.displayMessageSuccesses("参数保存成功");
            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"参数保存失败:{ex.Message}");
                //UIMessageTip.ShowError($"参数保存失败:{ex.Message}");
            }
        }

       
    }
}
