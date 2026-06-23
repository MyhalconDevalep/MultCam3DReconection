using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class WorldTransformer
    {
        private  WorldTransformerData config;

        public WorldTransformer(WorldTransformerData cfg) => config = cfg;
        public void UpdateConfig(WorldTransformerData data)
        {
            this.config = data;
        }

        public List<FindCoorData> Transform(HTuple X, HTuple Y, HTuple Z,HTuple rowImg, HTuple colImg,long Encoding)
        {
            return Transform(X, Y, Z, rowImg, colImg, Encoding, null);
        }

        public List<FindCoorData> Transform(HTuple X, HTuple Y, HTuple Z,HTuple rowImg, HTuple colImg,long Encoding, IList<MouthSizeMm> mouthSizes)
        {
            GlobalStaticData.HalconAlgorithmFunction.Coordinate_Transformation_Result(
                X, Y, Z,
                config.BoardHeight/1000*-1,
                config.Rz_Offset,
                config.hv_PlanePose,
                config.hv_World2CamMat0,
                config.hv_CamParamData0,
                out var row, out var col,
                out var x_mm, out var y_mm, out var z_mm);

            List<FindCoorData> list = new List<FindCoorData>();

            for (int i = 0; i < X.Length; i++)
            {
                MouthSizeMm mouthSize = mouthSizes != null && i < mouthSizes.Count ? mouthSizes[i] : null;

                list.Add(new FindCoorData
                {
                    pixelRow = row[i],
                    pixelCol = col[i],
                    WorldX = y_mm[i] + config.X_Offset,
                    WorldY = x_mm[i] + config.Y_Offset,
                    Height = z_mm[i] + config.Z_Offset,
                    // WorldXScurren = Encoding - y_mm[i],
                    WorldXScurren = Encoding - y_mm[i]- config.X_Offset,
                    encoding = Encoding,
                    MouthWidthMm = mouthSize?.WidthMm ?? -1,
                    MouthHeightMm = mouthSize?.HeightMm ?? -1,
                    MouthAverageDiameterMm = mouthSize?.AverageDiameterMm ?? -1
                }); 
            }

            return list;
        }
    }
}
