using HalconDotNet;
using System;
using System.IO;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public static class CalibrationFolderLoader
    {
        public static bool TryLoad(
            string dir,
            CameraGroupConfig cameraGroupConfig,
            HWindowControl window0,
            HWindowControl window1,
            HWindowControl window2,
            out string warning)
        {
            warning = string.Empty;

            if (string.IsNullOrWhiteSpace(dir))
            {
                warning = "请选择标定文件夹";
                return false;
            }

            if (!Directory.Exists(dir))
            {
                warning = $"标定文件夹不存在：{dir}";
                return false;
            }

            if (cameraGroupConfig?.findCoorPairsData == null)
            {
                warning = "相机组标定配置未初始化";
                return false;
            }

            string image0Path = Path.Combine(dir, "0", "1_0.bmp");
            string image1Path = Path.Combine(dir, "1", "2_0.bmp");
            string image2Path = Path.Combine(dir, "2", "3_0.bmp");
            string caltabPath = Path.Combine(dir, "caltab_240mm.descr");

            if (!File.Exists(image0Path))
            {
                warning = $"缺少标定图片：{image0Path}";
                return false;
            }

            if (!File.Exists(image1Path))
            {
                warning = $"缺少标定图片：{image1Path}";
                return false;
            }

            if (!File.Exists(image2Path))
            {
                warning = $"缺少标定图片：{image2Path}";
                return false;
            }

            if (!File.Exists(caltabPath))
            {
                warning = $"缺少标定描述文件：{caltabPath}";
                return false;
            }

            HObject image0 = null;
            HObject image1 = null;
            HObject image2 = null;

            try
            {
                HOperatorSet.ReadImage(out image0, image0Path);
                HOperatorSet.ReadImage(out image1, image1Path);
                HOperatorSet.ReadImage(out image2, image2Path);
                HOperatorSet.GetImageSize(image1, out HTuple width, out HTuple height);

                GlobalStaticData.HalconAlgorithmFunction.Calibration_model_Init(
                    0.006,
                    0.00000345,
                    0.00000345,
                    width,
                    height,
                    caltabPath,
                    out cameraGroupConfig.findCoorPairsData.hv_CalibDataID);

                GlobalStaticData.displayConvert.SetHalconScalingZoom(image0, window0.HalconWindow);
                GlobalStaticData.displayConvert.SetHalconScalingZoom(image1, window1.HalconWindow);
                GlobalStaticData.displayConvert.SetHalconScalingZoom(image2, window2.HalconWindow);
                window0.HalconWindow.DispObj(image0);
                window1.HalconWindow.DispObj(image1);
                window2.HalconWindow.DispObj(image2);

                return true;
            }
            catch (HalconException ex)
            {
                warning = $"读取标定文件失败：{ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                warning = $"打开标定文件夹失败：{ex.Message}";
                return false;
            }
            finally
            {
                image0?.Dispose();
                image1?.Dispose();
                image2?.Dispose();
            }
        }

        public static bool TrySave(CameraGroupConfig cameraGroupConfig, int groupIndex, out string warning)
        {
            warning = string.Empty;

            if (cameraGroupConfig?.findCoorPairsData == null || cameraGroupConfig.worldTransformerData == null)
            {
                warning = "相机组标定配置未初始化";
                return false;
            }

            string calibrationDir = Path.Combine(GlobalStaticData.WriteCalibrationPath, "Calibration");
            string calibrationPath = Path.Combine(calibrationDir, $"calibration_data{groupIndex}.cal");

            try
            {
                Directory.CreateDirectory(calibrationDir);

                bool res = GlobalStaticData.HalconAlgorithmFunction.Write_calibration_data(
                    cameraGroupConfig.findCoorPairsData.hv_CalibDataID,
                    calibrationPath,
                    out cameraGroupConfig.worldTransformerData.hv_CamParamData0,
                    out cameraGroupConfig.findCoorPairsData.hv_CamPose0,
                    out cameraGroupConfig.findCoorPairsData.hv_CamParamData1,
                    out cameraGroupConfig.findCoorPairsData.hv_CamPose1,
                    out cameraGroupConfig.findCoorPairsData.hv_CamParamData2,
                    out cameraGroupConfig.findCoorPairsData.hv_CamPose2,
                    out cameraGroupConfig.worldTransformerData.hv_World2CamMat0,
                    out cameraGroupConfig.findCoorPairsData.hv_InvertToCamMat0,
                    out cameraGroupConfig.findCoorPairsData.hv_InvertToCamMat1,
                    out cameraGroupConfig.findCoorPairsData.hv_InvertToCamMat2,
                    out cameraGroupConfig.worldTransformerData.hv_PlanePose,
                    out cameraGroupConfig.findCoorPairsData.hv_CameraSetupModel,
                    out cameraGroupConfig.hv_StereoModelIDGroup);

                if (!res)
                    warning = "标定保存失败";

                return res;
            }
            catch (HalconException ex)
            {
                warning = $"标定保存失败：{ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                warning = $"标定保存失败：{ex.Message}";
                return false;
            }
        }

        public static bool TryLoadCalibrationData(CameraGroupConfig cameraGroupConfig, int groupIndex, out string warning)
        {
            warning = string.Empty;

            if (cameraGroupConfig?.findCoorPairsData == null || cameraGroupConfig.worldTransformerData == null)
            {
                warning = "相机组标定配置未初始化";
                return false;
            }

            string calibrationPath = Path.Combine(
                GlobalStaticData.WriteCalibrationPath,
                "Calibration",
                $"calibration_data{groupIndex}.cal");

            try
            {
                bool res = GlobalStaticData.HalconAlgorithmFunction.Load_calibration_data(
                    calibrationPath,
                    out cameraGroupConfig.findCoorPairsData.hv_CalibDataID,
                    out cameraGroupConfig.worldTransformerData.hv_CamParamData0,
                    out cameraGroupConfig.findCoorPairsData.hv_CamPose0,
                    out cameraGroupConfig.findCoorPairsData.hv_CamParamData1,
                    out cameraGroupConfig.findCoorPairsData.hv_CamPose1,
                    out cameraGroupConfig.findCoorPairsData.hv_CamParamData2,
                    out cameraGroupConfig.findCoorPairsData.hv_CamPose2,
                    out cameraGroupConfig.worldTransformerData.hv_World2CamMat0,
                    out cameraGroupConfig.findCoorPairsData.hv_InvertToCamMat0,
                    out cameraGroupConfig.findCoorPairsData.hv_InvertToCamMat1,
                    out cameraGroupConfig.findCoorPairsData.hv_InvertToCamMat2,
                    out cameraGroupConfig.worldTransformerData.hv_PlanePose,
                    out cameraGroupConfig.findCoorPairsData.hv_CameraSetupModel,
                    out cameraGroupConfig.hv_StereoModelIDGroup);

                if (!res)
                    warning = $"相机组{groupIndex}标定参数加载失败";

                return res;
            }
            catch (HalconException ex)
            {
                warning = $"相机组{groupIndex}标定参数加载失败：{ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                warning = $"相机组{groupIndex}标定参数加载失败：{ex.Message}";
                return false;
            }
        }
    }
}
