using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.LoginHelperFile
{
    public class AuthService
    {
        private readonly UserRepository _repo = new UserRepository();

        public LoginResult Login(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "用户名或密码不能为空"
                };
            }

            var user = _repo.Find(userName);
            if (user == null)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "用户不存在"
                };
            }

            if (!user.IsEnabled)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "账号已被禁用"
                };
            }

            var hash = PasswordHelper.Hash(password);
            if (hash != user.PasswordHash)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "密码错误"
                };
            }

            return new LoginResult
            {
                Success = true,
                Message = "登录成功",
                User = user
            };
        }
    }
}
