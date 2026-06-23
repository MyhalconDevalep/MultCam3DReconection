using HalconDotNet;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public  class CameraProcessor
    {
        private  int config;
        private YoloInferenceEngine yolo;

        public CameraProcessor(CameraConfig cfg)
        {
            config = cfg.CamId;
            yolo = new YoloInferenceEngine(cfg.YoloInferData);
        }

        public void UpdateConfig(CameraConfig cfg)
        {
            config = cfg.CamId;   // 整合新参数即可
            yolo.UpdateConfig(cfg.YoloInferData);
        }

        public HObject GetImage()
        {

            if (GlobalStaticData._imageBuffer.TryGetValue(config, out var img)&& img != null && img.IsInitialized())
            {
                return img.Clone(); // 🔒 立刻切断生命周期
            }
            return null;
        }

        public YoloResult RunInference(HObject img)
        {
            Mat mat = null;
            try
            {
                mat = GlobalStaticData.displayConvert.HObjectToMatC(img);
                return yolo.Inference(mat);
            }
            finally
            {
                mat?.Dispose();
                img?.Dispose();
            }
        }
    }

    public class CameraConfig
    {
        public YoloInferData YoloInferData=new YoloInferData();
        public int CamId { get; set; }
    }
}
