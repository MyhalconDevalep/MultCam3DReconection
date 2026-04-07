using HalconDotNet;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static MvCamCtrl.NET.MyCamera;



namespace WideVisualPositionMultCam3D.ToolClass
{
    public class HIKCameraSDK
    {

        //定义一个委托，可以选择将委托类型看做只定义了一个方法的接口，将委托的实例看做实现了那个接口的一个对象
        public delegate void delegateRun(HObject Himage);//若有参数，执行方法要有相同签名
        //声明一个事件，看做特殊的委托类型变量
        public event delegateRun eventRun;
        public CancellationToken _token=new CancellationToken();

        MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList;
        public MyCamera m_pMyCamera;
        public bool _isThreadLoopflag=false;

        uint g_nPayloadSize = 0;
        public uint OffsetY = 0;
        public uint OffsetX = 0;
        public bool Camera_ReverseY=false;
        public bool Camera_ReverseX= false;

        public int Running = 0;
        Mutex m_mutex = new Mutex();
       // List<Bitmap> camlist = new List<Bitmap>();
        /// <summary>
        /// 初始化相机
        /// </summary>
        /// <param name="camName">序列号</param>
        /// <param name="SetX">OffsetX</param>
        /// <param name="SetY">OffsetY</param>
        public HIKCameraSDK(string camName, uint SetX, uint SetY)   //初始化事件
        {
            eventRun += new delegateRun(KMcameraSDK_eventRun);
            OffsetX = SetX;
            OffsetY = SetY;
            m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();      //查找到的设备列表    
            m_pMyCamera = new MyCamera();
            MyCamera.MV_CC_DEVICE_INFO device;
            DeviceListAcq(camName, out device);   //获取到指定名称的相机
            int nRet = -1;
            nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {

                return;
            }
            openCamera(ref device);
            
            SetCameraIntvalue("GevHeartbeatTimeout", 3000);

            //      SetCamerafloat("Gain", 10);
            SetCameraIntvalue("GevSCPD", 20000);
            SetCamerafloat("AcquisitionFrameRate", 50);
            // ch:开启抓图 | en:start grab
            nRet = m_pMyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("开始采集失败");
                return;
            }
            _isThreadLoopflag = true;
              Task.Factory.StartNew(() => this.ReceiveImageWorkThread(),TaskCreationOptions.LongRunning);
        }

        public HIKCameraSDK(string camName, uint SetX, uint SetY,bool Camera_ReverseX,bool Camera_RevveseY)   //初始化事件
        {
            eventRun += new delegateRun(KMcameraSDK_eventRun);
            OffsetX = SetX;
            OffsetY = SetY;
            m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();      //查找到的设备列表    
            m_pMyCamera = new MyCamera();
            MyCamera.MV_CC_DEVICE_INFO device;
            DeviceListAcq(camName, out device);   //获取到指定名称的相机
            int nRet = -1;
            nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {

                return;
            }
            openCamera(ref device);

            SetCameraIntvalue("GevHeartbeatTimeout", 3000);
            if (Camera_ReverseX)//设置图像是否翻转
            {
                SetCameraBoolValue("ReverseX", true);
            }
            else
            {
                SetCameraBoolValue("ReverseX", false);
            }
            if(Camera_RevveseY)
            {
                SetCameraBoolValue("ReverseY", true);
            }
            else
            {
                SetCameraBoolValue("ReverseY", false);
            }

            //      SetCamerafloat("Gain", 10);
            SetCameraIntvalue("GevSCPD", 20000);
            SetCamerafloat("AcquisitionFrameRate", 50);
            // ch:开启抓图 | en:start grab
            nRet = m_pMyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("开始采集失败");
                return;
            }
              Task.Factory.StartNew(() => this.ReceiveImageWorkThread(),TaskCreationOptions.LongRunning);
        }

        void KMcameraSDK_eventRun(HObject Himage)
        {

        }

        public void closeCamera() //关闭相机
        {
            _isThreadLoopflag = false;
            // ch:停止抓图 || en:Stop grab image
            m_pMyCamera.MV_CC_StopGrabbing_NET();
            // ch:关闭设备 || en: Close device
            m_pMyCamera.MV_CC_CloseDevice_NET();
         
        }

        public void StopGrabbing()//停止采集
        {
            int nRet = -1;
            // ch:停止抓图 || en:Stop grab image
            nRet = m_pMyCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show("Stop Grabbing Fail");
            }

        }

        public void StartGrabbing()//开始采集
        {
            int nRet;

            // ch:开启抓图 | en:start grab
            nRet = m_pMyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Start Grabbing Fail");

                return;
            }



        }
        public void TriggerMode(uint Md)         //触发模式
        {
            int nRet = m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", Md);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show("Set TriggerMode Fail");
                return;
            }
        }

        public void SetExposureTime(float time)
        {
            int nRet = MyCamera.MV_OK;
            nRet = m_pMyCamera.MV_CC_SetFloatValue_NET("ExposureTime", time);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show("Set ExposureTime Fail");
                return;
            }
        }
        public void SetGain(float gain)
        {
            int nRet = MyCamera.MV_OK;
            nRet = m_pMyCamera.MV_CC_SetFloatValue_NET("Gain", gain);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show("Set Gain Fail");
                return;
            }
        }

        public void TriggerCamera(uint Select)    //触发选择
        {
            int nRet = MyCamera.MV_OK;
            m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", Select);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show("Set TriggerMode Fail");
                return;
            }

            // ch: 触发源选择:0 - Line0 || en :TriggerMode select;
            //           1 - Line1;
            //           2 - Line2;
            //           3 - Line3;
            //           4 - Counter;
            //           7 - Software;
        }

        public void SoftwareTrigger()//软触发一次
        {
            int nRet;

            // ch: 触发命令 || en: Trigger command
            nRet = m_pMyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
            if (MyCamera.MV_OK != nRet)
            {
                Running = 100;
                //  MessageBox.Show("Trigger Fail");
            }
        }

        public float GetCamerafloat(string CameraParam)   //获取float参数
        {
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_pMyCamera.MV_CC_GetFloatValue_NET(CameraParam, ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                return stParam.fCurValue;
            }
            else
            {
                return 1000;
            }
        }
        public string GetCamerastring(string CameraParam)   //获取String参数
        {
            MyCamera.MVCC_STRINGVALUE stParam = new MyCamera.MVCC_STRINGVALUE();
            int nRet = m_pMyCamera.MV_CC_GetStringValue_NET(CameraParam, ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                return stParam.chCurValue;
            }
            else
            {
                return null;
            }
        }
        public uint GetCameraENUMVALUE(string CameraParam)//获取相机参数的枚举
        {
            int nRet;
            MyCamera.MVCC_ENUMVALUE stSelValue = new MyCamera.MVCC_ENUMVALUE();
            nRet = m_pMyCamera.MV_CC_GetEnumValue_NET("CameraParam", ref stSelValue);
            if (MyCamera.MV_OK != nRet)
            {

                return 100;
            }
            return stSelValue.nCurValue;
        }
        public uint GetCameraIntvalue(string CameraParam) //获取相机参数的INt
        {
            int nRet;
            MyCamera.MVCC_INTVALUE stSelValue = new MyCamera.MVCC_INTVALUE();
            nRet = m_pMyCamera.MV_CC_GetIntValue_NET(CameraParam, ref stSelValue);
            if (MyCamera.MV_OK != nRet)
            {

                return stSelValue.nCurValue;
            }
            return 122;
        }
        public bool SetCamerafloat(string CameraParam, float Value)   //设置float参数
        {


            int nRet = m_pMyCamera.MV_CC_SetFloatValue_NET(CameraParam, Value);
            if (MyCamera.MV_OK != nRet)
            {
                //  MessageBox.Show("设置参数失败");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool SetCameraString(string CameraParam, string Value)   //设置String参数
        {

            int nRet = m_pMyCamera.MV_CC_SetStringValue_NET(CameraParam, Value);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("设置参数失败");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool SetCameraENUMVALUE(string CameraParam, uint Value)   //设置枚举参数
        {

            int nRet = m_pMyCamera.MV_CC_SetEnumValue_NET(CameraParam, Value);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("设置参数失败");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool SetCameraIntvalue(string CameraParam, uint Value)   //设置int参数
        {

            int nRet = m_pMyCamera.MV_CC_SetIntValue_NET(CameraParam, Value);
            if (MyCamera.MV_OK != nRet)
            {
                Running = 100;
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SetCameraBoolValue(string CameraParam, bool Value)
        {
            int nRet = m_pMyCamera.MV_CC_SetBoolValue_NET(CameraParam, Value);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("设置BOOL参数失败");
                return false;
            }
            else
            {
                return true;
            }
         

        }


        public bool GetCameraTemperatureValue(string CameraParam,out MyCamera.MVCC_FLOATVALUE value)
        {
            value=new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_pMyCamera.MV_CC_GetFloatValue_NET(CameraParam,ref value);

     
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("获取参数失败");
                return false;
            }
            else
            {
                return true;
            }

        }



        private void openCamera(ref MyCamera.MV_CC_DEVICE_INFO device)
        {
            int nRet = -1;
            nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }

            // ch:打开设备 | en:Open device
            nRet = m_pMyCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Open Device Fail");
                return;
            }

            // ch:获取包大小 || en: Get Payload Size
            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            nRet = m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Get PayloadSize Fail");
                return;
            }
            g_nPayloadSize = stParam.nCurValue;

            // ch:获取高 || en: Get Height
            nRet = m_pMyCamera.MV_CC_GetIntValue_NET("Height", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Get Height Fail");
                return;
            }
            uint nHeight = stParam.nCurValue;

            // ch:获取宽 || en: Get Width
            nRet = m_pMyCamera.MV_CC_GetIntValue_NET("Width", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Get Width Fail");
                return;
            }


            // ch:设置触发模式为off || en:set trigger mode as off
            m_pMyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);//2为连续采集模式
            m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);//1为开启触发模式
            m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 0);//7为软触发，0为LINE0
           // TriggerCamera(7);
            SetCameraENUMVALUE("LineSelector", 2);
            m_pMyCamera.MV_CC_SetBoolValue_NET("LineInverter", false);
        }


        private void DeviceListAcq(string cName, out MyCamera.MV_CC_DEVICE_INFO mdevice)
        {
            int nRet;
            // ch:创建设备列表 || en: Create device list
            System.GC.Collect();
            mdevice = new MyCamera.MV_CC_DEVICE_INFO();
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Enum Devices Fail");

                return;
            }

            // ch:在窗体列表中显示设备名 || Display the device'name on window's list
            for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));

                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                //if (gigeInfo.chUserDefinedName==cName)
                if (gigeInfo.chSerialNumber == cName)
                {
                    mdevice = device;
                    break;
                }

            }

        }

        private Boolean IsColorData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YCBCR411_8_CBYYCRYY:
                    return true;

                default:
                    return false;
            }
        }

        /************************************************************************
*  @fn     ConvertBayer8ToHalcon()
*  @brief  Bayer8转换为Halcon格式数据
*  @param  Hobj                   [OUT]          转换后的输出Hobject数据
*  @param  nHeight                [IN]           图像高度
*  @param  nWidth                 [IN]           图像宽度
*  @param  nPixelType             [IN]           源数据格式
*  @param  pData                  [IN]           源数据
*  @return 成功，返回STATUS_OK；错误，返回STATUS_ERROR 
************************************************************************/
      private int ConvertBayer8ToHalcon(ref HObject Hobj, int nHeight, int nWidth, MvGvspPixelType nPixelType,IntPtr pData)
        {
            if (null == Hobj || null == pData)
            {
                return -1;
            }

          HOperatorSet.GenImage1(out Hobj, "byte", nWidth, nHeight, pData);

            if (nPixelType ==MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8)
            {
               HOperatorSet.CfaToRgb( Hobj,out Hobj, "bayer_gr", "bilinear");
            }
            else if (nPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8)
            {
               HOperatorSet.CfaToRgb(Hobj,out Hobj, "bayer_rg", "bilinear");
            }
            else if (nPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8)
            {
               HOperatorSet.CfaToRgb(Hobj,out Hobj, "bayer_gb", "bilinear");
            }
            else if (nPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8)
            {
               HOperatorSet.CfaToRgb(Hobj,out Hobj, "bayer_bg", "bilinear");
            }

            return MV_OK;
        }



        private object lockObject = new object();
        void ReceiveImageWorkThread()
        {
            HObject Gmap=null;
            int nRet = MyCamera.MV_OK;
            MyCamera device = m_pMyCamera;
            HOperatorSet.GenEmptyObj(out Gmap);
            MyCamera.MV_FRAME_OUT FrameInfoOut=new MyCamera.MV_FRAME_OUT();

          
           
            while (_isThreadLoopflag)
            {

                nRet = device.MV_CC_GetImageBuffer_NET(ref FrameInfoOut, 1000);
                // ch:获取一帧图像 | en:Get one image
                if (MyCamera.MV_OK == nRet)
                {
                    // Gmap.Dispose();
                    lock (lockObject)
                    {
                        if (Gmap != null)
                        {
                            Gmap.Dispose();
                        }
                        bool Res= IsColorData(FrameInfoOut.stFrameInfo.enPixelType);
                        // Bitmap bmp = new Bitmap(FrameInfo.nWidth, FrameInfo.nHeight, FrameInfo.nWidth*3, PixelFormat.Format24bppRgb, pBufForDriver);
                        if(Res)
                        {

                            nRet = ConvertBayer8ToHalcon(ref Gmap, FrameInfoOut.stFrameInfo.nHeight, FrameInfoOut.stFrameInfo.nWidth, FrameInfoOut.stFrameInfo.enPixelType, FrameInfoOut.pBufAddr);
                            if (nRet != 0)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            HOperatorSet.GenImage1(out Gmap, "byte", FrameInfoOut.stFrameInfo.nWidth, FrameInfoOut.stFrameInfo.nHeight, FrameInfoOut.pBufAddr);
                        }
                     
                           
                        eventRun(Gmap);
                        device.MV_CC_FreeImageBuffer_NET(ref FrameInfoOut);
                        //Marshal.FreeHGlobal(pBufForDriver);

                    }
                }
                else
                {
                 
                    Thread.Sleep(5);
                    continue;
                }
            }
        }
    }    //使用线程委托传递图片
}
