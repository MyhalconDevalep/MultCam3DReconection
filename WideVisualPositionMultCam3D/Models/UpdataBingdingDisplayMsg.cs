using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WideVisualPositionMultCam3D.Models
{
    public class UpdataBingdingDisplayMsg : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private long _encoding = 0;

        public long Encoding
        {
            get { return _encoding; }
            set
            {
                if (_encoding != value)
                {
                    _encoding = value;
                    OnPropertyChanged("Encoding");
                }
            }
        }

        private string _connectStatus = "未连接";

        public string ConnectStatus
        {
            get { return _connectStatus; }
            set
            {
                if (_connectStatus != value)
                {
                    _connectStatus = value;

                    OnPropertyChanged("ConnectStatus");
                }

            }
        }

        private int _robotUseData1;
        public int RobotUseData1
        {
            get { return _robotUseData1; }
            set {
                if (_robotUseData1 != value)
                {
                    _robotUseData1 = value;
                    OnPropertyChanged("RobotUseData1");
                }
            }
        }

        private int _robotUseData2;
        public int RobotUseData2
        {
            get { return _robotUseData2; }
            set
            {
                if (_robotUseData2 != value)
                {
                    _robotUseData2 = value;
                    OnPropertyChanged("RobotUseData2");
                }
            }
        }

        private int _robotUseData3;
        public int RobotUseData3
        {
            get { return _robotUseData3; }
            set
            {
                if (_robotUseData3 != value)
                {
                    _robotUseData3 = value;
                    OnPropertyChanged("RobotUseData3");
                }
            }
        }

        private int _userPower=0;

        public int UserPower
        {
            get { return _userPower; }
            set {
                if (_userPower != value)
                {
                    _userPower = value;
                    OnPropertyChanged("UserPower");
                }
               
            }
        }


        private int _runTime;

        public int RunTime
        {
            get { return _runTime; }
            set { 
        
                if (_runTime != value)
                {
                    _runTime = value;
                    OnPropertyChanged("RunTime");
                }
            }
        }

        private int _cacheNum;

        public int CacheNum
        {
            get { return _cacheNum;; }
            set { 
             
                if (_cacheNum != value)
                {
                    _cacheNum = value;
                    OnPropertyChanged("CacheNum");
                }
            }
        }


        private int _sendDataNum;

        public int SendDataNum
        {
            get { return _sendDataNum; }
            set {
       
                if (_sendDataNum != value)
                {
                    _sendDataNum = value;
                    OnPropertyChanged("SendDataNum");
                }
            }
        }



    }
}
