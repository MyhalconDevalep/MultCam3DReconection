using HalconDotNet;
using System;
using WideVisualPositionMultCam3D.Models;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public static class PixelToMmConverter
    {
        public static MouthSizeMm ConvertYoloCenterBoxToMm(
            double centerRow,
            double centerCol,
            double width,
            double height,
            HTuple camParam,
            HTuple planePose)
        {
            double leftCol = centerCol - width / 2.0;
            double rightCol = centerCol + width / 2.0;
            double topRow = centerRow - height / 2.0;
            double bottomRow = centerRow + height / 2.0;

            HTuple widthRows = new HTuple(centerRow, centerRow);
            HTuple widthCols = new HTuple(leftCol, rightCol);

            HOperatorSet.ImagePointsToWorldPlane(
                camParam,
                planePose,
                widthRows,
                widthCols,
                "mm",
                out HTuple widthX,
                out HTuple widthY);

            double realWidthMm = Distance(
                widthX[0].D, widthY[0].D,
                widthX[1].D, widthY[1].D);

            HTuple heightRows = new HTuple(topRow, bottomRow);
            HTuple heightCols = new HTuple(centerCol, centerCol);

            HOperatorSet.ImagePointsToWorldPlane(
                camParam,
                planePose,
                heightRows,
                heightCols,
                "mm",
                out HTuple heightX,
                out HTuple heightY);

            double realHeightMm = Distance(
                heightX[0].D, heightY[0].D,
                heightX[1].D, heightY[1].D);

            return new MouthSizeMm
            {
                WidthMm = realWidthMm,
                HeightMm = realHeightMm,
                CenterRow = centerRow,
                CenterCol = centerCol
            };
        }

        private static double Distance(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
