using HalconDotNet;
using Microsoft.ML.OnnxRuntime;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class YoloInferenceEngine
    {
        
        private YoloInference _yoloInference;
        public YoloInferData _yoloInferData;
        public InferenceSession onnx_session { get; set; }

        public YoloInferenceEngine(YoloInferData s)
        {
            SessionOptions options = new SessionOptions();

            try
            {
                options.AppendExecutionProvider_CUDA(0);  // 启用 CUDA GPU
                // options.GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL;
                options.GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_EXTENDED; // 改为扩展优化
                options.ExecutionMode = ExecutionMode.ORT_SEQUENTIAL; // 顺序执行，避免异步问题
                options.EnableMemoryPattern = false; // 禁用内存模式，提高稳定性
            }
            catch (Exception)
            {
                try
                {
                    MessageBox.Show("调用GPU失败，启用CPU推理");
                    options.LogSeverityLevel = OrtLoggingLevel.ORT_LOGGING_LEVEL_INFO;
                    options.InterOpNumThreads = 1;
                    options.AppendExecutionProvider_CPU(0);// 设置为CPU上运行
                }
                catch (Exception)
                {
                    MessageBox.Show("启用CPU失败");
                }
            }
            _yoloInference = new YoloInference();
            onnx_session = new InferenceSession(GlobalStaticData.model_path, options);
            _yoloInferData = s;
        }
        public void UpdateConfig(YoloInferData updata)
        {
            _yoloInferData= updata;
        }

        public YoloResult Inference(Mat img)
        {
            // 你原来 Yolo11InferenceTo3D 封装到这里
            _yoloInference.Yolo11InferenceTo3D(img, null, onnx_session, _yoloInferData.conf_threshold,_yoloInferData.nms_threshold,_yoloInferData.class_names,_yoloInferData.input_height,_yoloInferData.input_width,_yoloInferData.class_num,_yoloInferData.box_num,
                out HTuple rows,
                out HTuple cols,
                out HTuple width,
                out HTuple height,
                out HTuple score);

            return new YoloResult(rows, cols, width, height, score);
        }

    }

    public class YoloResult
    {
        public YoloResult(HTuple rows,HTuple cols,HTuple width,HTuple height,HTuple score)
        {
            _rows = rows;
            _cols = cols;
            _width = width;
            _height = height;
            _score = score;
        }
        public HTuple _rows { get; set; }
        public HTuple _cols { get; set; }
        public HTuple _width {  get; set; }
        public HTuple _height {  get; set; }
        public HTuple _score {  get; set; }
    }
}
