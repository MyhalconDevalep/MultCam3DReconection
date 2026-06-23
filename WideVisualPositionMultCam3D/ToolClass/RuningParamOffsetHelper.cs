using Sunny.UI;
using System;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public static class RuningParamOffsetHelper
    {
        private const string XOffsetKey = "XOffset";
        private const string YOffsetKey = "YOffset";
        private const string ZOffsetKey = "ZOffset";
        private const string RzOffsetKey = "RzOffset";

        public static void LoadCameraOffset(WorldTransformerData data, CameraOffsetControls controls, CameraOffsetState state)
        {
            state.X = data.X_Offset;
            state.Y = data.Y_Offset;
            state.Z = data.Z_Offset;
            state.Rz = data.Rz_Offset;

            controls.X.Value = state.X;
            controls.Y.Value = state.Y;
            controls.Z.Value = state.Z;
            controls.Rz.Value = state.Rz;
        }

        public static void SaveCameraOffset(string section, CameraGroupConfig cameraGroupConfig, CameraOffsetState state)
        {
            if (!CheckSavePermission())
            {
                return;
            }

            try
            {
                SaveOffsetConfig(section, state);
                ApplyCameraOffset(cameraGroupConfig.worldTransformerData, state);
                cameraGroupConfig.Version++;
                DisplayMessageHalper.displayMessageSuccesses("参数保存成功");
            }
            catch (Exception ex)
            {
                DisplayMessageHalper.displayMessageErro($"参数保存失败:{ex.Message}");
            }
        }

        private static void ApplyCameraOffset(WorldTransformerData data, CameraOffsetState state)
        {
            data.X_Offset = state.X;
            data.Y_Offset = state.Y;
            data.Z_Offset = state.Z;
            data.Rz_Offset = state.Rz;
        }

        private static void SaveOffsetConfig(string section, CameraOffsetState state)
        {
            GlobalStaticData.OperateConfig.SetValue(section, XOffsetKey, state.X.ToString());
            GlobalStaticData.OperateConfig.SetValue(section, YOffsetKey, state.Y.ToString());
            GlobalStaticData.OperateConfig.SetValue(section, ZOffsetKey, state.Z.ToString());
            GlobalStaticData.OperateConfig.SetValue(section, RzOffsetKey, state.Rz.ToString());
        }

        private static bool CheckSavePermission()
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower >= 1)
            {
                return true;
            }

            DisplayMessageHalper.displayMessageWarning("当前用户没有权限操作");
            return false;
        }
    }

    public sealed class CameraOffsetState
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Rz { get; set; }
    }

    public sealed class CameraOffsetControls
    {
        public CameraOffsetControls(UIDoubleUpDown x, UIDoubleUpDown y, UIDoubleUpDown z, UIDoubleUpDown rz)
        {
            X = x;
            Y = y;
            Z = z;
            Rz = rz;
        }

        public UIDoubleUpDown X { get; }
        public UIDoubleUpDown Y { get; }
        public UIDoubleUpDown Z { get; }
        public UIDoubleUpDown Rz { get; }
    }
}
