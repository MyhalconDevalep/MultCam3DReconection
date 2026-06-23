using Sunny.UI;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public static class CalibrationAcqUiStateHelper
    {
        private static readonly Color NormalFillColor = Color.FromArgb(80, 160, 255);
        private static readonly Color NormalHoverColor = Color.FromArgb(115, 179, 255);

        public static void StartContinuousCapture(
            ref bool isRunning,
            ref CancellationTokenSource cancellationTokenSource,
            UIButton startButton,
            Action<CancellationToken> worker)
        {
            if (isRunning)
                return;

            isRunning = true;
            SetContinuousButtonRunning(startButton, true);

            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            Task.Run(() => worker(token), token);
        }

        public static void StopContinuousCapture(
            ref bool isRunning,
            ref CancellationTokenSource cancellationTokenSource,
            UIButton startButton)
        {
            isRunning = false;
            SetContinuousButtonRunning(startButton, false);

            CancellationTokenSource source = cancellationTokenSource;
            cancellationTokenSource = null;

            if (source == null)
                return;

            try
            {
                source.Cancel();
            }
            finally
            {
                source.Dispose();
            }
        }

        public static void SetNormalModeUi(UIButton saveButton, UISwitch continuousSwitch, bool enabled)
        {
            saveButton.Enabled = enabled;
            continuousSwitch.Enabled = !enabled;
        }

        public static void SetSoftModeUi(UIButton startButton, UISwitch normalSwitch, bool enabled)
        {
            startButton.Enabled = enabled;
            normalSwitch.Enabled = !enabled;

            if (!enabled)
            {
                SetContinuousButtonRunning(startButton, false);
            }
        }

        public static void SetContinuousButtonRunning(UIButton button, bool running)
        {
            if (button == null || button.IsDisposed)
                return;

            button.Text = running ? "采集停止" : "采集启动";
            button.FillColor = running ? Color.Red : NormalFillColor;
            button.FillHoverColor = running ? Color.LightPink : NormalHoverColor;
        }
    }
}
