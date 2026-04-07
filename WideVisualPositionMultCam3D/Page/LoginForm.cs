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
using WideVisualPositionMultCam3D.LoginHelperFile;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class LoginForm : UILoginForm
    {
        private readonly AuthService _authService = new AuthService();
        public LoginForm()
        {
            InitializeComponent();
            UserName = "engineer";
        }

        private void FLogin_ButtonLoginClick(object sender, System.EventArgs e)
        {

            var result = _authService.Login(UserName.Trim(), Password);

            ShowSuccessTip( result.Message);

            if (result.Success)
            {
                UserContext.SetUser(result.User);
                GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower=Convert.ToInt32( UserContext.CurrentUser.Role);
                LoginSessionMonitor.Start(30);
                this.Close();
                //var mainForm = new MainForm();
                //mainForm.Show();
            }
            //UserName就是封装了界面里用户名输入框的值
            //Password就是封装了界面里密码输入框的值

        }

 
    }
}
