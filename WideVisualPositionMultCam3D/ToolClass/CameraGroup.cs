using Force.DeepCloner;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class CameraGroup
    {
        public int GroupId { get; }
        public  CameraProcessor[] cameras;
        public  PairMatcher matcher;
        public  StereoReconstructor reconstructor;
        public  WorldTransformer transformer;
        private int _lastConfigVersion = -1;
        public Action<HObject,HObject,HObject> ActionDispImageEvent;
        public Action<YoloResult[]> ActionDispYoloRoiEvent;
     

        public CameraGroup(CameraGroupConfig config)
        {
            GroupId = config.GroupId;
  
            cameras = new[]
            {
            new CameraProcessor(config.Cam0.DeepClone()),
            new CameraProcessor(config.Cam1.DeepClone()),
            new CameraProcessor(config.Cam2.DeepClone()),
        };

            matcher = new PairMatcher(config.findCoorPairsData,config.worldTransformerData,config.hv_StereoModelIDGroup);
            reconstructor = new StereoReconstructor(config.hv_StereoModelIDGroup);
            transformer = new WorldTransformer(config.worldTransformerData.DeepClone());
        }

        private void ApplyConfigUpdate(CameraGroupConfig config)
        {
            if (_lastConfigVersion == config.Version)
                return;

            // 1. 相机参数更新（例如曝光/阈值/ROI 等）
            for (int i = 0; i <cameras.Length; i++)
            {
                cameras[i].UpdateConfig(config.GetCameraConfig(i).DeepClone());
            }
           
            // 2 matcher 配对参数更新
            matcher.UpdateConfig(config.findCoorPairsData,config.worldTransformerData);

            // 3. 3D 重建模型更新
            reconstructor.UpdateConfig(config.hv_StereoModelIDGroup);

            // 4. 世界坐标转换参数更新
            transformer.UpdateConfig(config.worldTransformerData);

     

            _lastConfigVersion = config.Version;
        }

        public List<FindCoorData> Process(int endcoding)
        {
            ApplyConfigUpdate(GlobalStaticData.GetGroupConfig(GroupId));
            // int endcoding = GlobalStaticData.UpdataBingdingDisplayMsgq.Encoding;
            // 1. 获取三张图
            HObject[] images = new HObject[3];
            lock (cameras)
            {
                 images = cameras.Select(c => c.GetImage()).ToArray();
            }
            if (images[0] != null && images[1]!=null && images[2]!=null)
            {
                ActionDispImageEvent?.Invoke(images[0], images[1], images[2]);
            }
            else
            {
                return new List<FindCoorData>();
            }

            //YoloResult yoloResult0=cameras[0].RunInference(images[0]);
            //YoloResult yoloResult1 = cameras[1].RunInference(images[1]);
            //YoloResult yoloResult2 = cameras[2].RunInference(images[2]);

            // 2. 执行 YOLO
            var yoloResults = cameras.Select((c, i) =>
                c.RunInference(images[i].Clone())
            ).ToArray();
            ActionDispYoloRoiEvent?.Invoke(yoloResults);
            // 3. 点对三相机配对
            matcher.Match(yoloResults, out var rows, out var cols, out var cams, out var indices);

            // 4. 重建三维
            reconstructor.Reconstruct(rows, cols, cams, indices,out var X, out var Y, out var Z);

            // 5. 世界坐标转换
            return transformer.Transform(X, Y, Z, rows, cols, endcoding);
           // return new List<FindCoorData>();
        }
    }
}
