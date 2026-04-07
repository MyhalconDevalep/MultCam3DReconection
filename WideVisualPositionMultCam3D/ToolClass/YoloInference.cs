
using HalconDotNet;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class YoloInference
    {

        public unsafe float[] Transpose(float[] tensorData, int rows, int cols)
        {
            float[] transposedTensorData = new float[tensorData.Length];

            fixed (float* pTensorData = tensorData)
            {
                fixed (float* pTransposedData = transposedTensorData)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            int index = i * cols + j;
                            int transposedIndex = j * rows + i;
                            pTransposedData[transposedIndex] = pTensorData[index];
                        }
                    }
                }
            }
            return transposedTensorData;
        }


        /// <summary>
        /// 根据输入值相对于最大值和最小值的位置进行补偿
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="maxCompensation">最大补偿值</param>
        /// <returns>补偿后的值</returns>
        private double ApplyCompensation(int value, int minValue, int maxValue, double maxCompensation)
        {
            // 计算中间值
            double midValue = (minValue + maxValue) / 2.0;

            // 计算输入值与中间值的差值
            double diffFromMid = value - midValue;

            // 计算距离最大值和最小值的距离
            double distanceToMax = maxValue - midValue;
            double distanceToMin = midValue - minValue;

            // 计算补偿因子
            double compensationFactor;
            if (diffFromMid > 0)
            {
                // 输入值大于中间值，向大补偿
                compensationFactor = (value - midValue) / distanceToMax * maxCompensation;
            }
            else if (diffFromMid < 0)
            {
                // 输入值小于中间值，向小补偿
                compensationFactor = (midValue - value) / distanceToMin * maxCompensation;
            }
            else
            {
                // 输入值等于中间值，不补偿
                return value;
            }

            // 应用补偿
            double compensatedValue = value + compensationFactor * Math.Sign(diffFromMid);

            // 特殊处理：当输入值等于最小值时，直接减去最大补偿值
            if (value == minValue)
            {
                compensatedValue = minValue - maxCompensation;
            }

            return compensatedValue;
        }

        /// <summary>
        /// 根据输入值相对于最小值和最大值的位置计算比例值
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>按比例计算后的值，保留两位小数</returns>
        private double CalculateProportionalValue(double value, double minValue, double maxValue)
        {
            // 确保输入值在最小值和最大值之间
            if (value < minValue) value = minValue;
            if (value > maxValue) value = maxValue;

            // 计算中间值
            double midValue = (minValue + maxValue) / 2.0;

            // 计算输入值与中间值的距离
            double distanceFromMid = Math.Abs(value - midValue);

            // 计算最大距离（即从中间到最小值或最大值的距离）
            double maxDistance = (maxValue - minValue) / 2.0;

            // 计算比例值
            double proportionalValue = 1.0 - (distanceFromMid / maxDistance);

            // 限制比例值在0到1之间
            if (proportionalValue < 0.0) proportionalValue = 0.0;
            if (proportionalValue > 1.0) proportionalValue = 1.0;

            // 返回结果，保留两位小数
            return Math.Round(proportionalValue, 2);
        }
        public List<FindCoorData> Yolo11Inference(Mat image, InferenceSession onnx_session, HWindow window, HObject rectangle, HTuple hv_FindParams, HTuple hv_HomMat2D, float conf_threshold, float nms_threshold, string[] class_names, int input_height, int input_width, int class_num, int box_num, out float ratio_height, out float ratio_width, int encoder, bool dispLocationRegion, bool displayCoor)
        {
            List<FindCoorData> datas = new List<FindCoorData>();
            if (hv_HomMat2D.Length < 1)
            {
                ratio_height = 0;
                ratio_width = 0;
                return null;
            }
            //DateTime dt1 = DateTime.Now;
            if (image == null)
            {
                ratio_height = 0;
                ratio_width = 0;
                return null;
            }

            HOperatorSet.SmallestRectangle1(rectangle, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2);
            int rec_row = Convert.ToInt32(row1.D);
            int rec_column = Convert.ToInt32(column1.D);
            int rec_width = Convert.ToInt32((column2 - column1).D);
            int rec_height = Convert.ToInt32((row2 - row1).D);
            Mat Resize_Img = new Mat();
            Mat Rect_Img = new Mat();
            Rect_Img = GlobalStaticData.displayConvert.MatReducdoman(image.Clone(), new Rect(rec_column, rec_row, rec_width, rec_height));

            int width = Rect_Img.Width;
            int height = Rect_Img.Height;

            if (height > input_height || width > input_width)
            {
                float scale = Math.Min((float)input_height / height, (float)input_width / width);
                OpenCvSharp.Size new_size = new OpenCvSharp.Size((int)(width * scale), (int)(height * scale));
                Cv2.Resize(Rect_Img, Resize_Img, new_size);
            }
            ratio_height = (float)height / Resize_Img.Rows;
            ratio_width = (float)width / Resize_Img.Cols;
            Mat input_img = new Mat();
            Cv2.CopyMakeBorder(Resize_Img, input_img, 0, input_height - Resize_Img.Rows, 0, input_width - Resize_Img.Cols, BorderTypes.Constant);

            #region 弃用0

            ////输入Tensor
            //  Tensor<float> input_tensor = new DenseTensor<float>(new[] { 1, 3, 640, 640 });


            //for (int y = 0; y < input_img.Height; y++)
            //{
            //    for (int x = 0; x < input_img.Width; x++)
            //    {
            //        input_tensor[0, 0, y, x] = input_img.At<Vec3b>(y, x)[0] / 255f;
            //        input_tensor[0, 1, y, x] = input_img.At<Vec3b>(y, x)[1] / 255f;
            //        input_tensor[0, 2, y, x] = input_img.At<Vec3b>(y, x)[2] / 255f;
            //    }
            //}
            #endregion

            #region 弃用1

            // 启用并行化（需引入System.Threading.Tasks）
            //Tensor<float> input_tensor = new DenseTensor<float>(new[] { 1, 3, input_img.Height, input_img.Width });
            //var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 2 };


            //Parallel.For(0, input_img.Height, y =>
            //{
            //    for (int x = 0; x < input_img.Width; x++)
            //    {
            //        var pixel = input_img.At<Vec3b>(y, x);
            //        input_tensor[0, 0, y, x] = pixel[0] / 255f;
            //        input_tensor[0, 1, y, x] = pixel[1] / 255f;
            //        input_tensor[0, 2, y, x] = pixel[2] / 255f;
            //    }
            //});
            #endregion


            Mat blob = CvDnn.BlobFromImage(input_img,        // 输入 Mat（已resize+padding）
                                            1.0 / 255.0,      // 归一化
                                            new OpenCvSharp.Size(input_width, input_height),  // 尺寸
                                            new Scalar(),     // 均值(0,0,0)
                                            swapRB: true,     // BGR→RGB
                                            crop: false       // 不裁剪
                                            );

            // blob shape = (1, 3, H, W)
            int totalBlob = (int)(blob.Total() * blob.ElemSize() / sizeof(float));
            float[] chwData = new float[totalBlob];

            // 直接拷贝 blob 到 float[]，不需要手写 HWC→CHW
            Marshal.Copy(blob.Data, chwData, 0, totalBlob);

            // 直接构造 ONNX Tensor
            var input_tensor = new DenseTensor<float>(chwData, new[] { 1, 3, input_height, input_width });




            List<NamedOnnxValue> input_container = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("images", input_tensor)
            };

            //推理


            var ort_outputs = onnx_session.Run(input_container).ToArray();
            //  DateTime dt2 = DateTime.Now;

            float[] data = Transpose(ort_outputs[0].AsTensor<float>().ToArray(), 4 + class_num, box_num);

            float[] confidenceInfo = new float[class_num];
            float[] rectData = new float[4];

            List<DetectionResult> detResults = new List<DetectionResult>();
            #region 原方法
            for (int i = 0; i < box_num; i++)
            {
                Array.Copy(data, i * (class_num + 4), rectData, 0, 4);
                Array.Copy(data, i * (class_num + 4) + 4, confidenceInfo, 0, class_num);

                float score = confidenceInfo.Max(); // 获取最大值

                int maxIndex = Array.IndexOf(confidenceInfo, score); // 获取最大值的位置

                int _centerX = (int)(rectData[0] * ratio_width);
                int _centerY = (int)(rectData[1] * ratio_height);
                int _width = (int)(rectData[2] * ratio_width);
                int _height = (int)(rectData[3] * ratio_height);

                detResults.Add(new DetectionResult(
                   maxIndex,
                   class_names[maxIndex],
                   new Rect(_centerX - _width / 2, _centerY - _height / 2, _width, _height),
                   score));
            }
            #endregion



            //NMS
            CvDnn.NMSBoxes(detResults.Select(x => x.Rect), detResults.Select(x => x.Confidence), conf_threshold, nms_threshold, out int[] indices);
            detResults = detResults.Where((x, index) => indices.Contains(index)).ToList();
            HTuple result = new HTuple();
            HOperatorSet.GenEmptyObj(out HObject resultCross);
            foreach (DetectionResult r in detResults)
            {

                double offsetRow = ApplyCompensation((r.Rect.Bottom + r.Rect.Top) / 2, 0, height, hv_FindParams[13]);
                double offsetCol = ApplyCompensation((r.Rect.Right + r.Rect.Left) / 2, 0, width, hv_FindParams[14]);
                HOperatorSet.GenCrossContourXld(out HObject cross, offsetRow, offsetCol, 80, 0);
                HOperatorSet.GenRectangle1(out HObject rectangle1, r.Rect.Top, r.Rect.Left, r.Rect.Bottom, r.Rect.Right);
                HOperatorSet.ConcatObj(resultCross, rectangle1, out resultCross);
                HOperatorSet.ConcatObj(resultCross, cross, out resultCross);
                double score = CalculateProportionalValue(offsetCol, 0, width);

                HOperatorSet.AffineTransPoint2d(hv_HomMat2D, offsetRow, offsetCol, out HTuple _WorldRow, out HTuple _WorldColumn);
                //Cv2.PutText(result_image, $"{r.Class}:{r.Confidence:P0}", new OpenCvSharp.Point(r.Rect.TopLeft.X, r.Rect.TopLeft.Y - 10), HersheyFonts.HersheySimplex, 1, Scalar.Red, 2);
                //Cv2.Rectangle(result_image, r.Rect, Scalar.Red, thickness: 2);
               //window.DispText("[" + (_WorldColumn + hv_FindParams[1]) + "," + (_WorldRow + hv_FindParams[0]) + "," + r.Confidence.ToString("P0") + "]", "green", r.Rect.TopLeft.Y - 10, r.Rect.TopLeft.X, 20, "mono", "true", "true");
               
                datas.Add(new FindCoorData()
                {
                    Attribute = 0,
                    Score = score,
                    WorldX = _WorldRow + hv_FindParams[0],
                    WorldY = _WorldColumn + hv_FindParams[1],
                    WorldYScurren = encoder - (_WorldColumn + hv_FindParams[1]),
                    encoding = encoder,
                    pixelRow = offsetRow,
                    pixelCol = offsetCol,
                    ImageHeight = height,
                    ImageWidtht = width,
                    IsUpDate = true

                });
            }
            if (datas.Count > 0)
            {
                window.DispObj(resultCross);
            }


            return datas;
        }

        public void Yolo11InferenceTo3D(Mat image, HObject rectangle, InferenceSession onnx_session, float conf_threshold, float nms_threshold, string[] class_names, int input_height, int input_width, int class_num, int box_num, out HTuple hv_RowsOut, out HTuple hv_ColumnsOut,out HTuple hv_OutWidth, out HTuple hv_OutHeight, out HTuple hv_OutScore)
        {

  
            hv_RowsOut = new HTuple();
            hv_ColumnsOut = new HTuple();
            hv_OutWidth=new HTuple();   
            hv_OutHeight=new HTuple();
            hv_OutScore = new HTuple();
            //DateTime dt1 = DateTime.Now;

            if (image == null)
            {
                
                return;
            }
            try
            {


                //获取rectangle的左上角和右下角坐标
                //HOperatorSet.SmallestRectangle1(rectangle, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2);
                //int rec_row = Convert.ToInt32(row1.D);
                //int rec_column = Convert.ToInt32(column1.D);
                //int rec_width = Convert.ToInt32((column2 - column1).D);
                //int rec_height = Convert.ToInt32((row2 - row1).D);
                //Mat Resize_Img = new Mat();
                ////裁减图像
                //Mat Rect_Img = GlobalStaticData.displayConvert.MatReducdoman(image.Clone(), new Rect(rec_column, rec_row, rec_width, rec_height));

                int orig_h = image.Rows;
                int orig_w = image.Cols;

                // === 等比例缩放 ===
                float scale = Math.Min((float)input_height / orig_h, (float)input_width / orig_w);
                int new_w = (int)(orig_w * scale);
                int new_h = (int)(orig_h * scale);
            
                Mat resized = new Mat();

                Cv2.Resize(image, resized, new OpenCvSharp.Size(new_w, new_h));

                // === 对称填充 ===
                int top = (input_height - new_h) / 2;
                int bottom = input_height - new_h - top;
                int left = (input_width - new_w) / 2;
                int right = input_width - new_w - left;

                Mat input_img = new Mat();
                Cv2.CopyMakeBorder(resized, input_img, top, bottom, left, right, BorderTypes.Constant, Scalar.Black);
                #region 弃用0
                // === 转 float32, RGB, 归一化 ===
                //Tensor<float> input_tensor = new DenseTensor<float>(new[] { 1, 3, 640, 640 });
                //unsafe
                //{
                //    for (int y = 0; y < input_img.Height; y++)
                //    {
                //        Vec3b* ptr = (Vec3b*)input_img.Ptr(y).ToPointer();
                //        for (int x = 0; x < input_img.Width; x++)
                //        {
                //            input_tensor[0, 0, y, x] = ptr[x][2] / 255f; // R
                //            input_tensor[0, 1, y, x] = ptr[x][1] / 255f; // G
                //            input_tensor[0, 2, y, x] = ptr[x][0] / 255f; // B
                //        }
                //    }
                //}
                #endregion
                #region 弃用1
                //// 转 float 并归一化
                //Mat inputFloat = new Mat();
                //input_img.ConvertTo(inputFloat, MatType.CV_32FC3, 1.0 / 255.0);
                //Cv2.CvtColor(inputFloat, inputFloat, ColorConversionCodes.BGR2RGB);

                //int height = input_img.Rows;
                //int width = input_img.Cols;
                //int channels = 3;
                //int total = height * width * channels;

                //// 拷贝所有像素到 float[]
                //float[] inputData = new float[total];
                //Marshal.Copy(inputFloat.Data, inputData, 0, total);

                //// OpenCV 默认顺序是 HWC，我们要转为 CHW
                //float[] chwData = new float[total];
                //int hw = height * width;

                //for (int y = 0; y < height; y++)
                //{
                //    for (int x = 0; x < width; x++)
                //    {
                //        int hwIndex = y * width + x;
                //        chwData[0 * hw + hwIndex] = inputData[hwIndex * 3 + 0]; // R
                //        chwData[1 * hw + hwIndex] = inputData[hwIndex * 3 + 1]; // G
                //        chwData[2 * hw + hwIndex] = inputData[hwIndex * 3 + 2]; // B
                //    }
                //}

                //// 直接构造 Tensor
                //var input_tensor = new DenseTensor<float>(chwData, new[] { 1, 3, height, width });
                #endregion
                #region 方法3
                Mat blob = CvDnn.BlobFromImage(input_img,        // 输入 Mat（已resize+padding）
                                                1.0 / 255.0,      // 归一化
                                                new OpenCvSharp.Size(input_width, input_height),  // 尺寸
                                                new Scalar(),     // 均值(0,0,0)
                                                swapRB: true,     // BGR→RGB
                                                crop: false       // 不裁剪
                                                );

                // blob shape = (1, 3, H, W)
                int totalBlob = (int)(blob.Total() * blob.ElemSize() / sizeof(float));
                float[] chwData = new float[totalBlob];

                // 直接拷贝 blob 到 float[]，不需要手写 HWC→CHW
                Marshal.Copy(blob.Data, chwData, 0, totalBlob);

                // 直接构造 ONNX Tensor
                var input_tensor = new DenseTensor<float>(chwData, new[] { 1, 3, input_height, input_width });
                #endregion
                List<NamedOnnxValue> input_container = new List<NamedOnnxValue>
                {
                     NamedOnnxValue.CreateFromTensor("images", input_tensor)
                 };

                var ort_outputs = onnx_session.Run(input_container).ToArray();
         

                float[] data = Transpose(ort_outputs[0].AsTensor<float>().ToArray(), 4 + class_num, box_num);

                float[] confidenceInfo = new float[class_num];
                float[] rectData = new float[4];

                List<DetectionResult> detResults = new List<DetectionResult>();
                #region 原方法
                for (int i = 0; i < box_num; i++)
                {
                    Array.Copy(data, i * (class_num + 4), rectData, 0, 4);
                    Array.Copy(data, i * (class_num + 4) + 4, confidenceInfo, 0, class_num);

                    float score = confidenceInfo.Max(); // 获取最大值

                    int maxIndex = Array.IndexOf(confidenceInfo, score); // 获取最大值的位置

                    int _centerX = (int)((rectData[0] - left) / scale);
                    int _centerY = (int)((rectData[1] - top) / scale);
                    int _width = (int)(rectData[2] / scale);
                    int _height = (int)(rectData[3] / scale);

                    detResults.Add(new DetectionResult(
                       maxIndex,
                       class_names[maxIndex],
                       new Rect(_centerX - _width / 2, _centerY - _height / 2, _width, _height),
                       score));
                }
                #endregion
                //NMS
                CvDnn.NMSBoxes(detResults.Select(x => x.Rect), detResults.Select(x => x.Confidence), conf_threshold, nms_threshold, out int[] indices);
                detResults = detResults.Where((x, index) => indices.Contains(index)).ToList();
                HTuple result = new HTuple();
                HOperatorSet.GenEmptyObj(out HObject resultCross);
                foreach (DetectionResult r in detResults)
                {
                    hv_RowsOut.Append((r.Rect.Bottom + r.Rect.Top) / 2);
                    hv_ColumnsOut.Append((r.Rect.Right + r.Rect.Left) / 2);
                    hv_OutWidth.Append(r.Rect.Width);
                    hv_OutHeight.Append(r.Rect.Height);
                    hv_OutScore.Append(r.Confidence);
                }
                return;
            }
            catch (Exception)
            {


            }
        }
    }


    public class DetectionResult
    {
        public DetectionResult(int ClassId, string Class, Rect Rect, float Confidence)
        {
            this.ClassId = ClassId;
            this.Confidence = Confidence;
            this.Rect = Rect;
            this.Class = Class;
        }

        public string Class { get; set; }

        public int ClassId { get; set; }

        public float Confidence { get; set; }

        public Rect Rect { get; set; }

    }
}
