using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.Models
{
    public class HeightAligmentData
    {
        public bool IsEnable {  get; set; }=false;
        public double UpCompensation {  get; set; }
        public double DownCompensation { get; set; }
        public double BaseHeight {  get; set; }
        public double PlaceAttr { get; set; }
        public double PlaceHeightCompeensation {  get; set; }
        public double MouthMinMm { get; set; } = 0;
        public double MouthMaxMm { get; set; } = 9999;
    }
}
