using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.Models
{
    public class CameraInitData
    {

        public string Number {  get; set; }
        public float Gain { get; set; } = 1;
        public float ExpsureTime { get; set; } = 100;
    }
}
