using System.ComponentModel;

namespace WideVisualPositionMultCam3D.Models
{
    public class UpdataBingdingData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
           
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _calibrationIndex1=0;

        public int CalibrationIndex1
        {
            get { return _calibrationIndex1; }
            set {
                    if (_calibrationIndex1 != value)
                    {
                        _calibrationIndex1 = value;
                        OnPropertyChanged("CalibrationIndex1");
                    }
                }
        }

       
        //private float _conf_threshold=0.5f;

        //public float Conf_threshold
        //{
        //    get { return _conf_threshold; }
        //    set 
        //    {
        //        if (_conf_threshold != value)
        //        {


        //            _conf_threshold = value;
        //            OnPropertyChanged("Conf_threshold");
        //        }
        //    }
        //}

        //private float _nms_threshold=0.5f;

        //public float Nms_threshold
        //{
        //    get { return _nms_threshold; }
        //    set { _nms_threshold = value; }
        //}



        //private int _positioningTolerance;

        //public int PositioningTolerance
        //{
        //    get { return _positioningTolerance; }
        //    set 
        //    {
        //        if (_positioningTolerance != value)
        //        {
        //            _positioningTolerance = value;
        //            OnPropertyChanged("PositioningTolerance");
        //        }
        //    }
        //}

        //private int _calibrationBoardHeight;

        //public int CalibrationBoardHeight
        //{
        //    get { return _calibrationBoardHeight; }
        //    set 
        //    {
        //        if (_calibrationBoardHeight != value)
        //        {


        //            _calibrationBoardHeight = value;
        //            OnPropertyChanged("CalibrationBoardHeight");
        //        }
        //    }
        //}

        private int _bottleTolerance;

        public int BottleTolerance
        {
            get { return _bottleTolerance; }
            set 
            {
                if (_bottleTolerance != value)
                {
                    _bottleTolerance = value;
                    OnPropertyChanged("BottleTolerance");
                }
            }
        }

        private int _xCommandPoint;

        public int XCommandPoint
        {
            get { return _xCommandPoint; }
            set 
            {
                if (_xCommandPoint != value)
                {
                    _xCommandPoint = value;
                    OnPropertyChanged("XCommandPoint");
                }
            }
        }

        private int _safetyClearance;

        public int SafetyClearance
        {
            get { return _safetyClearance; }
            set 
            {
                if (_safetyClearance != value)
                {
                    _safetyClearance = value;
                    OnPropertyChanged("SafetyClearance");
                }
            }
        }

        private int _minHeight;

        public int MinHeight
        {
            get { return _minHeight; }
            set 
            {
                if (_minHeight != value)
                {
                    _minHeight = value;
                    OnPropertyChanged("MinHeight");
                }
            }
        }

        private int _maxHeight;

        public int MaxHeight
        {
            get { return _maxHeight; }
            set { 
                if (_maxHeight != value)
                {
                    _maxHeight = value;
                    OnPropertyChanged("MaxHeight");
                }

            }
        }


        private int _robot1Threshold;

        public int Robot1Threshold
        {
            get { return _robot1Threshold; }
            set 
            {
                if (_robot1Threshold != value)
                {
                    _robot1Threshold = value;
                    OnPropertyChanged("Robot1Threshold");
                }
            }
        }

        private int _robot2Threshold;

        public int Robot2Threshold
        {
            get { return _robot2Threshold; }
            set
            {
                if (_robot2Threshold != value)
                {
                    _robot2Threshold = value;
                    OnPropertyChanged("Robot2Threshold");
                }
            }
        }


        private int _cam1xOffset;

        public int Cam1XOffset
        {
            get { return _cam1xOffset; }
            set
            {
                if (_cam1xOffset != value)
                {
                    _cam1xOffset = value;
                    OnPropertyChanged("Cam1XOffset");
                }
            }
        }

        private int _cam1yOffset;

        public int Cam1YOffset
        {
            get { return _cam1yOffset; }
            set
            {
                if (_cam1yOffset != value)
                {
                    _cam1yOffset = value;
                    OnPropertyChanged("Cam1YOffset");
                }
            }
        }


        private int _cam1zOffset;

        public int Cam1ZOffset
        {
            get { return _cam1zOffset; }
            set
            {
                if (_cam1zOffset != value)
                {
                    _cam1zOffset = value;
                    OnPropertyChanged("Cam1ZOffset");
                }
            }
        }

        private double cam1RzOffset;

        public double Cam1RzOffset
        {
            get { return cam1RzOffset; }
            set {
                if (cam1RzOffset != value)
                {
                    cam1RzOffset = value;
                    OnPropertyChanged("Cam1RzOffset");
                }
                 }
        }


    }
}
