using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WideVisualPositionMultCam3D.LoginHelperFile
{

    public static class LoginSessionMonitor
    {
        private static Timer _timer;
        private static TimeSpan _timeout = TimeSpan.FromMinutes(5);

        public static event Action SessionTimeout;

        /// <summary>
        /// 启动会话监控
        /// </summary>
        public static void Start(int timeoutMinutes = 30)
        {
            Stop();

            _timeout = TimeSpan.FromMinutes(timeoutMinutes);

            _timer = new Timer();
            _timer.Interval = 1000; // 1 秒检查一次
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        /// <summary>
        /// 停止会话监控
        /// </summary>
        public static void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Tick -= Timer_Tick;
                _timer.Dispose();
                _timer = null;
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            var idle = NativeMethods.GetIdleTime();
            if (idle >= _timeout)
            {
                Stop();
                OnSessionTimeout();
            }
        }

        private static void OnSessionTimeout()
        {
            // 清空当前用户
            UserContext.Logout();

            // 事件通知（外部可扩展）
            SessionTimeout?.Invoke();
        }
    }
}
