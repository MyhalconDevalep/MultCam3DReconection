using HalconDotNet;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.ToolClass;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
//using static System.Net.Mime.MediaTypeNames;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class CalibrationAcq1 : UIPage
    {
        private Barrier _barrier = null;
        //private DrawRectangle1 _dispRectangle1 = null;
        //private DrawRectangle1 _dispRectangle2 = null;
        //private DrawRectangle1 _dispRectangle3 = null;

        public CalibrationAcq1()
        {
            InitializeComponent();
            //_dispRectangle1 = new DrawRectangle1(hWindowControl1.HalconWindow);
            //_dispRectangle2 = new DrawRectangle1(hWindowControl2.HalconWindow);
            //_dispRectangle3 = new DrawRectangle1(hWindowControl3.HalconWindow);
            _barrier = new Barrier(3);
            SaveImages.Enabled = false;
            btn_SaveContinuousStart.Enabled = false;
            SaveImageIndex.DataBindings.Add("Value", GlobalStaticData.UpdataBingdingData, "CalibrationIndex1",true,DataSourceUpdateMode.OnPropertyChanged);
        }

        private void Cam1AcqEnabel_ValueChanged(object sender, bool value)
        {
            if (!_allowChange)
                return;

            ApplyCamState(value);


        }

      
        private readonly object _lockCam1=new object();
        private readonly object _lockCam2=new object();
        private readonly object _lockCam3=new object();
        void Camera1Display(HObject Himage)
        {
        
            lock (_lockCam1)
            {
                if (isCam1AcqSave)
                {
                    isCam1AcqSave = false;
                   WriteImgaesHelper.WriteCalibrationImages(Himage.Clone(),"1", "0", $"1_{GlobalStaticData.UpdataBingdingData.CalibrationIndex1}");
                    _barrier.SignalAndWait();
                }

                // hWindowControl1.HalconWindow.DispObj(Himage.Clone());
                //_dispRectangle1.Image = Himage.Clone();
                hWindowControl1.HobjectToHimage(Himage.Clone());
                this.BeginInvoke(new Action(() => {
                  
                    //_dispRectangle1.SetHalconScalingZoom();
                    //_dispRectangle1.DisplayImage();
                    SaveImages.Enabled = true;
                }));
            }
           
            Himage.Dispose();
        }
        void Camera2Display(HObject Himage)
        {
           
            lock (_lockCam2)
            {
                if (isCam2AcqSave)
                {
                    isCam2AcqSave = false;
                    WriteImgaesHelper.WriteCalibrationImages(Himage.Clone(),"1","1", $"2_{GlobalStaticData.UpdataBingdingData.CalibrationIndex1}");
                    _barrier.SignalAndWait();
                }
                hWindowControl2.HobjectToHimage( Himage.Clone());
                //_dispRectangle2.Image = Himage.Clone();
                this.BeginInvoke(new Action(() => {
                    
                    //_dispRectangle2.SetHalconScalingZoom();
                    //_dispRectangle2.DisplayImage();
                   
                }));
                // hWindowControl2.HalconWindow.DispObj(Himage.Clone());
            }
           
            Himage.Dispose();
        }
        void Camera3Display(HObject Himage)
        {
            lock(_lockCam3)
            {
                if (isCam3AcqSave)
                {
                    isCam3AcqSave = false;
                    WriteImgaesHelper.WriteCalibrationImages(Himage.Clone(),"1", "2", $"3_{GlobalStaticData.UpdataBingdingData.CalibrationIndex1}");
                    _barrier.SignalAndWait();
                    GlobalStaticData.UpdataBingdingData.CalibrationIndex1++;
                }
                // hWindowControl3.HalconWindow.DispObj(Himage.Clone());
                hWindowControl3.HobjectToHimage(Himage.Clone());
                //_dispRectangle3.Image = Himage.Clone();
                this.BeginInvoke(new Action(() => {
                   
                    //_dispRectangle3.SetHalconScalingZoom();
                    //_dispRectangle3.DisplayImage();

                }));
            }
            
            Himage.Dispose();
        }
        bool isCam1AcqSave=false;
        bool isCam2AcqSave=false;
        bool isCam3AcqSave=false;
        private void SaveImages_Click(object sender, EventArgs e)
        {
           
            SaveImages.Enabled = false;
            isCam1AcqSave =true;
            isCam2AcqSave=true;
            isCam3AcqSave=true; 
            UIMessageTip.ShowOk("保存信号发送成功");
           
        }

        private void SaveImageIndex_ValueChanged(object sender, int value)
        {
            GlobalStaticData.UpdataBingdingData.CalibrationIndex1=value;
        }

        bool _IsContinuousAcqSaveEnable = false;
        private void CamContinuousEnable_ValueChanged(object sender, bool value)
        {
            if (!_allowChange2)
                return;
     
            ApplyCamSoftState(value);

         
        }

      
      

        private void Camera1ContinuousDisplay(HObject Himage)
        {
            lock (_lockCam1)
            {
              
                if(_IsContinuousAcqSaveEnable)
                {
                   WriteImgaesHelper.WriteAllImages(Himage.Clone(), "Cam0", "1");
                }
                hWindowControl1.HobjectToHimage(Himage.Clone());
                //_dispRectangle1.Image=Himage.Clone();
                //_dispRectangle1.SetHalconScalingZoom();
                //_dispRectangle1.DisplayImage();


            }
        }

        private void Camera2ContinuousDisplay(HObject Himage)
        {
            lock (_lockCam2)
            {
              
                if (_IsContinuousAcqSaveEnable)
                {
                    WriteImgaesHelper.WriteAllImages(Himage.Clone(), "Cam1", "2");
                }
                hWindowControl2.HobjectToHimage(Himage.Clone()) ;
                //_dispRectangle2.Image= Himage.Clone();
                //_dispRectangle2.SetHalconScalingZoom();
                //_dispRectangle2.DisplayImage();

              
            }

            // Himage.Dispose();
        }

        private void Camera3ContinuousDisplay(HObject Himage)
        {
            lock (_lockCam3)
            {
        
                if (_IsContinuousAcqSaveEnable)
                {
                    WriteImgaesHelper.WriteAllImages(Himage.Clone(), "Cam2", "3");
                }
                hWindowControl3.HobjectToHimage(Himage.Clone() );
                //_dispRectangle3.Image = Himage.Clone();
                //_dispRectangle3.SetHalconScalingZoom();
                //_dispRectangle3.DisplayImage();

            }
        }


        // 用于控制线程的取消令牌
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning = false;
        private void btn_SaveContinuousStart_Click(object sender, EventArgs e)
        {
        
            if (!_isRunning)
            {
                // 启动线程
                _isRunning = true;
                btn_SaveContinuousStart.Text = "采集停止"; // 更新按钮文本
                btn_SaveContinuousStart.FillColor = Color.Red;
                btn_SaveContinuousStart.FillHoverColor = Color.LightPink;
                _cancellationTokenSource = new CancellationTokenSource();

                // 启动新线程（使用 Task.Run 避免阻塞 UI）
                Task.Run(() => ContinuousSave(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
            }
            else
            {
                // 停止线程
                _isRunning = false;
                btn_SaveContinuousStart.Text = "采集启动"; // 恢复按钮文本
                btn_SaveContinuousStart.FillColor = Color.FromArgb(80, 160, 255);
                btn_SaveContinuousStart.FillHoverColor = Color.FromArgb(115, 179, 255);
                _cancellationTokenSource?.Cancel(); // 发送取消信号
                _cancellationTokenSource?.Dispose(); // 释放资源
            }
        }


        // 循环运行的逻辑
        private void ContinuousSave(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    // 在这里执行你的循环逻辑（例如保存数据）
                    //  Console.WriteLine($"正在保存数据... {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                    GlobalStaticData.HIKCamera1.SoftwareTrigger();
                    GlobalStaticData.HIKCamera2.SoftwareTrigger();
                    GlobalStaticData.HIKCamera3.SoftwareTrigger();
                    // 模拟耗时操作（例如每1秒执行一次）
                    Thread.Sleep(500); // 可调整时间间隔
                }
            }
            catch (OperationCanceledException)
            {
                // 线程被正常取消，不做处理
               
              this.BeginInvoke(new Action(()=>  UIMessageTip.ShowWarning("线程已停止")));
            }
            catch (Exception ex)
            {
                // 其他异常处理
               this.BeginInvoke(new Action(()=> UIMessageTip.ShowError($"发生错误: {ex.Message}")));
            }
        }

        private void ContinuousAcq_Check_ValueChanged(object sender, bool value)
        {
            if(ContinuousAcq_Check.Active)
            {
                _IsContinuousAcqSaveEnable = true;
            }
            else
            {
                _IsContinuousAcqSaveEnable = false;
            }
        }

        private void ApplyCamState(bool enable)
        {
            if (enable)
            {
                GlobalStaticData.HIKCamera1?.TriggerMode(0);
                GlobalStaticData.HIKCamera2?.TriggerMode(0);
                GlobalStaticData.HIKCamera3?.TriggerMode(0);

                GlobalStaticData.HIKCamera1.eventRun += Camera1Display;
                GlobalStaticData.HIKCamera2.eventRun += Camera2Display;
                GlobalStaticData.HIKCamera3.eventRun += Camera3Display;
                this.Invoke(new Action(() => {
                    SaveImages.Enabled = true;
                CamContinuousEnable.Enabled = false;
                }));
            }
            else
            {
                GlobalStaticData.HIKCamera1.TriggerMode(1);
                GlobalStaticData.HIKCamera2.TriggerMode(1);
                GlobalStaticData.HIKCamera3.TriggerMode(1);

                GlobalStaticData.HIKCamera1.eventRun -= Camera1Display;
                GlobalStaticData.HIKCamera2.eventRun -= Camera2Display;
                GlobalStaticData.HIKCamera3.eventRun -= Camera3Display;

                GlobalStaticData.UpdataBingdingData.CalibrationIndex1 = 0;
                this.Invoke(new Action(() => {
                    SaveImages.Enabled = false;
                    CamContinuousEnable.Enabled = true;
                }));
              
            }
        }

        private void ApplyCamSoftState(bool enable)
        {
            if (enable)
            {
               

                    GlobalStaticData.HIKCamera1.TriggerCamera(7);
                    GlobalStaticData.HIKCamera2.TriggerCamera(7);
                    GlobalStaticData.HIKCamera3.TriggerCamera(7);
                    GlobalStaticData.HIKCamera1.eventRun += Camera1ContinuousDisplay;
                    GlobalStaticData.HIKCamera2.eventRun += Camera2ContinuousDisplay;
                    GlobalStaticData.HIKCamera3.eventRun += Camera3ContinuousDisplay;
                    btn_SaveContinuousStart.Enabled = true;
                    Cam1AcqEnabel.Enabled = false;

                
            }
            else
            {


                GlobalStaticData.HIKCamera1.TriggerCamera(0);
                GlobalStaticData.HIKCamera2.TriggerCamera(0);
                GlobalStaticData.HIKCamera3.TriggerCamera(0);
                GlobalStaticData.HIKCamera1.eventRun -= Camera1ContinuousDisplay;
                GlobalStaticData.HIKCamera2.eventRun -= Camera2ContinuousDisplay;
                GlobalStaticData.HIKCamera3.eventRun -= Camera3ContinuousDisplay;
                btn_SaveContinuousStart.Enabled = false;
                Cam1AcqEnabel.Enabled = true;
            }
        }

        private bool _allowChange = false;
        private bool _allowUpdatefirst = true;
        private void Cam1AcqEnabel_Click(object sender, EventArgs e)
        {

            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 1)
            {
                _allowChange = false;

                // 回滚状态
                Cam1AcqEnabel.Active = !Cam1AcqEnabel.Active;
                MessageBox.Show("权限不足，无法操作该功能");
                return;
            }
        
                _allowChange = true;
                // ⭐ 第一次就是真实状态 ds
                if (_allowUpdatefirst)
                {
                    bool state = Cam1AcqEnabel.Active;
                    ApplyCamState(state);
                    _allowUpdatefirst = false;
                }
         

        }

        private bool _allowChange2 = false;
        private bool _allowUpdatefirst2 = true;
        private void CamContinuousEnable_Click(object sender, EventArgs e)
        {
            if (GlobalStaticData.UpdataBingdingDisplayMsgq.UserPower < 1)
            {
                // 阻止状态变化
                _allowChange2 = false;

                // 强制恢复原状态（关键）
                CamContinuousEnable.Active = !CamContinuousEnable.Active;

                MessageBox.Show("权限不足，无法操作该功能");
                return;
            }
          
                _allowChange2 = true;
            if (_allowUpdatefirst2)
            {
                bool state = CamContinuousEnable.Active;
                ApplyCamSoftState(state);
                _allowUpdatefirst2 = false;
            }
          
        }
    }
}
