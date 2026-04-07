using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.Models
{
    public class YoloInferData
    {
       // public InferenceSession onnx_session { get; set; }
        public float conf_threshold {  get; set; }  
        public float nms_threshold { get; set; }
        public string[] class_names {  get; set; }= File.ReadAllLines(GlobalStaticData.classer_path, Encoding.UTF8);
        public int input_height { get; set; } = 640;
        public int input_width { get; set; } = 640;
        public int class_num { get; set; } = 1;
        public int box_num { get; set; } = 8400;
    }
}
