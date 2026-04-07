using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D.Models
{
    public class CameraGroupConfig
    {
        public int GroupId {  get; set; }
        public int Version { get; set; }
        public CameraConfig Cam0 {get;set;}=new CameraConfig();
        public CameraConfig Cam1 { get;set;}=new CameraConfig();
        public CameraConfig Cam2 { get;set;}=new CameraConfig();
        public FindCoorPairsData findCoorPairsData { get;set;}=new FindCoorPairsData();

        public HTuple hv_StereoModelIDGroup;
      //  public HTuple stereoModelId { get; set; } = new HTuple();
        public WorldTransformerData worldTransformerData { get; set; }=new WorldTransformerData();
        public CameraConfig GetCameraConfig(int i)
        {
            switch (i)
            {
                case 0:
                    return Cam0;
                case 1:
                    return Cam1;
                case 2:
                    return Cam2;
                default:
                    return null;
            }
        }

    }
}
