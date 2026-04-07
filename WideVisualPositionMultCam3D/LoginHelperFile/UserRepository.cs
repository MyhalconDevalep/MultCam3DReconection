using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.LoginHelperFile
{
    public class UserRepository
    {
        private readonly List<User> _users = new List<User>
 {
     new User
     {
         UserName = "operator",
         PasswordHash = PasswordHelper.Hash("123"),
         Role = UserRole.Operator,
         IsEnabled = true
     },
     new User
     {
         UserName = "engineer",
         PasswordHash = PasswordHelper.Hash("123456"),
         Role = UserRole.Engineer,
         IsEnabled = true
     },
     new User
     {
         UserName = "admin",
         PasswordHash = PasswordHelper.Hash("admin"),
         Role = UserRole.Admin,
         IsEnabled = true
     }
 };

        public User Find(string userName)
        {
            return _users.FirstOrDefault(u => u.UserName == userName);
        }
    }
}
