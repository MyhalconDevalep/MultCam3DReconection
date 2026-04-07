using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.Models
{
    public class WorldTransformerData
    {


        private double _boardHeight;

        public double BoardHeight
        {
            get { return _boardHeight; }
            set { 
                _boardHeight = value; 
            }
        }

        public HTuple hv_PlanePose;
        public HTuple hv_World2CamMat0;
        public HTuple hv_CamParamData0;
        public double X_Offset { get; set; }
        public double Y_Offset { get; set; }
        public double Z_Offset { get; set; }
        public double Rz_Offset { get; set; }

    }
}
