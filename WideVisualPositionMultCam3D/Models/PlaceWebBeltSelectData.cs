using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.Models
{
    public class PlaceWebBeltSelectData
    {
        public bool IsEnable { get; set; } = false;
        //网带分割阈值
        public double SegmentationThreshold1Down {  get; set; }
        public double SegmentationThreshold1Up { get; set; }
        public double SegmentationThreshold2Down { get; set; }
        public double SegmentationThreshold2Up { get; set; }
        public double SegmentationThreshold3Down { get; set; }
        public double SegmentationThreshold3Up { get; set; }
        public double SegmentationThreshold4Down { get; set; }
        public double SegmentationThreshold4Up { get; set; }

        public int SegmentationAttri1 { get; set; } = 1;
        public int SegmentationAttri2 { get; set; } = 1;
        public int SegmentationAttri3 { get; set; } = 1;
        public int SegmentationAttri4 { get; set; } = 1;

    }
}
