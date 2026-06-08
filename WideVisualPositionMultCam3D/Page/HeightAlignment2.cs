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
using WideVisualPositionMultCam3D.Models;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class HeightAlignment2 : UIPage
    {
        public HeightAlignment2()
        {
            InitializeComponent();
            this.Load += HeightAlignment2_Load;
        }

        private void HeightAlignment2_Load(object sender, EventArgs e)
        {
            try
            {
                cb_BottleEnable.Checked = GlobalStaticData.HeightAligmentData5.IsEnable;
                if (GlobalStaticData.HeightAligmentData5.IsEnable == false)
                {
                    panel1.Enabled = false;
                }
                num_UpCompensation1.Value = GlobalStaticData.HeightAligmentData5.UpCompensation;
                num_DownCompensation1.Value = GlobalStaticData.HeightAligmentData5.DownCompensation;
                num_BaseHeight1.Value = GlobalStaticData.HeightAligmentData5.BaseHeight;
                num_placeAttr.Value = GlobalStaticData.HeightAligmentData5.PlaceAttr;
                num_MouthMin1.Value = GlobalStaticData.HeightAligmentData5.MouthMinMm;
                num_MouthMax1.Value = GlobalStaticData.HeightAligmentData5.MouthMaxMm;
                // num_PlaceCompensation1.Value = GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("高度对齐参数1加载失败");
            }


            try
            {
                cb_BottleEnable2.Checked = GlobalStaticData.HeightAligmentData6.IsEnable;
                if (GlobalStaticData.HeightAligmentData6.IsEnable == false)
                {
                    panel2.Enabled = false;
                }
                num_UpCompensation2.Value = GlobalStaticData.HeightAligmentData6.UpCompensation;
                num_DownCompensation2.Value = GlobalStaticData.HeightAligmentData6.DownCompensation;
                num_BaseHeight2.Value = GlobalStaticData.HeightAligmentData6.BaseHeight;
                num_placeAttr2.Value = GlobalStaticData.HeightAligmentData6.PlaceAttr;
                num_MouthMin2.Value = GlobalStaticData.HeightAligmentData6.MouthMinMm;
                num_MouthMax2.Value = GlobalStaticData.HeightAligmentData6.MouthMaxMm;
                // num_PlaceCompensation1.Value = GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("高度对齐参数2加载失败");
            }

            try
            {
                cb_BottleEnable3.Checked = GlobalStaticData.HeightAligmentData7.IsEnable;
                if (GlobalStaticData.HeightAligmentData7.IsEnable == false)
                {
                    panel3.Enabled = false;
                }
                num_UpCompensation3.Value = GlobalStaticData.HeightAligmentData7.UpCompensation;
                num_DownCompensation3.Value = GlobalStaticData.HeightAligmentData7.DownCompensation;
                num_BaseHeight3.Value = GlobalStaticData.HeightAligmentData7.BaseHeight;
                num_placeAttr3.Value = GlobalStaticData.HeightAligmentData7.PlaceAttr;
                num_MouthMin3.Value = GlobalStaticData.HeightAligmentData7.MouthMinMm;
                num_MouthMax3.Value = GlobalStaticData.HeightAligmentData7.MouthMaxMm;
                // num_PlaceCompensation1.Value = GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("高度对齐参数3加载失败");
            }

            try
            {
                cb_BottleEnable4.Checked = GlobalStaticData.HeightAligmentData8.IsEnable;
                if (GlobalStaticData.HeightAligmentData8.IsEnable == false)
                {
                    panel4.Enabled = false;
                }
                num_UpCompensation4.Value = GlobalStaticData.HeightAligmentData8.UpCompensation;
                num_DownCompensation4.Value = GlobalStaticData.HeightAligmentData8.DownCompensation;
                num_BaseHeight4.Value = GlobalStaticData.HeightAligmentData8.BaseHeight;
                num_placeAttr4.Value = GlobalStaticData.HeightAligmentData8.PlaceAttr;
                num_MouthMin4.Value = GlobalStaticData.HeightAligmentData8.MouthMinMm;
                num_MouthMax4.Value = GlobalStaticData.HeightAligmentData8.MouthMaxMm;
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
                GlobalStaticData.HeightAligmentData5.IsEnable = cb_BottleEnable.Checked;
                GlobalStaticData.HeightAligmentData5.UpCompensation = num_UpCompensation1.Value;
                GlobalStaticData.HeightAligmentData5.DownCompensation = num_DownCompensation1.Value;
                GlobalStaticData.HeightAligmentData5.BaseHeight = num_BaseHeight1.Value;
                GlobalStaticData.HeightAligmentData5.PlaceAttr = num_placeAttr.Value;
                GlobalStaticData.HeightAligmentData5.MouthMinMm = num_MouthMin1.Value;
                GlobalStaticData.HeightAligmentData5.MouthMaxMm = num_MouthMax1.Value;
                //GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation = num_PlaceCompensation1.Value;
                GlobalStaticData.OperateConfig.SetValue("Bottle5AlignmentCompensation", "IsEnable", GlobalStaticData.HeightAligmentData5.IsEnable.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle5AlignmentCompensation", "UpCompensation", GlobalStaticData.HeightAligmentData5.UpCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle5AlignmentCompensation", "DownCompensation", GlobalStaticData.HeightAligmentData5.DownCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle5AlignmentCompensation", "BaseHeight​", GlobalStaticData.HeightAligmentData5.BaseHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle5AlignmentCompensation", "PlaceAttr", GlobalStaticData.HeightAligmentData5.PlaceAttr.ToString());
                SaveMouthSizeConfig(GlobalStaticData.HeightAligmentData5, "Bottle5AlignmentCompensation");
                // GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "PlaceCompensation", GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation.ToString());
                GlobalStaticData.SendRobotCoorRefresh = true;
                DisplayMessageHalper.displayMessageSuccesses("瓶型对齐参数5保存成功");
                // UIMessageTip.ShowOk("参数保存成功");

            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"瓶型对齐参数5保存失败{ex.Message}");
                // UIMessageTip.ShowError("参数保存失败");
            }
        }

        private void btn_SaveHeight2_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalStaticData.HeightAligmentData6.IsEnable = cb_BottleEnable2.Checked;
                GlobalStaticData.HeightAligmentData6.UpCompensation = num_UpCompensation2.Value;
                GlobalStaticData.HeightAligmentData6.DownCompensation = num_DownCompensation2.Value;
                GlobalStaticData.HeightAligmentData6.BaseHeight = num_BaseHeight2.Value;
                GlobalStaticData.HeightAligmentData6.PlaceAttr = num_placeAttr2.Value;
                GlobalStaticData.HeightAligmentData6.MouthMinMm = num_MouthMin2.Value;
                GlobalStaticData.HeightAligmentData6.MouthMaxMm = num_MouthMax2.Value;
                //GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation = num_PlaceCompensation1.Value;
                GlobalStaticData.OperateConfig.SetValue("Bottle6AlignmentCompensation", "IsEnable", GlobalStaticData.HeightAligmentData6.IsEnable.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle6AlignmentCompensation", "UpCompensation", GlobalStaticData.HeightAligmentData6.UpCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle6AlignmentCompensation", "DownCompensation", GlobalStaticData.HeightAligmentData6.DownCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle6AlignmentCompensation", "BaseHeight​", GlobalStaticData.HeightAligmentData6.BaseHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle6AlignmentCompensation", "PlaceAttr", GlobalStaticData.HeightAligmentData6.PlaceAttr.ToString());
                SaveMouthSizeConfig(GlobalStaticData.HeightAligmentData6, "Bottle6AlignmentCompensation");
                // GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "PlaceCompensation", GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation.ToString());
                GlobalStaticData.SendRobotCoorRefresh = true;
                DisplayMessageHalper.displayMessageSuccesses("瓶型对齐参数6保存成功");
                // UIMessageTip.ShowOk("参数保存成功");

            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"瓶型对齐参数6保存失败{ex.Message}");
                // UIMessageTip.ShowError("参数保存失败");
            }
        }

        private void btn_SaveHeight3_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalStaticData.HeightAligmentData7.IsEnable = cb_BottleEnable3.Checked;
                GlobalStaticData.HeightAligmentData7.UpCompensation = num_UpCompensation3.Value;
                GlobalStaticData.HeightAligmentData7.DownCompensation = num_DownCompensation3.Value;
                GlobalStaticData.HeightAligmentData7.BaseHeight = num_BaseHeight3.Value;
                GlobalStaticData.HeightAligmentData7.PlaceAttr = num_placeAttr3.Value;
                GlobalStaticData.HeightAligmentData7.MouthMinMm = num_MouthMin3.Value;
                GlobalStaticData.HeightAligmentData7.MouthMaxMm = num_MouthMax3.Value;
                //GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation = num_PlaceCompensation1.Value;
                GlobalStaticData.OperateConfig.SetValue("Bottle7AlignmentCompensation", "IsEnable", GlobalStaticData.HeightAligmentData7.IsEnable.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle7AlignmentCompensation", "UpCompensation", GlobalStaticData.HeightAligmentData7.UpCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle7AlignmentCompensation", "DownCompensation", GlobalStaticData.HeightAligmentData7.DownCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle7AlignmentCompensation", "BaseHeight​", GlobalStaticData.HeightAligmentData7.BaseHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle7AlignmentCompensation", "PlaceAttr", GlobalStaticData.HeightAligmentData7.PlaceAttr.ToString());
                SaveMouthSizeConfig(GlobalStaticData.HeightAligmentData7, "Bottle7AlignmentCompensation");
                // GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "PlaceCompensation", GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation.ToString());
                GlobalStaticData.SendRobotCoorRefresh = true;
                DisplayMessageHalper.displayMessageSuccesses("瓶型对齐参数7保存成功");
                // UIMessageTip.ShowOk("参数保存成功");

            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"瓶型对齐参数7保存失败{ex.Message}");
                // UIMessageTip.ShowError("参数保存失败");
            }
        }

        private void btn_SaveHeight4_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalStaticData.HeightAligmentData8.IsEnable = cb_BottleEnable4.Checked;
                GlobalStaticData.HeightAligmentData8.UpCompensation = num_UpCompensation4.Value;
                GlobalStaticData.HeightAligmentData8.DownCompensation = num_DownCompensation4.Value;
                GlobalStaticData.HeightAligmentData8.BaseHeight = num_BaseHeight4.Value;
                GlobalStaticData.HeightAligmentData8.PlaceAttr = num_placeAttr4.Value;
                GlobalStaticData.HeightAligmentData8.MouthMinMm = num_MouthMin4.Value;
                GlobalStaticData.HeightAligmentData8.MouthMaxMm = num_MouthMax4.Value;
                //GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation = num_PlaceCompensation1.Value;
                GlobalStaticData.OperateConfig.SetValue("Bottle8AlignmentCompensation", "IsEnable", GlobalStaticData.HeightAligmentData8.IsEnable.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle8AlignmentCompensation", "UpCompensation", GlobalStaticData.HeightAligmentData8.UpCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle8AlignmentCompensation", "DownCompensation", GlobalStaticData.HeightAligmentData8.DownCompensation.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle8AlignmentCompensation", "BaseHeight​", GlobalStaticData.HeightAligmentData8.BaseHeight.ToString());
                GlobalStaticData.OperateConfig.SetValue("Bottle8AlignmentCompensation", "PlaceAttr", GlobalStaticData.HeightAligmentData8.PlaceAttr.ToString());
                SaveMouthSizeConfig(GlobalStaticData.HeightAligmentData8, "Bottle8AlignmentCompensation");
                // GlobalStaticData.OperateConfig.SetValue("Bottle1AlignmentCompensation", "PlaceCompensation", GlobalStaticData.HeightAligmentData1.PlaceHeightCompeensation.ToString());
                GlobalStaticData.SendRobotCoorRefresh = true;
                DisplayMessageHalper.displayMessageSuccesses("瓶型对齐参数8保存成功");
                // UIMessageTip.ShowOk("参数保存成功");

            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"瓶型对齐参数8保存失败{ex.Message}");
                // UIMessageTip.ShowError("参数保存失败");
            }
        }



        private void cb_BottleEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_BottleEnable.Checked == true)
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

        private void SaveMouthSizeConfig(HeightAligmentData data, string section)
        {
            GlobalStaticData.OperateConfig.SetValue(section, "MouthMinMm", data.MouthMinMm.ToString());
            GlobalStaticData.OperateConfig.SetValue(section, "MouthMaxMm", data.MouthMaxMm.ToString());
        }


    }
}
