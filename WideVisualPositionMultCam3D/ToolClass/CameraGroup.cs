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

        public List<FindCoorData> Process(long endcoding)
        {
            HObject[] images = new HObject[3];
            try
            {
                ApplyConfigUpdate(GlobalStaticData.GetGroupConfig(GroupId));
                // 1. 获取三张图
                lock (cameras)
                {
                    images = cameras.Select(c => c.GetImage()).ToArray();
                }
                if (images[0] != null && images[1]!=null && images[2]!=null)
                {
                    ActionDispImageEvent?.Invoke(images[0].Clone(), images[1].Clone(), images[2].Clone());
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
                List<MouthSizeMm> mouthSizes = BuildCam0MouthSizes(yoloResults[0], rows, cols, cams, GlobalStaticData.GetGroupConfig(GroupId).worldTransformerData);

                // 4. 重建三维
                reconstructor.Reconstruct(rows, cols, cams, indices,out var X, out var Y, out var Z);

                // 5. 世界坐标转换
                return transformer.Transform(X, Y, Z, rows, cols, endcoding, mouthSizes);
               // return new List<FindCoorData>();
            }
            finally
            {
                foreach (HObject image in images)
                    image?.Dispose();
            }
        }

        private List<MouthSizeMm> BuildCam0MouthSizes(YoloResult cam0Result, HTuple rows, HTuple cols, HTuple cams, WorldTransformerData worldTransformerData)
        {
            List<MouthSizeMm> mouthSizes = new List<MouthSizeMm>();
            if (rows == null || cols == null || cams == null)
                return mouthSizes;

            int pointCount = Math.Min(rows.Length, Math.Min(cols.Length, cams.Length)) / 3;

            for (int i = 0; i < pointCount; i++)
            {
                int cam0TupleIndex = FindCamTupleIndex(cams, i, 0);
                if (cam0TupleIndex < 0)
                {
                    mouthSizes.Add(null);
                    continue;
                }

                mouthSizes.Add(TryConvertCam0MouthSize(cam0Result, rows[cam0TupleIndex], cols[cam0TupleIndex], worldTransformerData));
            }

            return mouthSizes;
        }

        private int FindCamTupleIndex(HTuple cams, int pointIndex, int camId)
        {
            int start = pointIndex * 3;
            for (int offset = 0; offset < 3; offset++)
            {
                int tupleIndex = start + offset;
                if (tupleIndex < cams.Length && cams[tupleIndex].I == camId)
                    return tupleIndex;
            }

            return -1;
        }

        private MouthSizeMm TryConvertCam0MouthSize(YoloResult cam0Result, HTuple cam0Row, HTuple cam0Col, WorldTransformerData worldTransformerData)
        {
            try
            {
                int index = FindYoloBoxIndex(cam0Result, cam0Row.D, cam0Col.D);
                if (cam0Result == null || index < 0 || index >= cam0Result._rows.Length)
                    return null;

                if (worldTransformerData == null || worldTransformerData.hv_CamParamData0 == null || worldTransformerData.hv_PlanePose == null)
                    return null;

                return PixelToMmConverter.ConvertYoloCenterBoxToMm(
                    cam0Result._rows[index].D,
                    cam0Result._cols[index].D,
                    cam0Result._width[index].D,
                    cam0Result._height[index].D,
                    worldTransformerData.hv_CamParamData0,
                    worldTransformerData.hv_PlanePose);
            }
            catch (Exception ex)
            {
                LoggerHelper._.Warn($"瓶口尺寸转换失败:{ex.Message}");
                return null;
            }
        }

        private int FindYoloBoxIndex(YoloResult cam0Result, double row, double col)
        {
            if (cam0Result == null ||
                cam0Result._rows == null ||
                cam0Result._cols == null ||
                cam0Result._width == null ||
                cam0Result._height == null)
                return -1;

            int count = Math.Min(
                Math.Min(cam0Result._rows.Length, cam0Result._cols.Length),
                Math.Min(cam0Result._width.Length, cam0Result._height.Length));

            const double tolerance = 0.001;
            for (int i = 0; i < count; i++)
            {
                if (Math.Abs(cam0Result._rows[i].D - row) <= tolerance &&
                    Math.Abs(cam0Result._cols[i].D - col) <= tolerance)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
