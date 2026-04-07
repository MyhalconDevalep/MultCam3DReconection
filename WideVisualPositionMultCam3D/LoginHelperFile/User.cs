using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.LoginHelperFile
{
    public enum UserRole
    {
        Operator,   // 操作员
        Engineer,   // 工程师
        Admin       // 管理员
    }
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class User
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public bool IsEnabled { get; set; }
    }
}
