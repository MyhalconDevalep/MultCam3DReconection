using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public static class WriteImgaesHelper
    {
        /// <summary>
        /// 保存 HALCON 图像到指定路径
        /// </summary>
        /// <param name="image">HALCON 图像对象</param>
        /// <param name="dirPath">目录路径（不包含文件名）</param>
        /// <param name="fileName">文件名（可带扩展名或不带）</param>
        /// <param name="format">图像格式，如 bmp / jpg / png</param>
        /// <returns>是否保存成功</returns>
        public static bool WriteImage(
            HObject image,
            string dirPath,
            string fileName,
            string format = "bmp")
        {
            if (image == null || !image.IsInitialized())
                return false;

            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                // 拼接完整路径
                string fullPath = Path.Combine(dirPath, fileName);

                HOperatorSet.WriteImage(image, format, 0, fullPath);
                return true;
            }
            catch (Exception ex)
            {
                // 这里你也可以接入日志系统
                // LogHelper.Error("WriteImage failed", ex);
                return false;
            }
        }

        public static bool WriteCalibrationImages(HObject Image, string group, string fileName, string imageName)
        {
           return WriteImage(Image, Application.StartupPath + "\\Images\\CalibrationImages\\"+group+"\\" + fileName + "\\", imageName);
            // GlobalStaticData.HalconAlgorithmFunction.WriteImage(Image, GlobalDataClass._halconDispTool.GetDirectory() + "Images\\AllImage\\", name);

        }


        public static bool WriteAllImages(HObject image,string fileName,string imageName)
        {
            DateTime now = DateTime.Now;
            int year = now.Year;          // 年（如 2025）
            int month = now.Month;        // 月（1-12）
            int day = now.Day;            // 日（1-31）
            int hour = now.Hour;          // 时（0-23）
            int minute = now.Minute;      // 分（0-59）
            int second = now.Second;      // 秒（0-59）
            int millisecond = now.Millisecond; // 毫秒（0-999）
           return WriteImage(image, Application.StartupPath + "\\Images\\AllImages\\" + fileName + "\\", $"{imageName}_{year}-{month}-{day}-{hour}-{minute}-{second}-{millisecond}");
        }

        public static bool WriteGlobalAllImages(HObject image, string imageName)
        {
            DateTime now = DateTime.Now;
            int year = now.Year;          // 年（如 2025）
            int month = now.Month;        // 月（1-12）
            int day = now.Day;            // 日（1-31）
            int hour = now.Hour;          // 时（0-23）
            int minute = now.Minute;      // 分（0-59）
            int second = now.Second;      // 秒（0-59）
            int millisecond = now.Millisecond; // 毫秒（0-999）
            return WriteImage(image, Application.StartupPath + "\\Images\\GlobalAllImages", $"{imageName}_{year}-{month}-{day}-{hour}-{minute}-{second}-{millisecond}");
        }

    }
}
