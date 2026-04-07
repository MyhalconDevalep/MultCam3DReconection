using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class PairMatcher
    {
        public  FindCoorPairsData findConfig;
        public WorldTransformerData worldConfig;
        private HTuple hv_stereoModelID;

        public PairMatcher(FindCoorPairsData cfg,WorldTransformerData worldcfg,HTuple hv_StereoModelID)
        {
            hv_stereoModelID=hv_StereoModelID;
            this.findConfig = cfg;
            this.worldConfig = worldcfg;

        }
            
        public void UpdateConfig(FindCoorPairsData updataFind,WorldTransformerData updataWorld)
        {
            this.findConfig = updataFind;
            this.worldConfig=updataWorld;
        }

        public void Match(YoloResult[] res, out HTuple allRows, out HTuple allCols, out HTuple allCams, out HTuple allIndices)
        {
            GlobalStaticData.HalconAlgorithmFunction.Find_coordinate_pairs(
                res[0]._rows, res[1]._rows, res[2]._rows,
                res[0]._cols, res[1]._cols, res[2]._cols,
                findConfig.PositionTolerance,
                findConfig.hv_ZTolerance,
                findConfig.hv_XYTolerance,
                hv_stereoModelID,
                worldConfig.hv_PlanePose,
                worldConfig.hv_CamParamData0,
                findConfig.hv_CamParamData1,
                findConfig.hv_CamParamData2,
                findConfig.hv_InvertToCamMat0,
                findConfig.hv_InvertToCamMat1,
                findConfig.hv_InvertToCamMat2,
                out var triples, out allRows, out allCols, out allCams, out allIndices);
        }

    }
}
