using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public  class StereoReconstructor
    {
        public  HTuple config;

        public StereoReconstructor(HTuple cfg) => config = cfg;
        public void UpdateConfig(HTuple stereoModelId)
        {
            this.config = stereoModelId;
        }

        public void Reconstruct(HTuple rows, HTuple cols, HTuple cams, HTuple indices,
            out HTuple X, out HTuple Y, out HTuple Z)
        {
            HOperatorSet.ReconstructPointsStereo(
                config,
                rows, cols,
                new HTuple(),
                cams, indices,
                out X, out Y, out Z,
                out HTuple cov, out HTuple idx);
        }
    }
}
