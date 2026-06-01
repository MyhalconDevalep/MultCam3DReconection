namespace WideVisualPositionMultCam3D.Page
{
    partial class Calibration6
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
            this.uiTableLayoutPanel1 = new Sunny.UI.UITableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.hWindowControl3 = new HalconDotNet.HWindowControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.hWindowControl2 = new HalconDotNet.HWindowControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.uiTableLayoutPanel2 = new Sunny.UI.UITableLayoutPanel();
            this.btn_SelectFile = new Sunny.UI.UISymbolButton();
            this.uiSmoothLabel1 = new Sunny.UI.UISmoothLabel();
            this.lb_CalibrationErr = new Sunny.UI.UIMarkLabel();
            this.btn_StarCalibrationCams = new Sunny.UI.UISymbolButton();
            this.btn_SaveCalibration = new Sunny.UI.UISymbolButton();
            this.uiTableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.uiTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTableLayoutPanel1
            // 
            this.uiTableLayoutPanel1.ColumnCount = 2;
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.uiTableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.uiTableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.uiTableLayoutPanel1.Controls.Add(this.uiTableLayoutPanel2, 1, 1);
            this.uiTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiTableLayoutPanel1.Name = "uiTableLayoutPanel1";
            this.uiTableLayoutPanel1.RowCount = 2;
            this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.uiTableLayoutPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTableLayoutPanel1.TabIndex = 3;
            this.uiTableLayoutPanel1.TagString = null;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.hWindowControl3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.groupBox3.Location = new System.Drawing.Point(3, 228);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(394, 219);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "相机3";
            // 
            // hWindowControl3
            // 
            this.hWindowControl3.BackColor = System.Drawing.Color.Black;
            this.hWindowControl3.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl3.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl3.Location = new System.Drawing.Point(3, 25);
            this.hWindowControl3.Name = "hWindowControl3";
            this.hWindowControl3.Size = new System.Drawing.Size(388, 191);
            this.hWindowControl3.TabIndex = 0;
            this.hWindowControl3.WindowSize = new System.Drawing.Size(388, 191);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.hWindowControl2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.groupBox2.Location = new System.Drawing.Point(403, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 219);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机2";
            // 
            // hWindowControl2
            // 
            this.hWindowControl2.BackColor = System.Drawing.Color.Black;
            this.hWindowControl2.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl2.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl2.Location = new System.Drawing.Point(3, 25);
            this.hWindowControl2.Name = "hWindowControl2";
            this.hWindowControl2.Size = new System.Drawing.Size(388, 191);
            this.hWindowControl2.TabIndex = 0;
            this.hWindowControl2.WindowSize = new System.Drawing.Size(388, 191);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hWindowControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 219);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "相机1";
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(3, 25);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(388, 191);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(388, 191);
            // 
            // uiTableLayoutPanel2
            // 
            this.uiTableLayoutPanel2.ColumnCount = 2;
            this.uiTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel2.Controls.Add(this.btn_SelectFile, 0, 1);
            this.uiTableLayoutPanel2.Controls.Add(this.uiSmoothLabel1, 0, 0);
            this.uiTableLayoutPanel2.Controls.Add(this.lb_CalibrationErr, 1, 0);
            this.uiTableLayoutPanel2.Controls.Add(this.btn_StarCalibrationCams, 1, 1);
            this.uiTableLayoutPanel2.Controls.Add(this.btn_SaveCalibration, 1, 2);
            this.uiTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTableLayoutPanel2.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTableLayoutPanel2.Location = new System.Drawing.Point(403, 228);
            this.uiTableLayoutPanel2.Name = "uiTableLayoutPanel2";
            this.uiTableLayoutPanel2.RowCount = 3;
            this.uiTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.uiTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.uiTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.uiTableLayoutPanel2.Size = new System.Drawing.Size(394, 219);
            this.uiTableLayoutPanel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiTableLayoutPanel2.TabIndex = 4;
            this.uiTableLayoutPanel2.TagString = null;
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_SelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SelectFile.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SelectFile.Location = new System.Drawing.Point(33, 89);
            this.btn_SelectFile.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(130, 40);
            this.btn_SelectFile.Style = Sunny.UI.UIStyle.Custom;
            this.btn_SelectFile.Symbol = 61717;
            this.btn_SelectFile.TabIndex = 0;
            this.btn_SelectFile.Text = "文件夹选择";
            this.btn_SelectFile.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SelectFile.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // uiSmoothLabel1
            // 
            this.uiSmoothLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uiSmoothLabel1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSmoothLabel1.Location = new System.Drawing.Point(31, 12);
            this.uiSmoothLabel1.Name = "uiSmoothLabel1";
            this.uiSmoothLabel1.Size = new System.Drawing.Size(134, 49);
            this.uiSmoothLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSmoothLabel1.TabIndex = 2;
            this.uiSmoothLabel1.Text = "标定误差";
            this.uiSmoothLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lb_CalibrationErr
            // 
            this.lb_CalibrationErr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_CalibrationErr.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_CalibrationErr.Location = new System.Drawing.Point(226, 15);
            this.lb_CalibrationErr.MarkPos = Sunny.UI.UIMarkLabel.UIMarkPos.Bottom;
            this.lb_CalibrationErr.Name = "lb_CalibrationErr";
            this.lb_CalibrationErr.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lb_CalibrationErr.Size = new System.Drawing.Size(138, 43);
            this.lb_CalibrationErr.Style = Sunny.UI.UIStyle.Custom;
            this.lb_CalibrationErr.TabIndex = 3;
            this.lb_CalibrationErr.Text = "0";
            this.lb_CalibrationErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_CalibrationErr.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btn_StarCalibrationCams
            // 
            this.btn_StarCalibrationCams.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_StarCalibrationCams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_StarCalibrationCams.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_StarCalibrationCams.Location = new System.Drawing.Point(230, 89);
            this.btn_StarCalibrationCams.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_StarCalibrationCams.Name = "btn_StarCalibrationCams";
            this.btn_StarCalibrationCams.Size = new System.Drawing.Size(130, 40);
            this.btn_StarCalibrationCams.Style = Sunny.UI.UIStyle.Custom;
            this.btn_StarCalibrationCams.TabIndex = 1;
            this.btn_StarCalibrationCams.Text = "开始标定";
            this.btn_StarCalibrationCams.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_StarCalibrationCams.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_StarCalibrationCams.Click += new System.EventHandler(this.btn_StarCalibrationCams_Click);
            // 
            // btn_SaveCalibration
            // 
            this.btn_SaveCalibration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_SaveCalibration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveCalibration.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveCalibration.Location = new System.Drawing.Point(230, 162);
            this.btn_SaveCalibration.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_SaveCalibration.Name = "btn_SaveCalibration";
            this.btn_SaveCalibration.Size = new System.Drawing.Size(130, 40);
            this.btn_SaveCalibration.Style = Sunny.UI.UIStyle.Custom;
            this.btn_SaveCalibration.Symbol = 61639;
            this.btn_SaveCalibration.TabIndex = 1;
            this.btn_SaveCalibration.Text = "保存标定";
            this.btn_SaveCalibration.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveCalibration.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_SaveCalibration.Click += new System.EventHandler(this.btn_SaveCalibration_Click);
            // 
            // Calibration6
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiTableLayoutPanel1);
            this.Name = "Calibration6";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "相机组6";
            this.uiTableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.uiTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private HalconDotNet.HWindowControl hWindowControl3;
        private System.Windows.Forms.GroupBox groupBox2;
        private HalconDotNet.HWindowControl hWindowControl2;
        private System.Windows.Forms.GroupBox groupBox1;
        private HalconDotNet.HWindowControl hWindowControl1;
        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel2;
        private Sunny.UI.UISymbolButton btn_SelectFile;
        private Sunny.UI.UISmoothLabel uiSmoothLabel1;
        private Sunny.UI.UIMarkLabel lb_CalibrationErr;
        private Sunny.UI.UISymbolButton btn_StarCalibrationCams;
        private Sunny.UI.UISymbolButton btn_SaveCalibration;
    }
}