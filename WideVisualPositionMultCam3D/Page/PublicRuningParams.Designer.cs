namespace WideVisualPositionMultCam3D.Page
{
    partial class PublicRuningParams
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.num_Calib_Board_H = new Sunny.UI.UIDoubleUpDown();
            this.num_PositionTolerance = new Sunny.UI.UIDoubleUpDown();
            this.num_CofThreshold = new Sunny.UI.UIDoubleUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_SaveConfig = new Sunny.UI.UISymbolButton();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 450);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "定位参数";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.num_CofThreshold, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.num_PositionTolerance, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.num_Calib_Board_H, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.btn_SaveConfig, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 422);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "标定板高(mm)";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "定位容差";
            // 
            // num_Calib_Board_H
            // 
            this.num_Calib_Board_H.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.num_Calib_Board_H.Decimal = 0;
            this.num_Calib_Board_H.DecimalPlaces = 0;
            this.num_Calib_Board_H.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_Calib_Board_H.HasMaximum = true;
            this.num_Calib_Board_H.HasMinimum = true;
            this.num_Calib_Board_H.Location = new System.Drawing.Point(239, 235);
            this.num_Calib_Board_H.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_Calib_Board_H.Maximum = 500D;
            this.num_Calib_Board_H.MaximumEnabled = true;
            this.num_Calib_Board_H.Minimum = -10D;
            this.num_Calib_Board_H.MinimumEnabled = true;
            this.num_Calib_Board_H.MinimumSize = new System.Drawing.Size(100, 0);
            this.num_Calib_Board_H.Name = "num_Calib_Board_H";
            this.num_Calib_Board_H.ShowText = false;
            this.num_Calib_Board_H.Size = new System.Drawing.Size(116, 29);
            this.num_Calib_Board_H.Step = 1D;
            this.num_Calib_Board_H.TabIndex = 2;
            this.num_Calib_Board_H.Text = "uiDoubleUpDown1";
            this.num_Calib_Board_H.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.num_Calib_Board_H.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.num_Calib_Board_H.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.num_Calib_Board_H_ValueChanged);
            // 
            // num_PositionTolerance
            // 
            this.num_PositionTolerance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.num_PositionTolerance.Decimal = 0;
            this.num_PositionTolerance.DecimalPlaces = 0;
            this.num_PositionTolerance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_PositionTolerance.HasMaximum = true;
            this.num_PositionTolerance.HasMinimum = true;
            this.num_PositionTolerance.Location = new System.Drawing.Point(239, 135);
            this.num_PositionTolerance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_PositionTolerance.Maximum = 100D;
            this.num_PositionTolerance.MaximumEnabled = true;
            this.num_PositionTolerance.Minimum = 1D;
            this.num_PositionTolerance.MinimumEnabled = true;
            this.num_PositionTolerance.MinimumSize = new System.Drawing.Size(100, 0);
            this.num_PositionTolerance.Name = "num_PositionTolerance";
            this.num_PositionTolerance.ShowText = false;
            this.num_PositionTolerance.Size = new System.Drawing.Size(116, 29);
            this.num_PositionTolerance.Step = 1D;
            this.num_PositionTolerance.TabIndex = 2;
            this.num_PositionTolerance.Text = "uiDoubleUpDown1";
            this.num_PositionTolerance.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.num_PositionTolerance.Value = 1D;
            this.num_PositionTolerance.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.num_PositionTolerance.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.num_PositionTolerance_ValueChanged);
            // 
            // num_CofThreshold
            // 
            this.num_CofThreshold.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.num_CofThreshold.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_CofThreshold.HasMaximum = true;
            this.num_CofThreshold.HasMinimum = true;
            this.num_CofThreshold.Location = new System.Drawing.Point(239, 35);
            this.num_CofThreshold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_CofThreshold.Maximum = 1D;
            this.num_CofThreshold.MaximumEnabled = true;
            this.num_CofThreshold.Minimum = 0.1D;
            this.num_CofThreshold.MinimumEnabled = true;
            this.num_CofThreshold.MinimumSize = new System.Drawing.Size(100, 0);
            this.num_CofThreshold.Name = "num_CofThreshold";
            this.num_CofThreshold.ShowText = false;
            this.num_CofThreshold.Size = new System.Drawing.Size(116, 29);
            this.num_CofThreshold.TabIndex = 2;
            this.num_CofThreshold.Text = "uiDoubleUpDown1";
            this.num_CofThreshold.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.num_CofThreshold.Value = 0.1D;
            this.num_CofThreshold.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.num_CofThreshold.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.num_CofThreshold_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "置信阈值";
            // 
            // btn_SaveConfig
            // 
            this.btn_SaveConfig.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_SaveConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveConfig.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveConfig.Location = new System.Drawing.Point(247, 332);
            this.btn_SaveConfig.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_SaveConfig.Name = "btn_SaveConfig";
            this.btn_SaveConfig.Size = new System.Drawing.Size(100, 35);
            this.btn_SaveConfig.Symbol = 61639;
            this.btn_SaveConfig.TabIndex = 3;
            this.btn_SaveConfig.Text = "保存";
            this.btn_SaveConfig.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveConfig.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_SaveConfig.Click += new System.EventHandler(this.btn_SaveConfig_Click);
            // 
            // PublicRuningParams
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "PublicRuningParams";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "公共参数";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Sunny.UI.UIDoubleUpDown num_CofThreshold;
        private Sunny.UI.UIDoubleUpDown num_PositionTolerance;
        private Sunny.UI.UIDoubleUpDown num_Calib_Board_H;
        private Sunny.UI.UISymbolButton btn_SaveConfig;
    }
}