using HalconDotNet;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WideVisualPositionMultCam3D.ToolClass;

namespace WideVisualPositionMultCam3D.Page
{
    public partial class CalibrationAcq6 : UIPage
    {
        private Barrier _barrier = null;
        //private DrawRectangle1 _dispRectangle1 = null;
        //private DrawRectangle1 _dispRectangle2 = null;
        //private DrawRectangle1 _dispRectangle3 = null;

        public CalibrationAcq6()
        {
            InitializeComponent();
            //_dispRectangle1 = new DrawRectangle1(hWindowControl1.HalconWindow);
            //_dispRectangle2 = new DrawRectangle1(hWindowControl2.HalconWindow);
            //_dispRectangle3 = new DrawRectangle1(hWindowControl3.HalconWindow);
            _barrier = new Barrier(3);
            SaveImages.Enabled = false;
            btn_SaveContinuousStart.Enabled = false;
            Disposed += (s, e) => CalibrationAcqUiStateHelper.StopContinuousCapture(ref _isRunning, ref _cancellationTokenSource, btn_SaveContinuousStart);
            SaveImageIndex.DataBindings.Add("Value", GlobalStaticData.UpdataBingdingData, "CalibrationIndex1", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void Cam1AcqEnabel_ValueChanged(object sender, bool value)
        {

            if (!_allowChange)
                return;

            ApplyCamState(value);
        }


        private readonly object _lockCam1 = new object();
        private readonly object _lockCam2 = new object();
        private readonly object _lockCam3 = new object();
        void Camera1Display(HObject Himage)
        {
            lock (_lockCam1)
            {
                if (isCam1AcqSave)
                {
                    isCam1AcqSave = false;
                    WriteImgaesHelper.WriteCalibrationImages(Himage.Clone(), "6", "0", $"1_{GlobalStaticData.UpdataBingdingData.CalibrationIndex1}");
                    _barrier.SignalAndWait();
                }

                // hWindowControl1.HalconWindow.DispObj(Himage.Clone());
                hWindowControl1.HobjectToHimage(Himage.Clone());
             
                this.BeginInvoke(new Action(() => {

                 
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
                    WriteImgaesHelper.WriteCalibrationImages(Himage.Clone(), "6", "1", $"2_{GlobalStaticData.UpdataBingdingData.CalibrationIndex1}");
                    _barrier.SignalAndWait();
                }

                hWindowControl2.HobjectToHimage(Himage.Clone()) ;
                // hWindowControl2.HalconWindow.DispObj(Himage.Clone());
            }

            Himage.Dispose();
        }
        void Camera3Display(HObject Himage)
        {
            lock (_lockCam3)
            {
                if (isCam3AcqSave)
                {
                    isCam3AcqSave = false;
                    WriteImgaesHelper.WriteCalibrationImages(Himage.Clone(), "6", "2", $"3_{GlobalStaticData.UpdataBingdingData.CalibrationIndex1}");
                    _barrier.SignalAndWait();
                    GlobalStaticData.UpdataBingdingData.CalibrationIndex1++;
                }
                // hWindowControl3.HalconWindow.DispObj(Himage.Clone());
                hWindowControl3.HobjectToHimage(Himage.Clone());
            }

            Himage.Dispose();
        }
        bool isCam1AcqSave = false;
        bool isCam2AcqSave = false;
        bool isCam3AcqSave = false;
        private void SaveImages_Click(object sender, EventArgs e)
        {
            SaveImages.Enabled = false;
            isCam1AcqSave = true;
            isCam2AcqSave = true;
            isCam3AcqSave = true;
            UIMessageTip.ShowOk("保存信号发送成功");

        }

        private void SaveImageIndex_ValueChanged(object sender, int value)
        {
            GlobalStaticData.UpdataBingdingData.CalibrationIndex1 = value;
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

                if (_IsContinuousAcqSaveEnable)
                {
                    WriteImgaesHelper.WriteAllImages(Himage.Clone(), "Cam15", "16");
                }
                hWindowControl1.HobjectToHimage(Himage.Clone());


            }
        }

        private void Camera2ContinuousDisplay(HObject Himage)
        {
            lock (_lockCam2)
            {

                if (_IsContinuousAcqSaveEnable)
                {
                    WriteImgaesHelper.WriteAllImages(Himage.Clone(), "Cam16", "17");
                }
                hWindowControl2.HobjectToHimage(Himage.Clone()) ;

            }

            // Himage.Dispose();
        }

        private void Camera3ContinuousDisplay(HObject Himage)
        {
            lock (_lockCam3)
            {

                if (_IsContinuousAcqSaveEnable)
                {
                    WriteImgaesHelper.WriteAllImages(Himage.Clone(), "Cam17", "18");
                }
                hWindowControl3.HobjectToHimage(Himage.Clone() );

            }
        }


        // 用于控制线程的取消令牌
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning = false;
        private void btn_SaveContinuousStart_Click(object sender, EventArgs e)
        {
            if (!_isRunning)
            {
                CalibrationAcqUiStateHelper.StartContinuousCapture(ref _isRunning, ref _cancellationTokenSource, btn_SaveContinuousStart, ContinuousSave);
            }
            else
            {
                CalibrationAcqUiStateHelper.StopContinuousCapture(ref _isRunning, ref _cancellationTokenSource, btn_SaveContinuousStart);
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
                    GlobalStaticData.HIKCamera16.SoftwareTrigger();
                    GlobalStaticData.HIKCamera17.SoftwareTrigger();
                    GlobalStaticData.HIKCamera18.SoftwareTrigger();
                    // 模拟耗时操作（例如每1秒执行一次）
                    if (cancellationToken.WaitHandle.WaitOne(500))
                    {
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 线程被正常取消，不做处理

                this.BeginInvoke(new Action(() => UIMessageTip.ShowWarning("线程已停止")));
            }
            catch (Exception ex)
            {
                // 其他异常处理
                this.BeginInvoke(new Action(() => UIMessageTip.ShowError($"发生错误: {ex.Message}")));
            }
        }

        private void ContinuousAcq_Check_ValueChanged(object sender, bool value)
        {
            if (ContinuousAcq_Check.Active)
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
                GlobalStaticData.HIKCamera16?.TriggerMode(0);
                GlobalStaticData.HIKCamera17?.TriggerMode(0);
                GlobalStaticData.HIKCamera18?.TriggerMode(0);

                GlobalStaticData.HIKCamera16.eventRun -= Camera1Display;
                GlobalStaticData.HIKCamera16.eventRun += Camera1Display;
                GlobalStaticData.HIKCamera17.eventRun -= Camera2Display;
                GlobalStaticData.HIKCamera17.eventRun += Camera2Display;
                GlobalStaticData.HIKCamera18.eventRun -= Camera3Display;
                GlobalStaticData.HIKCamera18.eventRun += Camera3Display;
                this.Invoke(new Action(() => {
                    CalibrationAcqUiStateHelper.SetNormalModeUi(SaveImages, CamContinuousEnable, true);
                }));
            }
            else
            {
                GlobalStaticData.HIKCamera16.TriggerMode(1);
                GlobalStaticData.HIKCamera17.TriggerMode(1);
                GlobalStaticData.HIKCamera18.TriggerMode(1);

                GlobalStaticData.HIKCamera16.eventRun -= Camera1Display;
                GlobalStaticData.HIKCamera17.eventRun -= Camera2Display;
                GlobalStaticData.HIKCamera18.eventRun -= Camera3Display;

                GlobalStaticData.UpdataBingdingData.CalibrationIndex1 = 0;
                    this.Invoke(new Action(() =>
                    {
                        CalibrationAcqUiStateHelper.SetNormalModeUi(SaveImages, CamContinuousEnable, false);
                    }));
            }
        }

        private void ApplyCamSoftState(bool enable)
        {
            if (enable)
            {


                GlobalStaticData.HIKCamera16.TriggerCamera(7);
                GlobalStaticData.HIKCamera17.TriggerCamera(7);
                GlobalStaticData.HIKCamera18.TriggerCamera(7);
                GlobalStaticData.HIKCamera16.eventRun -= Camera1ContinuousDisplay;
                GlobalStaticData.HIKCamera16.eventRun += Camera1ContinuousDisplay;
                GlobalStaticData.HIKCamera17.eventRun -= Camera2ContinuousDisplay;
                GlobalStaticData.HIKCamera17.eventRun += Camera2ContinuousDisplay;
                GlobalStaticData.HIKCamera18.eventRun -= Camera3ContinuousDisplay;
                GlobalStaticData.HIKCamera18.eventRun += Camera3ContinuousDisplay;
                CalibrationAcqUiStateHelper.SetSoftModeUi(btn_SaveContinuousStart, Cam1AcqEnabel, true);


            }
            else
            {


                CalibrationAcqUiStateHelper.StopContinuousCapture(ref _isRunning, ref _cancellationTokenSource, btn_SaveContinuousStart);
                GlobalStaticData.HIKCamera16.TriggerCamera(0);
                GlobalStaticData.HIKCamera17.TriggerCamera(0);
                GlobalStaticData.HIKCamera18.TriggerCamera(0);
                GlobalStaticData.HIKCamera16.eventRun -= Camera1ContinuousDisplay;
                GlobalStaticData.HIKCamera17.eventRun -= Camera2ContinuousDisplay;
                GlobalStaticData.HIKCamera18.eventRun -= Camera3ContinuousDisplay;
                CalibrationAcqUiStateHelper.SetSoftModeUi(btn_SaveContinuousStart, Cam1AcqEnabel, false);
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
