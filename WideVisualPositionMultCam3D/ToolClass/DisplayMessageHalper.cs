using HalconDotNet;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public static class DisplayMessageHalper
    {
        public static void displayMessageSuccesses(string message)
        {

            UIMessageTip.ShowOk(message);
            LoggerHelper._.Info(message);

        }

        public static void displayMessageWarning(string message)
        {

            UIMessageTip.ShowWarning(message);
            LoggerHelper._.Warn(message);

        }

        public static void displayMessageErro(string message)
        {

            UIMessageTip.ShowError(message);
            LoggerHelper._.Error(message);

        }

    }
}
