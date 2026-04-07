namespace WideVisualPositionMultCam3D.Page
{
    partial class CalibrationAcq2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Cam1AcqEnabel = new Sunny.UI.UISwitch();
            this.SaveImages = new Sunny.UI.UIButton();
            this.label3 = new System.Windows.Forms.Label();
            this.SaveImageIndex = new Sunny.UI.UIIntegerUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_SaveContinuousStart = new Sunny.UI.UIButton();
            this.label6 = new System.Windows.Forms.Label();
            this.CamContinuousEnable = new Sunny.UI.UISwitch();
            this.ContinuousAcq_Check = new Sunny.UI.UISwitch();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hWindowControl1 = new ChoiceTech.Halcon.Control.HWindow_Final();
            this.hWindowControl2 = new ChoiceTech.Halcon.Control.HWindow_Final();
            this.hWindowControl3 = new ChoiceTech.Halcon.Control.HWindow_Final();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.hWindowControl2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.groupBox3.Location = new System.Drawing.Point(403, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(394, 219);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "相机2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.hWindowControl3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.groupBox2.Location = new System.Drawing.Point(3, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 219);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机3";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Cam1AcqEnabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.SaveImages, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.SaveImageIndex, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_SaveContinuousStart, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.CamContinuousEnable, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.ContinuousAcq_Check, 3, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(403, 228);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(394, 219);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "标定采集";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(6, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "保存图像";
            // 
            // Cam1AcqEnabel
            // 
            this.Cam1AcqEnabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cam1AcqEnabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cam1AcqEnabel.Location = new System.Drawing.Point(89, 19);
            this.Cam1AcqEnabel.MinimumSize = new System.Drawing.Size(1, 1);
            this.Cam1AcqEnabel.Name = "Cam1AcqEnabel";
            this.Cam1AcqEnabel.Size = new System.Drawing.Size(104, 35);
            this.Cam1AcqEnabel.TabIndex = 1;
            this.Cam1AcqEnabel.Text = "uiSwitch1";
            this.Cam1AcqEnabel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.Cam1AcqEnabel.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.Cam1AcqEnabel_ValueChanged);
            this.Cam1AcqEnabel.Click += new System.EventHandler(this.Cam1AcqEnabel_Click);
            // 
            // SaveImages
            // 
            this.SaveImages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveImages.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveImages.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveImages.Location = new System.Drawing.Point(89, 92);
            this.SaveImages.MinimumSize = new System.Drawing.Size(1, 1);
            this.SaveImages.Name = "SaveImages";
            this.SaveImages.Size = new System.Drawing.Size(104, 35);
            this.SaveImages.StyleCustomMode = true;
            this.SaveImages.TabIndex = 2;
            this.SaveImages.Text = "保存1组";
            this.SaveImages.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveImages.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.SaveImages.Click += new System.EventHandler(this.SaveImages_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(6, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "图像计数";
            // 
            // SaveImageIndex
            // 
            this.SaveImageIndex.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveImageIndex.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveImageIndex.Location = new System.Drawing.Point(90, 165);
            this.SaveImageIndex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SaveImageIndex.Maximum = 50;
            this.SaveImageIndex.Minimum = 0;
            this.SaveImageIndex.MinimumSize = new System.Drawing.Size(100, 0);
            this.SaveImageIndex.Name = "SaveImageIndex";
            this.SaveImageIndex.ShowText = false;
            this.SaveImageIndex.Size = new System.Drawing.Size(102, 35);
            this.SaveImageIndex.TabIndex = 3;
            this.SaveImageIndex.Text = null;
            this.SaveImageIndex.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.SaveImageIndex.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.SaveImageIndex.ValueChanged += new Sunny.UI.UIIntegerUpDown.OnValueChanged(this.SaveImageIndex_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(202, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "连续采集";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.label5.Location = new System.Drawing.Point(202, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "训练采集";
            // 
            // btn_SaveContinuousStart
            // 
            this.btn_SaveContinuousStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_SaveContinuousStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveContinuousStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveContinuousStart.Location = new System.Drawing.Point(286, 92);
            this.btn_SaveContinuousStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_SaveContinuousStart.Name = "btn_SaveContinuousStart";
            this.btn_SaveContinuousStart.Size = new System.Drawing.Size(104, 35);
            this.btn_SaveContinuousStart.StyleCustomMode = true;
            this.btn_SaveContinuousStart.TabIndex = 2;
            this.btn_SaveContinuousStart.Text = "采集启动";
            this.btn_SaveContinuousStart.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveContinuousStart.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_SaveContinuousStart.Click += new System.EventHandler(this.btn_SaveContinuousStart_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.label6.Location = new System.Drawing.Point(202, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "保存图像";
            // 
            // CamContinuousEnable
            // 
            this.CamContinuousEnable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CamContinuousEnable.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CamContinuousEnable.Location = new System.Drawing.Point(286, 19);
            this.CamContinuousEnable.MinimumSize = new System.Drawing.Size(1, 1);
            this.CamContinuousEnable.Name = "CamContinuousEnable";
            this.CamContinuousEnable.Size = new System.Drawing.Size(104, 35);
            this.CamContinuousEnable.TabIndex = 1;
            this.CamContinuousEnable.Text = "uiSwitch1";
            this.CamContinuousEnable.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.CamContinuousEnable.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.CamContinuousEnable_ValueChanged);
            this.CamContinuousEnable.Click += new System.EventHandler(this.CamContinuousEnable_Click);
            // 
            // ContinuousAcq_Check
            // 
            this.ContinuousAcq_Check.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ContinuousAcq_Check.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ContinuousAcq_Check.Location = new System.Drawing.Point(286, 165);
            this.ContinuousAcq_Check.MinimumSize = new System.Drawing.Size(1, 1);
            this.ContinuousAcq_Check.Name = "ContinuousAcq_Check";
            this.ContinuousAcq_Check.Size = new System.Drawing.Size(104, 35);
            this.ContinuousAcq_Check.TabIndex = 1;
            this.ContinuousAcq_Check.Text = "uiSwitch1";
            this.ContinuousAcq_Check.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ContinuousAcq_Check.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.ContinuousAcq_Check_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hWindowControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 219);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "相机1";
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Transparent;
            this.hWindowControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.DrawModel = false;
            this.hWindowControl1.EditModel = true;
            this.hWindowControl1.Image = null;
            this.hWindowControl1.Location = new System.Drawing.Point(3, 25);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(388, 191);
            this.hWindowControl1.TabIndex = 0;
            // 
            // hWindowControl2
            // 
            this.hWindowControl2.BackColor = System.Drawing.Color.Transparent;
            this.hWindowControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hWindowControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl2.DrawModel = false;
            this.hWindowControl2.EditModel = true;
            this.hWindowControl2.Image = null;
            this.hWindowControl2.Location = new System.Drawing.Point(3, 25);
            this.hWindowControl2.Name = "hWindowControl2";
            this.hWindowControl2.Size = new System.Drawing.Size(388, 191);
            this.hWindowControl2.TabIndex = 0;
            // 
            // hWindowControl3
            // 
            this.hWindowControl3.BackColor = System.Drawing.Color.Transparent;
            this.hWindowControl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hWindowControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl3.DrawModel = false;
            this.hWindowControl3.EditModel = true;
            this.hWindowControl3.Image = null;
            this.hWindowControl3.Location = new System.Drawing.Point(3, 25);
            this.hWindowControl3.Name = "hWindowControl3";
            this.hWindowControl3.Size = new System.Drawing.Size(388, 191);
            this.hWindowControl3.TabIndex = 0;
            // 
            // CalibrationAcq2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CalibrationAcq2";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "相机组2";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Sunny.UI.UISwitch Cam1AcqEnabel;
        private Sunny.UI.UIButton SaveImages;
        private System.Windows.Forms.Label label3;
        private Sunny.UI.UIIntegerUpDown SaveImageIndex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Sunny.UI.UIButton btn_SaveContinuousStart;
        private System.Windows.Forms.Label label6;
        private Sunny.UI.UISwitch CamContinuousEnable;
        private Sunny.UI.UISwitch ContinuousAcq_Check;
        private System.Windows.Forms.GroupBox groupBox1;
        private ChoiceTech.Halcon.Control.HWindow_Final hWindowControl2;
        private ChoiceTech.Halcon.Control.HWindow_Final hWindowControl3;
        private ChoiceTech.Halcon.Control.HWindow_Final hWindowControl1;
    }
}