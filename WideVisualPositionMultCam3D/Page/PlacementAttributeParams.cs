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
    public partial class PlacementAttributeParams : UIPage
    {
        public PlacementAttributeParams()
        {
            InitializeComponent();
            this.Load += PlacementAttributeParams_Load;
        }

        private void PlacementAttributeParams_Load(object sender, EventArgs e)
        {
          cb_isOrNoStartAttri.Active=  GlobalStaticData.placeWebBeltSelectData1.IsEnable;
            if (cb_isOrNoStartAttri.Active)
            {
                panel1.Enabled = true;
            }
            else
            {
                panel1.Enabled = false;
            }
            num_Threshold1Down.Value = GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold1Down;
            num_Threshold1Up.Value = GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold1Up;
            num_Threshold2Up.Value = GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold2Up;
            num_Threshold3Up.Value = GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold3Up;
            num_Threshold4Up.Value = GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold4Up;
            num_Threshold2Down.Value = GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold2Down;
            num_Threshold3Down.Value = GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold3Down;
            num_Threshold4Down.Value = GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold4Down;
            cmb_Atrribute1.SelectedIndex = GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri1 - 1;
            cmb_Atrribute2.SelectedIndex = GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri2 - 1;
            cmb_Atrribute3.SelectedIndex = GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri3 - 1;


        }

        private void cb_isOrNoStartAttri_ValueChanged(object sender, bool value)
        {
            if (cb_isOrNoStartAttri.Active)
            {
                panel1.Enabled = true;
                GlobalStaticData.placeWebBeltSelectData1.IsEnable=true;
            }
            else
            {
                panel1.Enabled= false;
                GlobalStaticData.placeWebBeltSelectData1.IsEnable = false;
            }
           // GlobalStaticData.placeWebBeltSelectData1.IsEnable = bool.Parse(GlobalStaticData.OperateConfig.GetValue("PlaceWebBeltSelectData1", "IsEnable"));
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "IsEnable", GlobalStaticData.placeWebBeltSelectData1.IsEnable.ToString());
            GlobalStaticData.SendRobotCoorRefresh=true;
        }

        private void btn_SaveConfig1_Click(object sender, EventArgs e)
        {
            GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold1Down = num_Threshold1Down.Value;
            GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold1Up = num_Threshold1Up.Value;
            GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri1 = Convert.ToInt32(cmb_Atrribute1.Text);
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationThreshold1Down", GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold1Down.ToString());
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationThreshold1Up", GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold1Up.ToString());
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationAttri1", GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri1.ToString());
            DisplayMessageHalper.displayMessageSuccesses("网带1属性设置成功");
            GlobalStaticData.SendRobotCoorRefresh = true;
        }

        private void btn_SaveConfig2_Click(object sender, EventArgs e)
        {
            GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold2Down = num_Threshold2Down.Value;
            GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold2Up = num_Threshold2Up.Value;
            GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri2 = Convert.ToInt32(cmb_Atrribute2.Text);
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationThreshold2Down", GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold2Down.ToString());
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationThreshold2Up", GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold2Up.ToString());
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationAttri2", GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri2.ToString());
            DisplayMessageHalper.displayMessageSuccesses("网带2属性设置成功");
            GlobalStaticData.SendRobotCoorRefresh = true;
        }

        private void btn_SaveConfig3_Click(object sender, EventArgs e)
        {
            GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold3Down = num_Threshold3Down.Value;
            GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold3Up = num_Threshold3Up.Value;
            GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri3 = Convert.ToInt32(cmb_Atrribute3.Text);
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationThreshold3Down", GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold3Down.ToString());
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationThreshold3Up", GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold3Up.ToString());
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationAttri3", GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri3.ToString());
            DisplayMessageHalper.displayMessageSuccesses("网带3属性设置成功");
            GlobalStaticData.SendRobotCoorRefresh = true;
        }

        private void btn_SaveConfig4_Click(object sender, EventArgs e)
        {
            GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold4Down = num_Threshold4Down.Value;
            GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold4Up = num_Threshold4Up.Value;
            GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri4 = Convert.ToInt32(cmb_Atrribute4.Text);
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationThreshold4Down", GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold4Down.ToString());
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationThreshold4Up", GlobalStaticData.placeWebBeltSelectData1.SegmentationThreshold4Up.ToString());
            GlobalStaticData.OperateConfig.SetValue("PlaceWebBeltSelectData1", "SegmentationAttri4", GlobalStaticData.placeWebBeltSelectData1.SegmentationAttri4.ToString());
            DisplayMessageHalper.displayMessageSuccesses("网带3属性设置成功");
            GlobalStaticData.SendRobotCoorRefresh = true;
        }
    }
}
