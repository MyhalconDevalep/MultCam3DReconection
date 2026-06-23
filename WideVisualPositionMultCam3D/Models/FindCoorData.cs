using HalconDotNet;


namespace WideVisualPositionMultCam3D.Models
{
    public class FindCoorData
    {
        //计算X坐标+偏移值
        public HTuple WorldX { get; set; }
        //计算Y坐标+偏移值
        public HTuple WorldY { get; set; }
        //计算高度值+偏移值
        public HTuple Height { get; set; }
        //减去编码器值
        public HTuple WorldXScurren { get; set; }
        public HTuple WorldYScurren { get; set; }
       
        //当前坐标处于图像的哪个位置，越靠中分数越高
        public HTuple Score { get; set; }
        public int Attribute { get; set; }
        public bool IsUpDate { get; set; }
        public int SafeRegionMark { get; set; }

        public HTuple pixelRow { get; set; }
        public HTuple pixelCol { get; set; }

        public HTuple ImageHeight { get; set; }
        public HTuple ImageWidtht { get; set; }
        public double MouthWidthMm { get; set; } = -1;
        public double MouthHeightMm { get; set; } = -1;
        public double MouthAverageDiameterMm { get; set; } = -1;

        public long encoding { get; set; }
        public int placeCompensation {  get; set; }
    }
}
