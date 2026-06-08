namespace WideVisualPositionMultCam3D.Models
{
    public class MouthSizeMm
    {
        public double WidthMm { get; set; }
        public double HeightMm { get; set; }
        public double CenterRow { get; set; }
        public double CenterCol { get; set; }

        public double AverageDiameterMm
        {
            get { return (WidthMm + HeightMm) / 2.0; }
        }
    }
}
