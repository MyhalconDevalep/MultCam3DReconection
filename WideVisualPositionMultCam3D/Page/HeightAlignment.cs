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
    public partial class HeightAlignment : UIPage
    {
        public HeightAlignment()
        {
            InitializeComponent();
            this.Load += HeightAlignment_Load;
        }

        private void HeightAlignment_Load(object sender, EventArgs e)
        {
            try
            {
                cb_BottleEnable.Checked = GlobalStaticData.HeightAligmentData1.IsEnable;
                if(GlobalStaticData.HeightAligmentData1.IsEnable==false)
                {
                    panel1.Enabled = false;
                }
                num_UpCompensation1.Value = GlobalStaticData.HeightAligmentData1.UpCompensation;
                num_DownCompensation1.Value = GlobalStaticData.HeightAligmentData1.DownCompensation;
                num_BaseHeight1.Value = GlobalStaticData.HeightAligmentData1.BaseHeight;
                num_placeAttr.Value = GlobalStaticData.HeightAligmentData1.PlaceAttr;
               // num_PlaceCompensation1.Value = GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("高度对齐参数1加载失败");
            }


            try
            {
                cb_BottleEnable2.Checked = GlobalStaticData.HeightAligmentData2.IsEnable;
                if (GlobalStaticData.HeightAligmentData2.IsEnable == false)
                {
                    panel2.Enabled = false;
                }
                num_UpCompensation2.Value = GlobalStaticData.HeightAligmentData2.UpCompensation;
                num_DownCompensation2.Value = GlobalStaticData.HeightAligmentData2.DownCompensation;
                num_BaseHeight2.Value = GlobalStaticData.HeightAligmentData2.BaseHeight;
                num_placeAttr2.Value = GlobalStaticData.HeightAligmentData2.PlaceAttr;
                // num_PlaceCompensation1.Value = GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("高度对齐参数2加载失败");
            }

            try
            {
                cb_BottleEnable3.Checked = GlobalStaticData.HeightAligmentData3.IsEnable;
                if (GlobalStaticData.HeightAligmentData3.IsEnable == false)
                {
                    panel3.Enabled = false;
                }
                num_UpCompensation3.Value = GlobalStaticData.HeightAligmentData3.UpCompensation;
                num_DownCompensation3.Value = GlobalStaticData.HeightAligmentData3.DownCompensation;
                num_BaseHeight3.Value = GlobalStaticData.HeightAligmentData3.BaseHeight;
                num_placeAttr3.Value = GlobalStaticData.HeightAligmentData3.PlaceAttr;
                // num_PlaceCompensation1.Value = GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("高度对齐参数3加载失败");
            }

            try
            {
                cb_BottleEnable4.Checked = GlobalStaticData.HeightAligmentData4.IsEnable;
                if (GlobalStaticData.HeightAligmentData4.IsEnable == false)
                {
                    panel4.Enabled = false;
                }
                num_UpCompensation4.Value = GlobalStaticData.HeightAligmentData4.UpCompensation;
                num_DownCompensation4.Value = GlobalStaticData.HeightAligmentData4.DownCompensation;
                num_BaseHeight4.Value = GlobalStaticData.HeightAligmentData4.BaseHeight;
                num_placeAttr4.Value = GlobalStaticData.HeightAligmentData4.PlaceAttr;
                // num_PlaceCompensation1.Value = GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("高度对齐参数4加载失败");
            }



        }

        private void btn_SaveHeight1_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalStaticData.HeightAligmentData1.IsEnable = cb_BottleEnable.Checked;
                GlobalStaticData.HeightAligmentData1.UpCompensation = num_UpCompensation1.Value;
                GlobalStaticData.HeightAligmentData1.DownCompensation = num_DownCompensation1.Value;
                GlobalStaticData.HeightAligmentData1.BaseHeight = num_BaseHeight1.Value;
                GlobalStaticData.HeightAligmentData1.PlaceAttr = num_placeAttr.Value;
                //GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation = num_PlaceCompensation1.Value;
                GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "IsEnable", GlobalStaticData.HeightAligmentData1.IsEnable.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "UpCompensation", GlobalStaticData.HeightAligmentData1.UpCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "DownCompensation", GlobalStaticData.HeightAligmentData1.DownCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "BaseHeight​", GlobalStaticData.HeightAligmentData1.BaseHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "PlaceAttr", GlobalStaticData.HeightAligmentData1.PlaceAttr.ToString());
               // GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "PlaceCompensation", GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation.ToString());
                GlobalStaticData.SendRobotCoorRefresh = true;
                DisplayMessageHalper.displayMessageSuccesses("瓶型对齐参数1保存成功");
               // UIMessageTip.ShowOk("参数保存成功");

            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"瓶型对齐参数1保存失败{ex.Message}");
               // UIMessageTip.ShowError("参数保存失败");
            }
        }

        private void btn_SaveHeight2_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalStaticData.HeightAligmentData2.IsEnable = cb_BottleEnable2.Checked;
                GlobalStaticData.HeightAligmentData2.UpCompensation = num_UpCompensation2.Value;
                GlobalStaticData.HeightAligmentData2.DownCompensation = num_DownCompensation2.Value;
                GlobalStaticData.HeightAligmentData2.BaseHeight = num_BaseHeight2.Value;
                GlobalStaticData.HeightAligmentData2.PlaceAttr = num_placeAttr2.Value;
                //GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation = num_PlaceCompensation1.Value;
                GlobalStaticData.OperateConfig.SetValue("Bottle2AlignmentCompensation", "IsEnable", GlobalStaticData.HeightAligmentData2.IsEnable.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle2AlignmentCompensation", "UpCompensation", GlobalStaticData.HeightAligmentData2.UpCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle2AlignmentCompensation", "DownCompensation", GlobalStaticData.HeightAligmentData2.DownCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle2AlignmentCompensation", "BaseHeight​", GlobalStaticData.HeightAligmentData2.BaseHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle2AlignmentCompensation", "PlaceAttr", GlobalStaticData.HeightAligmentData2.PlaceAttr.ToString());
                // GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "PlaceCompensation", GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation.ToString());
                GlobalStaticData.SendRobotCoorRefresh = true;
                DisplayMessageHalper.displayMessageSuccesses("瓶型对齐参数2保存成功");
                // UIMessageTip.ShowOk("参数保存成功");

            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"瓶型对齐参数2保存失败{ex.Message}");
                // UIMessageTip.ShowError("参数保存失败");
            }
        }

        private void btn_SaveHeight3_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalStaticData.HeightAligmentData3.IsEnable = cb_BottleEnable3.Checked;
                GlobalStaticData.HeightAligmentData3.UpCompensation = num_UpCompensation3.Value;
                GlobalStaticData.HeightAligmentData3.DownCompensation = num_DownCompensation3.Value;
                GlobalStaticData.HeightAligmentData3.BaseHeight = num_BaseHeight3.Value;
                GlobalStaticData.HeightAligmentData3.PlaceAttr = num_placeAttr3.Value;
                //GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation = num_PlaceCompensation1.Value;
                GlobalStaticData.OperateConfig.SetValue("Bottle3AlignmentCompensation", "IsEnable", GlobalStaticData.HeightAligmentData3.IsEnable.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle3AlignmentCompensation", "UpCompensation", GlobalStaticData.HeightAligmentData3.UpCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle3AlignmentCompensation", "DownCompensation", GlobalStaticData.HeightAligmentData3.DownCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle3AlignmentCompensation", "BaseHeight​", GlobalStaticData.HeightAligmentData3.BaseHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle3AlignmentCompensation", "PlaceAttr", GlobalStaticData.HeightAligmentData3.PlaceAttr.ToString());
                // GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "PlaceCompensation", GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation.ToString());
                GlobalStaticData.SendRobotCoorRefresh = true;
                DisplayMessageHalper.displayMessageSuccesses("瓶型对齐参数3保存成功");
                // UIMessageTip.ShowOk("参数保存成功");

            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"瓶型对齐参数3保存失败{ex.Message}");
                // UIMessageTip.ShowError("参数保存失败");
            }
        }

        private void btn_SaveHeight4_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalStaticData.HeightAligmentData4.IsEnable = cb_BottleEnable4.Checked;
                GlobalStaticData.HeightAligmentData4.UpCompensation = num_UpCompensation4.Value;
                GlobalStaticData.HeightAligmentData4.DownCompensation = num_DownCompensation4.Value;
                GlobalStaticData.HeightAligmentData4.BaseHeight = num_BaseHeight4.Value;
                GlobalStaticData.HeightAligmentData4.PlaceAttr = num_placeAttr4.Value;
                //GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation = num_PlaceCompensation1.Value;
                GlobalStaticData.OperateConfig.SetValue("Bottle4AlignmentCompensation", "IsEnable", GlobalStaticData.HeightAligmentData4.IsEnable.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle4AlignmentCompensation", "UpCompensation", GlobalStaticData.HeightAligmentData4.UpCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle4AlignmentCompensation", "DownCompensation", GlobalStaticData.HeightAligmentData4.DownCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle4AlignmentCompensation", "BaseHeight​", GlobalStaticData.HeightAligmentData4.BaseHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle4AlignmentCompensation", "PlaceAttr", GlobalStaticData.HeightAligmentData4.PlaceAttr.ToString());
                // GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "PlaceCompensation", GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation.ToString());
                GlobalStaticData.SendRobotCoorRefresh = true;
                DisplayMessageHalper.displayMessageSuccesses("瓶型对齐参数4保存成功");
                // UIMessageTip.ShowOk("参数保存成功");

            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"瓶型对齐参数4保存失败{ex.Message}");
                // UIMessageTip.ShowError("参数保存失败");
            }
        }



        private void cb_BottleEnable_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_BottleEnable.Checked == true)
            {
                panel1.Enabled = true;
            }
            else
            {
                panel1.Enabled = false;
            }
        }
        private void cb_BottleEnable2_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_BottleEnable2.Checked == true)
            {
                panel2.Enabled = true;
            }
            else
            {
                panel2.Enabled = false;
            }
        }

        private void cb_BottleEnable3_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_BottleEnable3.Checked == true)
            {
                panel3.Enabled = true;
            }
            else
            {
                panel3.Enabled = false;
            }
        }

        private void cb_BottleEnable4_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_BottleEnable4.Checked == true)
            {
                panel4.Enabled = true;
            }
            else
            {
                panel4.Enabled = false;
            }
        }

      
     
    }
}
