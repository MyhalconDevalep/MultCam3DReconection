namespace WideVisualPositionMultCam3D.Page
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(3, 34);
            this.lblTitle.Text = "湖北楚大智能装备股份有限公司";
            // 
            // uiPanel1
            // 
            this.uiPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.uiPanel1.Location = new System.Drawing.Point(247, 94);
            this.uiPanel1.Size = new System.Drawing.Size(198, 260);
            // 
            // lblSubText
            // 
            this.lblSubText.ForeColor = System.Drawing.Color.Blue;
            this.lblSubText.Location = new System.Drawing.Point(323, 406);
            this.lblSubText.Size = new System.Drawing.Size(423, 39);
            this.lblSubText.Style = Sunny.UI.UIStyle.Custom;
            this.lblSubText.Text = "HuBei Chuda Intelligent Equipment Co., LTD  V2.01";
            this.lblSubText.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(750, 450);
            this.Name = "LoginForm";
            this.SubText = "HuBei Chuda Intelligent Equipment Co., LTD  V2.01";
            this.Text = "LoginFrom";
            this.Title = "湖北楚大智能装备股份有限公司";
            this.ButtonLoginClick += new System.EventHandler(this.FLogin_ButtonLoginClick);
            this.ResumeLayout(false);

        }

        #endregion
    }
}