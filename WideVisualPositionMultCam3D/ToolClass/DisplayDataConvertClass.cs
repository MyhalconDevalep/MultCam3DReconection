using HalconDotNet;
using OpenCvSharp;
using Sunny.UI.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace WideVisualPositionMultCam3D.ToolClass
{
    public class DisplayDataConvertClass
    {
        public void SetHalconScalingZoom(HObject image, HWindow window)
        {
            HTuple dispWidth = 0;
            HTuple dispHeight = 0;
            HTuple offset = 0;
            HTuple colum = 0;
            HTuple Row = 0;
            if (image == null)
                return;
            
            HOperatorSet.GetImageSize(image, out HTuple width, out HTuple height);
            int windowWidth = 0;
            int windowHeight = 0;
            window.GetWindowExtents(out int rowWindow, out int colWindow, out windowWidth, out windowHeight);
            HTuple picWHRatio = 1.0 * width / height;
            HTuple winWHRatio = 1.0 * windowWidth / windowHeight;
            // Halcon 是 WPF 控件对象
            if (width > windowWidth || height > windowHeight)
            {
                if (picWHRatio >= winWHRatio)
                {
                    dispWidth = width;
                    dispHeight = width / winWHRatio;
                    offset = (dispHeight - height) / 2;
                    colum = 0;
                    window.SetPart(-offset, colum, dispHeight - offset, dispWidth);
                }
                else
                {
                    dispWidth = height * winWHRatio;
                    dispHeight = height;
                    offset = (dispWidth - width) / 2;
                    Row = 0;
                    window.SetPart(Row, -offset, dispHeight, dispWidth - offset);
                }
            }
            else
            {
                if (picWHRatio >= winWHRatio)
                {
                    dispWidth = width;
                    dispHeight = width / winWHRatio;
                    offset = (dispHeight - height) / 2;
                    colum = 0;
                    window.SetPart(-offset, colum, dispHeight - offset, width);
                }
                else
                {
                    dispWidth = height * winWHRatio;
                    dispHeight = height;
                    offset = (dispWidth - width) / 2;
                    Row = 0;
                    window.SetPart(Row, -offset, dispHeight, dispWidth - offset);

                }
            }

        }

        /// <summary>
        /// HObject转bitmap
        /// </summary>
        /// <param name="hObject"></param>
        /// <param name="bitmap"></param>
        public void HObjectToBitmap(HObject hObject, out Bitmap bitmap)
        {
            try
            {
                HTuple hRed, hGreen, hBlue, type, width, height;
                HOperatorSet.GetImagePointer3(hObject, out hRed, out hGreen, out hBlue, out type, out width, out height);

                bitmap = new Bitmap(width.I, height.I, PixelFormat.Format24bppRgb);
                Rectangle rect = new Rectangle(0, 0, width.I, height.I);
                BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                unsafe
                {
                    byte* bPtr = (byte*)bitmapData.Scan0;
                    byte* rPtr = (byte*)hRed.L;
                    byte* gPtr = (byte*)hGreen.L;
                    byte* bPtrSrc = (byte*)hBlue.L;

                    for (int i = 0; i < width.I * height.I; i++)
                    {
                        bPtr[i * 3] = bPtrSrc[i];
                        bPtr[i * 3 + 1] = gPtr[i];
                        bPtr[i * 3 + 2] = rPtr[i];
                    }
                }

                bitmap.UnlockBits(bitmapData);
            }
            catch
            {
                bitmap = null;
            }
        }


        public HObject MatToHObject(Mat mat)
        {
            HObject hImage = new HObject();
            try
            {
                if (mat.Type() == MatType.CV_8UC1)
                {
                    // 单通道处理
                    HOperatorSet.GenImage1(out hImage, "byte", mat.Cols, mat.Rows, mat.Data);
                }
                else if (mat.Type() == MatType.CV_8UC3)
                {
                    // 三通道处理
                    Mat[] channels = mat.Split();
                    byte[] rData = new byte[mat.Total()];
                    byte[] gData = new byte[mat.Total()];
                    byte[] bData = new byte[mat.Total()];
                    Marshal.Copy(channels[2].Data, rData, 0, rData.Length); // R
                    Marshal.Copy(channels[1].Data, gData, 0, gData.Length); // G
                    Marshal.Copy(channels[0].Data, bData, 0, bData.Length); // B
                    HOperatorSet.GenImage3(out hImage, "byte", mat.Cols, mat.Rows, new HTuple(rData), new HTuple(gData), new HTuple(bData));
                }
                else
                {
                    throw new NotSupportedException("仅支持CV_8UC1或CV_8UC3格式");
                }
                return hImage;
            }
            finally
            {
                // 释放资源
                mat?.Dispose();
            }
        }

        public Mat HObjectToMat(HObject hObject)
        {
            HTuple pointer, type, width, height;
            HOperatorSet.GetImagePointer1(hObject, out pointer, out type, out width, out height);

            Mat mat = new Mat(height.I, width.I, MatType.CV_8UC1);
            unsafe
            {
                byte* srcPtr = (byte*)pointer.IP;
                byte* dstPtr = (byte*)mat.Data;
                int totalPixels = width.I * height.I;
                Buffer.MemoryCopy(srcPtr, dstPtr, totalPixels, totalPixels);
            }
            return mat;
        }

        public Mat HObjectToMatC(HObject hObject)
        {
            HTuple pointerR, pointerG, pointerB, type, width, height;

            // 获取图像通道信息
            HTuple channels;
            HOperatorSet.CountChannels(hObject, out channels);

            if (channels.I == 3)
            {
                // 3通道彩色图像
                HOperatorSet.GetImagePointer3(hObject, out pointerR, out pointerG, out pointerB,
                                             out type, out width, out height);

                Mat mat = new Mat(height.I, width.I, MatType.CV_8UC3);
                unsafe
                {
                    byte* srcPtrR = (byte*)pointerR.IP;
                    byte* srcPtrG = (byte*)pointerG.IP;
                    byte* srcPtrB = (byte*)pointerB.IP;
                    byte* dstPtr = (byte*)mat.Data;

                    int totalPixels = width.I * height.I;

                    // 将Halcon的RGB分别存储转换为OpenCV的BGR交错存储
                    for (int i = 0; i < totalPixels; i++)
                    {
                        dstPtr[i * 3 + 2] = srcPtrR[i]; // R -> OpenCV的BGR中的R
                        dstPtr[i * 3 + 1] = srcPtrG[i]; // G -> OpenCV的BGR中的G
                        dstPtr[i * 3 + 0] = srcPtrB[i]; // B -> OpenCV的BGR中的B
                    }
                }
                return mat;
            }
            else if (channels.I == 1)
            {
                // 单通道图像（保持原有逻辑）
                HOperatorSet.GetImagePointer1(hObject, out pointerR, out type, out width, out height);

                Mat mat = new Mat(height.I, width.I, MatType.CV_8UC1);
                unsafe
                {
                    byte* srcPtr = (byte*)pointerR.IP;
                    byte* dstPtr = (byte*)mat.Data;
                    int totalPixels = width.I * height.I;
                    Buffer.MemoryCopy(srcPtr, dstPtr, totalPixels, totalPixels);
                }
                return mat;
            }
            else
            {
                throw new NotSupportedException($"不支持的通道数: {channels.I}");
            }
        }
        public Mat HObjectToMatColor(HObject hObject)
        {
            HTuple ptrR, ptrG, ptrB, type, width, height;
            HOperatorSet.GetImagePointer3(hObject, out ptrR, out ptrG, out ptrB, out type, out width, out height);

            Mat mat = new Mat(height.I, width.I, MatType.CV_8UC3);
            unsafe
            {
                byte* srcR = (byte*)ptrR.IP;
                byte* srcG = (byte*)ptrG.IP;
                byte* srcB = (byte*)ptrB.IP;
                byte* dstPtr = (byte*)mat.Data;

                for (int i = 0; i < height.I * width.I; i++)
                {
                    dstPtr[3 * i + 2] = srcR[i]; // R通道
                    dstPtr[3 * i + 1] = srcG[i]; // G通道
                    dstPtr[3 * i] = srcB[i];     // B通道
                }
            }
            return mat;
        }

        public Mat HObjectToMatAuto(HObject hObject)
        {
            HTuple channels;
            HOperatorSet.CountChannels(hObject, out channels);

            if (channels.I == 1)
                return HObjectToMat(hObject);
            else if (channels.I == 3)
                return HObjectToMatColor(hObject);
            else
                throw new NotSupportedException("仅支持单通道或三通道图像");
        }



        public Mat MatReducdoman(Mat src, Rect rect)
        {
            try
            {
                Mat mask = new Mat(src.Size(), MatType.CV_8UC1, Scalar.Black);
                mask[rect].SetTo(Scalar.White);
                //// 应用掩码
                Mat img2 = new Mat();
                src.CopyTo(img2, mask);
                return img2;
            }
            catch (Exception)
            {
                return null;
            }
            // Rect rect = new Rect(100, 100, 200, 150);

        }
    }
}
