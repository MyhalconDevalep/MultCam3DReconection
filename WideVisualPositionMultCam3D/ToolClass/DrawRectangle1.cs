using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class DrawRectangle1
    {
        public HWindow Window;
        public HObject Image;
        public List<Roi> RoiList;
        public int SelectIndexRoi=0;
        public int NumHandles = 5;
        public bool IsdisplayDrawRectangle = false;
        public DrawRectangle1(HWindow window)
        {
            Window = window;
            RoiList = new List<Roi>();
        }

        public void DisplayImage()
        {
            if(Image != null)
            {
                Window.DispObj(Image);
            }
            IsdisplayDrawRectangle = false;
        }

        public void GetRectagle1Coor(out HTuple row1,out HTuple col1,out HTuple row2,out HTuple col2)
        {
            if (RoiList.Count > 0)
            {
                row1 = RoiList[SelectIndexRoi].Row1;
                col1 = RoiList[SelectIndexRoi].Col1;
                row2 = RoiList[SelectIndexRoi].Row2;
                col2 = RoiList[SelectIndexRoi].Col2;
            }
            else
            {
                row1 = null;
                col1 = null;
                row2 = null;
                col2 = null;
            }

           
        }


        public HObject GetRectangle1Region()
        {
            if(RoiList.Count > 0)
            {
                DisplayImage();
                HOperatorSet.GenRectangle1(out HObject rectangle1, RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col1, RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col2);
                Window.DispObj(rectangle1);
                return rectangle1;
            }
            else
            {
                return null;
            }
           

        }

        public void GenRectangle1(HTuple row1, HTuple col1,HTuple row2,HTuple col2,int size,ref HObject regions)
        {
            RoiList.Add(new Roi()
            {
                LineType = "Rectangle1",
                Color = "yellow",
                Size = new System.Drawing.Size(size, size),
                Row1 = row1.D,
                Col1 = col1.D,
                Row2 = row2.D,
                Col2 = col2.D,
                MidR = (row1.D + row2.D) / 2,
                MidC = (col1.D + col2.D) / 2,
                Angle = 0

            });
            DrawsRectangle1();
            IsdisplayDrawRectangle = true;

        }

        public void DrawsRectangle1()
        {
            HSystem.SetSystem("flush_graphic", "false");
            Window.SetDraw("margin");
            Window.SetColor(RoiList[SelectIndexRoi].Color);
            Window.SetLineWidth(1);
            Window.ClearWindow();
            Window.DispObj(Image);
            Window.DispRectangle1(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col1, RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col2);
           
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col1, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col2, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col2, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col1, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].MidR, RoiList[SelectIndexRoi].MidC, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);

            Window.SetColor("red");
            displayActive(4);
            HSystem.SetSystem("flush_graphic", "true");

        }



        public void RepaintRectangle1()
        {
            HSystem.SetSystem("flush_graphic", "false");
            Window.SetDraw("margin");
            Window.SetColor(RoiList[SelectIndexRoi].Color);
            Window.SetLineWidth(1);
            Window.ClearWindow();
            Window.DispObj(Image);
            Window.DispRectangle1(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col1, RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col2);

            Window.DispRectangle2(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col1, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col2, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col2, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col1, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].MidR, RoiList[SelectIndexRoi].MidC, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            HSystem.SetSystem("flush_graphic", "true");

        }

        public void MoveRepaintRectangle1()
        {

            HSystem.SetSystem("flush_graphic", "false");
            Window.SetDraw("margin");
            Window.SetColor("yellow");
            Window.SetColor(RoiList[SelectIndexRoi].Color);
            Window.SetLineWidth(1);
            Window.ClearWindow();
            Window.DispObj(Image);
            Window.DispRectangle1(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col1, RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col2);

            Window.DispRectangle2(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col1, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col2, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col2, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col1, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.DispRectangle2(RoiList[SelectIndexRoi].MidR, RoiList[SelectIndexRoi].MidC, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
            Window.SetColor("red");
            displayActive(activeHandleIdx);
            HSystem.SetSystem("flush_graphic", "true");
        }


        /// <summary> 
		/// Paints the active handle of the ROI object into the supplied window
		/// </summary>
		/// <param name="window">HALCON window</param>
		public  void displayActive(int activeHandleIdx)
        {
            switch (activeHandleIdx)
            {
                case 0:
                    Window.DispRectangle2(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col1, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
                    break;
                case 1:
                    Window.DispRectangle2(RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col2, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
                    break;
                case 2:
                    Window.DispRectangle2(RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col2, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
                    break;
                case 3:
                    Window.DispRectangle2(RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col1, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
                    break;
                case 4:
                    Window.DispRectangle2(RoiList[SelectIndexRoi].MidR, RoiList[SelectIndexRoi].MidC, 0, RoiList[SelectIndexRoi].Size.Width, RoiList[SelectIndexRoi].Size.Height);
                    break;
            }
        }


        private int activeHandleIdx = 0;
        public  double distToClosestHandle(double x, double y)
        {

            double max = 35;
            double[] val = new double[NumHandles];

            val[0] = HMisc.DistancePp(y, x,RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col1); // upper left 
            val[1] = HMisc.DistancePp(y, x, RoiList[SelectIndexRoi].Row1, RoiList[SelectIndexRoi].Col2); // upper right 
            val[2] = HMisc.DistancePp(y, x, RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col2); // lower right 
            val[3] = HMisc.DistancePp(y, x, RoiList[SelectIndexRoi].Row2, RoiList[SelectIndexRoi].Col1); // lower left 
            val[4] = HMisc.DistancePp(y, x, RoiList[SelectIndexRoi].MidR, RoiList[SelectIndexRoi].MidC); // midpoint 

            for (int i = 0; i < NumHandles; i++)
            {
                if (val[i] < max)
                {
                    max = val[i];
                    activeHandleIdx = i;
                }
            }// end of for 

            return val[activeHandleIdx];
        }


        double max = 10000;
        double epsilon = 35.0;

        public int mouseDownAction(double imgX, double imgY)
        {
            double dist = 0;
            if (RoiList.Count > 0&&IsdisplayDrawRectangle)     // ... or an existing one is manipulated
            {
               
                RepaintRectangle1();
       
                    dist =distToClosestHandle(imgX, imgY);
                    if ((dist < max) && (dist < epsilon))
                    {
                        max = dist;
                       
                    }
                Window.SetColor("red");
                displayActive(activeHandleIdx);
            }
            else
            {
                return -1;
            }
            return activeHandleIdx;
        }

        double currX = 0;
        double currY = 0;

        public void mouseMoveAction(double newX, double newY)
        {
            try
            {
                //if (EditModel == false) return;
                if ((newX == currX) && (newY == currY))
                    return;
                if(RoiList.Count > 0&&IsdisplayDrawRectangle)
                {
                    moveByHandle(newX, newY);
                    MoveRepaintRectangle1();

                    currX = newX;
                    currY = newY;
                }
               
               
            }
            catch (Exception)
            {
                //没有显示roi的时候 移动鼠标会报错
            }

        }



        /// <summary> 
        /// Recalculates the shape of the ROI instance. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y)
        /// </summary>
        /// <param name="newX">x mouse coordinate</param>
        /// <param name="newY">y mouse coordinate</param>
        public  void moveByHandle(double newX, double newY)
        {
            double len1, len2;
            double tmp;

            switch (activeHandleIdx)
            {
                case 0: // upper left 
                    RoiList[SelectIndexRoi].Row1 = newY;
                    RoiList[SelectIndexRoi].Col1 = newX;
                    break;
                case 1: // upper right 
                    RoiList[SelectIndexRoi].Row1 = newY;
                    RoiList[SelectIndexRoi].Col2 = newX;
                    break;
                case 2: // lower right 
                    RoiList[SelectIndexRoi].Row2 = newY;
                    RoiList[SelectIndexRoi].Col2 = newX;
                    break;
                case 3: // lower left
                    RoiList[SelectIndexRoi].Row2 = newY;
                    RoiList[SelectIndexRoi].Col1 = newX;
                    break;
                case 4: // midpoint 
                    len1 = ((RoiList[SelectIndexRoi].Row2 - RoiList[SelectIndexRoi].Row1) / 2);
                    len2 = ((RoiList[SelectIndexRoi].Col2 - RoiList[SelectIndexRoi].Col1) / 2);

                    RoiList[SelectIndexRoi].Row1 = newY - len1;
                    RoiList[SelectIndexRoi].Row2 = newY + len1;

                    RoiList[SelectIndexRoi].Col1 = newX - len2;
                    RoiList[SelectIndexRoi].Col2 = newX + len2;

                    break;
            }

            if (RoiList[SelectIndexRoi].Row2 <= RoiList[SelectIndexRoi].Row1)
            {
                tmp = RoiList[SelectIndexRoi].Row1;
                RoiList[SelectIndexRoi].Row1 = RoiList[SelectIndexRoi].Row2;
                RoiList[SelectIndexRoi].Row2 = tmp;
            }

            if (RoiList[SelectIndexRoi].Col2 <= RoiList[SelectIndexRoi].Col1)
            {
                tmp = RoiList[SelectIndexRoi].Col1;
                RoiList[SelectIndexRoi].Col1 = RoiList[SelectIndexRoi].Col2;
                RoiList[SelectIndexRoi].Col2 = tmp;
            }

            RoiList[SelectIndexRoi].MidR = ((RoiList[SelectIndexRoi].Row2 - RoiList[SelectIndexRoi].Row1) / 2) + RoiList[SelectIndexRoi].Row1;
            RoiList[SelectIndexRoi].MidC = ((RoiList[SelectIndexRoi].Col2 - RoiList[SelectIndexRoi].Col1) / 2) + RoiList[SelectIndexRoi].Col1;

        }//end of method


        string text = string.Empty;
        double text_Row = 0;
        double text_Col = 0;
        string text_color = string.Empty;
        int text_size = 15;

        public void dispText(string hText, double row,double column, string color,int size)
        {
             text = hText;
             text_Row = row;
             text_Col = column;
             text_color = color;
             text_size = size;
            dispText_local();
        }

        public void dispText_local()
        {
            lock (this)
            {
                if (text != string.Empty)
                {
                    set_display_font(Window, text_size, "宋体", "true", "false");
                    Window.DispText(text, "image", text_Row, text_Col, text_color, "box", "false");
                }
            }
        }


        public void ResizeChanged()
        {
            SetHalconScalingZoom();
            DisplayImage();
            GetRectangle1Region();
            dispText_local();
        }


        private void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font,
HTuple hv_Bold, HTuple hv_Slant)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_OS = new HTuple(), hv_Fonts = new HTuple();
            HTuple hv_Style = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_AvailableFonts = new HTuple(), hv_Fdx = new HTuple();
            HTuple hv_Indices = new HTuple();
            HTuple hv_Font_COPY_INP_TMP = new HTuple(hv_Font);
            HTuple hv_Size_COPY_INP_TMP = new HTuple(hv_Size);

            // Initialize local and output iconic variables 
            try
            {
                //This procedure sets the text font of the current window with
                //the specified attributes.
                //
                //Input parameters:
                //WindowHandle: The graphics window for which the font will be set
                //Size: The font size. If Size=-1, the default of 16 is used.
                //Bold: If set to 'true', a bold font is used
                //Slant: If set to 'true', a slanted font is used
                //
                hv_OS.Dispose();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
                {
                    hv_Size_COPY_INP_TMP.Dispose();
                    hv_Size_COPY_INP_TMP = 16;
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    //Restore previous behaviour
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = ((1.13677 * hv_Size_COPY_INP_TMP)).TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = hv_Size_COPY_INP_TMP.TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Courier";
                    hv_Fonts[1] = "Courier 10 Pitch";
                    hv_Fonts[2] = "Courier New";
                    hv_Fonts[3] = "CourierNew";
                    hv_Fonts[4] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Consolas";
                    hv_Fonts[1] = "Menlo";
                    hv_Fonts[2] = "Courier";
                    hv_Fonts[3] = "Courier 10 Pitch";
                    hv_Fonts[4] = "FreeMono";
                    hv_Fonts[5] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Luxi Sans";
                    hv_Fonts[1] = "DejaVu Sans";
                    hv_Fonts[2] = "FreeSans";
                    hv_Fonts[3] = "Arial";
                    hv_Fonts[4] = "Liberation Sans";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Times New Roman";
                    hv_Fonts[1] = "Luxi Serif";
                    hv_Fonts[2] = "DejaVu Serif";
                    hv_Fonts[3] = "FreeSerif";
                    hv_Fonts[4] = "Utopia";
                    hv_Fonts[5] = "Liberation Serif";
                }
                else
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple(hv_Font_COPY_INP_TMP);
                }
                hv_Style.Dispose();
                hv_Style = "";
                if ((int)(new HTuple(hv_Bold.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Bold";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Bold.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Italic";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Slant.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Style.TupleEqual(""))) != 0)
                {
                    hv_Style.Dispose();
                    hv_Style = "Normal";
                }
                hv_AvailableFonts.Dispose();
                HOperatorSet.QueryFont(hv_WindowHandle, out hv_AvailableFonts);
                hv_Font_COPY_INP_TMP.Dispose();
                hv_Font_COPY_INP_TMP = "";
                for (hv_Fdx = 0; (int)hv_Fdx <= (int)((new HTuple(hv_Fonts.TupleLength())) - 1); hv_Fdx = (int)hv_Fdx + 1)
                {
                    hv_Indices.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Indices = hv_AvailableFonts.TupleFind(
                            hv_Fonts.TupleSelect(hv_Fdx));
                    }
                    if ((int)(new HTuple((new HTuple(hv_Indices.TupleLength())).TupleGreater(
                        0))) != 0)
                    {
                        if ((int)(new HTuple(((hv_Indices.TupleSelect(0))).TupleGreaterEqual(0))) != 0)
                        {
                            hv_Font_COPY_INP_TMP.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(
                                    hv_Fdx);
                            }
                            break;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(""))) != 0)
                {
                    throw new HalconException("Wrong value of control parameter Font");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Font = (((hv_Font_COPY_INP_TMP + "-") + hv_Style) + "-") + hv_Size_COPY_INP_TMP;
                        hv_Font_COPY_INP_TMP.Dispose();
                        hv_Font_COPY_INP_TMP = ExpTmpLocalVar_Font;
                    }
                }
                HOperatorSet.SetFont(hv_WindowHandle, hv_Font_COPY_INP_TMP);

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void SetHalconScalingZoom()
        {
            try
            {
                HTuple dispWidth = 0;
                HTuple dispHeight = 0;
                HTuple offset = 0;
                HTuple colum = 0;
                HTuple Row = 0;
                if (Image == null)
                    return;
                HOperatorSet.GetImageSize(Image, out HTuple width, out HTuple height);
                int windowWidth = 0;
                int windowHeight = 0;
                Window.GetWindowExtents(out int rowWindow, out int colWindow, out windowWidth, out windowHeight);
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
                        Window.SetPart(-offset, colum, dispHeight - offset, dispWidth);
                    }
                    else
                    {
                        dispWidth = height * winWHRatio;
                        dispHeight = height;
                        offset = (dispWidth - width) / 2;
                        Row = 0;
                        Window.SetPart(Row, -offset, dispHeight, dispWidth - offset);
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
                        Window.SetPart(-offset, colum, dispHeight - offset, width);
                    }
                    else
                    {
                        dispWidth = height * winWHRatio;
                        dispHeight = height;
                        offset = (dispWidth - width) / 2;
                        Row = 0;
                        Window.SetPart(Row, -offset, dispHeight, dispWidth - offset);

                    }
                }
            }
            catch (Exception)
            {

            }


        }


    }

    public class Roi 
    {
        public string LineType {  get; set; }
        public string Color { get; set; } = "yellow";
        public System.Drawing.Size Size { get; set; }=new System.Drawing.Size(8,8);
        public double Row1 { get; set; }   // upper left
        public double Col1{get; set; }
        public double Row2 {  get; set; }
        public double Col2 { get; set; }  // lower right 
        public double MidR { get; set; }
        public double MidC { get; set; }
        public double Angle { get; set; }

    }
}
