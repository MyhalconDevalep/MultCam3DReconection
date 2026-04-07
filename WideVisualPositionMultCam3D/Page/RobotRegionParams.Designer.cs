namespace WideVisualPositionMultCam3D.Page
{
    partial class RobotRegionParams
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.num_Robot1Threshold = new Sunny.UI.UIDoubleUpDown();
            this.num_BottleTolerance = new Sunny.UI.UIDoubleUpDown();
            this.num_MinHeight = new Sunny.UI.UIDoubleUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.num_SafelyDisp = new Sunny.UI.UIDoubleUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.num_X_CommandPoint = new Sunny.UI.UIDoubleUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_SaveConfig = new Sunny.UI.UISymbolButton();
            this.num_MaxHeight = new Sunny.UI.UIDoubleUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1000, 650);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "坐标筛选";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.num_BottleTolerance, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.num_MinHeight, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.num_Robot1Threshold, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.num_X_CommandPoint, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.num_SafelyDisp, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.num_MaxHeight, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.btn_SaveConfig, 3, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(994, 622);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "瓶间容差(mm)";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(583, 239);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "臂一阈值";
            // 
            // num_Robot1Threshold
            // 
            this.num_Robot1Threshold.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.num_Robot1Threshold.Decimal = 0;
            this.num_Robot1Threshold.DecimalPlaces = 0;
            this.num_Robot1Threshold.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_Robot1Threshold.HasMaximum = true;
            this.num_Robot1Threshold.HasMinimum = true;
            this.num_Robot1Threshold.Location = new System.Drawing.Point(811, 235);
            this.num_Robot1Threshold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_Robot1Threshold.Maximum = 4000D;
            this.num_Robot1Threshold.MaximumEnabled = true;
            this.num_Robot1Threshold.Minimum = -1000D;
            this.num_Robot1Threshold.MinimumEnabled = true;
            this.num_Robot1Threshold.MinimumSize = new System.Drawing.Size(100, 0);
            this.num_Robot1Threshold.Name = "num_Robot1Threshold";
            this.num_Robot1Threshold.ShowText = false;
            this.num_Robot1Threshold.Size = new System.Drawing.Size(116, 29);
            this.num_Robot1Threshold.Step = 1D;
            this.num_Robot1Threshold.TabIndex = 2;
            this.num_Robot1Threshold.Text = "uiDoubleUpDown1";
            this.num_Robot1Threshold.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.num_Robot1Threshold.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.num_Robot1Threshold.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.num_Robot1Threshold_ValueChanged);
            // 
            // num_BottleTolerance
            // 
            this.num_BottleTolerance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.num_BottleTolerance.Decimal = 0;
            this.num_BottleTolerance.DecimalPlaces = 0;
            this.num_BottleTolerance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_BottleTolerance.HasMaximum = true;
            this.num_BottleTolerance.HasMinimum = true;
            this.num_BottleTolerance.Location = new System.Drawing.Point(314, 35);
            this.num_BottleTolerance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_BottleTolerance.Maximum = 200D;
            this.num_BottleTolerance.MaximumEnabled = true;
            this.num_BottleTolerance.Minimum = 0D;
            this.num_BottleTolerance.MinimumEnabled = true;
            this.num_BottleTolerance.MinimumSize = new System.Drawing.Size(100, 0);
            this.num_BottleTolerance.Name = "num_BottleTolerance";
            this.num_BottleTolerance.ShowText = false;
            this.num_BottleTolerance.Size = new System.Drawing.Size(116, 29);
            this.num_BottleTolerance.Step = 1D;
            this.num_BottleTolerance.TabIndex = 2;
            this.num_BottleTolerance.Text = "uiDoubleUpDown1";
            this.num_BottleTolerance.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.num_BottleTolerance.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.num_BottleTolerance.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.num_BottleTolerance_ValueChanged);
            // 
            // num_MinHeight
            // 
            this.num_MinHeight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.num_MinHeight.Decimal = 0;
            this.num_MinHeight.DecimalPlaces = 0;
            this.num_MinHeight.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_MinHeight.HasMaximum = true;
            this.num_MinHeight.HasMinimum = true;
            this.num_MinHeight.Location = new System.Drawing.Point(314, 135);
            this.num_MinHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_MinHeight.Maximum = 500D;
            this.num_MinHeight.MaximumEnabled = true;
            this.num_MinHeight.Minimum = 0D;
            this.num_MinHeight.MinimumEnabled = true;
            this.num_MinHeight.MinimumSize = new System.Drawing.Size(100, 0);
            this.num_MinHeight.Name = "num_MinHeight";
            this.num_MinHeight.ShowText = false;
            this.num_MinHeight.Size = new System.Drawing.Size(116, 29);
            this.num_MinHeight.Step = 1D;
            this.num_MinHeight.TabIndex = 2;
            this.num_MinHeight.Text = "uiDoubleUpDown1";
            this.num_MinHeight.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.num_MinHeight.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.num_MinHeight.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.num_MinHeight_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(87, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "最小高度";
            // 
            // num_SafelyDisp
            // 
            this.num_SafelyDisp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.num_SafelyDisp.Decimal = 0;
            this.num_SafelyDisp.DecimalPlaces = 0;
            this.num_SafelyDisp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_SafelyDisp.HasMaximum = true;
            this.num_SafelyDisp.HasMinimum = true;
            this.num_SafelyDisp.Location = new System.Drawing.Point(811, 135);
            this.num_SafelyDisp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_SafelyDisp.Maximum = 1000D;
            this.num_SafelyDisp.MaximumEnabled = true;
            this.num_SafelyDisp.Minimum = 0D;
            this.num_SafelyDisp.MinimumEnabled = true;
            this.num_SafelyDisp.MinimumSize = new System.Drawing.Size(100, 0);
            this.num_SafelyDisp.Name = "num_SafelyDisp";
            this.num_SafelyDisp.ShowText = false;
            this.num_SafelyDisp.Size = new System.Drawing.Size(116, 29);
            this.num_SafelyDisp.Step = 1D;
            this.num_SafelyDisp.TabIndex = 2;
            this.num_SafelyDisp.Text = "uiDoubleUpDown1";
            this.num_SafelyDisp.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.num_SafelyDisp.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.num_SafelyDisp.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.num_SafelyDisp_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(583, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "安全间距";
            // 
            // num_X_CommandPoint
            // 
            this.num_X_CommandPoint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.num_X_CommandPoint.Decimal = 0;
            this.num_X_CommandPoint.DecimalPlaces = 0;
            this.num_X_CommandPoint.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_X_CommandPoint.HasMaximum = true;
            this.num_X_CommandPoint.HasMinimum = true;
            this.num_X_CommandPoint.Location = new System.Drawing.Point(811, 35);
            this.num_X_CommandPoint.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_X_CommandPoint.Maximum = 2000D;
            this.num_X_CommandPoint.MaximumEnabled = true;
            this.num_X_CommandPoint.Minimum = -2000D;
            this.num_X_CommandPoint.MinimumEnabled = true;
            this.num_X_CommandPoint.MinimumSize = new System.Drawing.Size(100, 0);
            this.num_X_CommandPoint.Name = "num_X_CommandPoint";
            this.num_X_CommandPoint.ShowText = false;
            this.num_X_CommandPoint.Size = new System.Drawing.Size(116, 29);
            this.num_X_CommandPoint.Step = 1D;
            this.num_X_CommandPoint.TabIndex = 2;
            this.num_X_CommandPoint.Text = "uiDoubleUpDown1";
            this.num_X_CommandPoint.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.num_X_CommandPoint.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.num_X_CommandPoint.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.num_X_CommandPoint_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(586, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "X发令点";
            // 
            // btn_SaveConfig
            // 
            this.btn_SaveConfig.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_SaveConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveConfig.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveConfig.Location = new System.Drawing.Point(819, 332);
            this.btn_SaveConfig.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_SaveConfig.Name = "btn_SaveConfig";
            this.btn_SaveConfig.Size = new System.Drawing.Size(100, 35);
            this.btn_SaveConfig.Symbol = 61639;
            this.btn_SaveConfig.TabIndex = 4;
            this.btn_SaveConfig.Text = "保存";
            this.btn_SaveConfig.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveConfig.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_SaveConfig.Click += new System.EventHandler(this.btn_SaveConfig_Click);
            // 
            // num_MaxHeight
            // 
            this.num_MaxHeight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.num_MaxHeight.Decimal = 0;
            this.num_MaxHeight.DecimalPlaces = 0;
            this.num_MaxHeight.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_MaxHeight.HasMaximum = true;
            this.num_MaxHeight.HasMinimum = true;
            this.num_MaxHeight.Location = new System.Drawing.Point(314, 235);
            this.num_MaxHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_MaxHeight.Maximum = 500D;
            this.num_MaxHeight.MaximumEnabled = true;
            this.num_MaxHeight.Minimum = 0D;
            this.num_MaxHeight.MinimumEnabled = true;
            this.num_MaxHeight.MinimumSize = new System.Drawing.Size(100, 0);
            this.num_MaxHeight.Name = "num_MaxHeight";
            this.num_MaxHeight.ShowText = false;
            this.num_MaxHeight.Size = new System.Drawing.Size(116, 29);
            this.num_MaxHeight.Step = 1D;
            this.num_MaxHeight.TabIndex = 6;
            this.num_MaxHeight.Text = "uiDoubleUpDown1";
            this.num_MaxHeight.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.num_MaxHeight.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.num_MaxHeight.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.num_MaxHeight_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "最大高度";
            // 
            // RobotRegionParams
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.groupBox2);
            this.Name = "RobotRegionParams";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "坐标控制";
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private Sunny.UI.UIDoubleUpDown num_Robot1Threshold;
        private Sunny.UI.UIDoubleUpDown num_BottleTolerance;
        private Sunny.UI.UIDoubleUpDown num_MinHeight;
        private System.Windows.Forms.Label label8;
        private Sunny.UI.UIDoubleUpDown num_SafelyDisp;
        private System.Windows.Forms.Label label6;
        private Sunny.UI.UIDoubleUpDown num_X_CommandPoint;
        private System.Windows.Forms.Label label5;
        private Sunny.UI.UISymbolButton btn_SaveConfig;
        private Sunny.UI.UIDoubleUpDown num_MaxHeight;
        private System.Windows.Forms.Label label1;
    }
}