using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.LoginHelperFile
{
    public static class UserContext
    {
        public static User CurrentUser { get; private set; }
   

        public static void SetUser(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
