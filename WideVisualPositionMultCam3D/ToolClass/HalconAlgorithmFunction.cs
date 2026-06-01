using HalconDotNet;
using System;
using System.Threading;


namespace WideVisualPositionMultCam3D.ToolClass
{
    public class HalconAlgorithmFunction
    {
        public void gen_cam_par_area_scan_division(HTuple hv_Focus, HTuple hv_Kappa, HTuple hv_Sx,
    HTuple hv_Sy, HTuple hv_Cx, HTuple hv_Cy, HTuple hv_ImageWidth, HTuple hv_ImageHeight,
    out HTuple hv_CameraParam)
        {



            // Local iconic variables 
            // Initialize local and output iconic variables 
            hv_CameraParam = new HTuple();
            //Generate a camera parameter tuple for an area scan camera
            //with distortions modeled by the division model.
            //
            hv_CameraParam.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_CameraParam = new HTuple();
                hv_CameraParam[0] = "area_scan_division";
                hv_CameraParam = hv_CameraParam.TupleConcat(hv_Focus, hv_Kappa, hv_Sx, hv_Sy, hv_Cx, hv_Cy, hv_ImageWidth, hv_ImageHeight);
            }


            return;
        }

        public void list_image_files(HTuple hv_ImageDirectory, HTuple hv_Extensions, HTuple hv_Options,
    out HTuple hv_ImageFiles)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ImageDirectoryIndex = new HTuple();
            HTuple hv_ImageFilesTmp = new HTuple(), hv_CurrentImageDirectory = new HTuple();
            HTuple hv_HalconImages = new HTuple(), hv_OS = new HTuple();
            HTuple hv_Directories = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Length = new HTuple(), hv_NetworkDrive = new HTuple();
            HTuple hv_Substring = new HTuple(), hv_FileExists = new HTuple();
            HTuple hv_AllFiles = new HTuple(), hv_i = new HTuple();
            HTuple hv_Selection = new HTuple();
            HTuple hv_Extensions_COPY_INP_TMP = new HTuple(hv_Extensions);

            // Initialize local and output iconic variables 
            hv_ImageFiles = new HTuple();
            try
            {
                //This procedure returns all files in a given directory
                //with one of the suffixes specified in Extensions.
                //
                //Input parameters:
                //ImageDirectory: Directory or a tuple of directories with images.
                //   If a directory is not found locally, the respective directory
                //   is searched under %HALCONIMAGES%/ImageDirectory.
                //   See the Installation Guide for further information
                //   in case %HALCONIMAGES% is not set.
                //Extensions: A string tuple containing the extensions to be found
                //   e.g. ['png','tif',jpg'] or others
                //If Extensions is set to 'default' or the empty string '',
                //   all image suffixes supported by HALCON are used.
                //Options: as in the operator list_files, except that the 'files'
                //   option is always used. Note that the 'directories' option
                //   has no effect but increases runtime, because only files are
                //   returned.
                //
                //Output parameter:
                //ImageFiles: A tuple of all found image file names
                //
                if ((int)((new HTuple((new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(""))))).TupleOr(new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(
                    "default")))) != 0)
                {
                    hv_Extensions_COPY_INP_TMP.Dispose();
                    hv_Extensions_COPY_INP_TMP = new HTuple();
                    hv_Extensions_COPY_INP_TMP[0] = "ima";
                    hv_Extensions_COPY_INP_TMP[1] = "tif";
                    hv_Extensions_COPY_INP_TMP[2] = "tiff";
                    hv_Extensions_COPY_INP_TMP[3] = "gif";
                    hv_Extensions_COPY_INP_TMP[4] = "bmp";
                    hv_Extensions_COPY_INP_TMP[5] = "jpg";
                    hv_Extensions_COPY_INP_TMP[6] = "jpeg";
                    hv_Extensions_COPY_INP_TMP[7] = "jp2";
                    hv_Extensions_COPY_INP_TMP[8] = "jxr";
                    hv_Extensions_COPY_INP_TMP[9] = "png";
                    hv_Extensions_COPY_INP_TMP[10] = "pcx";
                    hv_Extensions_COPY_INP_TMP[11] = "ras";
                    hv_Extensions_COPY_INP_TMP[12] = "xwd";
                    hv_Extensions_COPY_INP_TMP[13] = "pbm";
                    hv_Extensions_COPY_INP_TMP[14] = "pnm";
                    hv_Extensions_COPY_INP_TMP[15] = "pgm";
                    hv_Extensions_COPY_INP_TMP[16] = "ppm";
                    //
                }
                hv_ImageFiles.Dispose();
                hv_ImageFiles = new HTuple();
                //Loop through all given image directories.
                for (hv_ImageDirectoryIndex = 0; (int)hv_ImageDirectoryIndex <= (int)((new HTuple(hv_ImageDirectory.TupleLength()
                    )) - 1); hv_ImageDirectoryIndex = (int)hv_ImageDirectoryIndex + 1)
                {
                    hv_ImageFilesTmp.Dispose();
                    hv_ImageFilesTmp = new HTuple();
                    hv_CurrentImageDirectory.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CurrentImageDirectory = hv_ImageDirectory.TupleSelect(
                            hv_ImageDirectoryIndex);
                    }
                    if ((int)(new HTuple(hv_CurrentImageDirectory.TupleEqual(""))) != 0)
                    {
                        hv_CurrentImageDirectory.Dispose();
                        hv_CurrentImageDirectory = ".";
                    }
                    hv_HalconImages.Dispose();
                    HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
                    hv_OS.Dispose();
                    HOperatorSet.GetSystem("operating_system", out hv_OS);
                    if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                    ";");
                                hv_HalconImages.Dispose();
                                hv_HalconImages = ExpTmpLocalVar_HalconImages;
                            }
                        }
                    }
                    else
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                    ":");
                                hv_HalconImages.Dispose();
                                hv_HalconImages = ExpTmpLocalVar_HalconImages;
                            }
                        }
                    }
                    hv_Directories.Dispose();
                    hv_Directories = new HTuple(hv_CurrentImageDirectory);
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_HalconImages.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_Directories = hv_Directories.TupleConcat(
                                    ((hv_HalconImages.TupleSelect(hv_Index)) + "/") + hv_CurrentImageDirectory);
                                hv_Directories.Dispose();
                                hv_Directories = ExpTmpLocalVar_Directories;
                            }
                        }
                    }
                    hv_Length.Dispose();
                    HOperatorSet.TupleStrlen(hv_Directories, out hv_Length);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_NetworkDrive.Dispose();
                        HOperatorSet.TupleGenConst(new HTuple(hv_Length.TupleLength()), 0, out hv_NetworkDrive);
                    }
                    if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                    {
                        for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength()
                            )) - 1); hv_Index = (int)hv_Index + 1)
                        {
                            if ((int)(new HTuple(((((hv_Directories.TupleSelect(hv_Index))).TupleStrlen()
                                )).TupleGreater(1))) != 0)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_Substring.Dispose();
                                    HOperatorSet.TupleStrFirstN(hv_Directories.TupleSelect(hv_Index), 1,
                                        out hv_Substring);
                                }
                                if ((int)((new HTuple(hv_Substring.TupleEqual("//"))).TupleOr(new HTuple(hv_Substring.TupleEqual(
                                    "\\\\")))) != 0)
                                {
                                    if (hv_NetworkDrive == null)
                                        hv_NetworkDrive = new HTuple();
                                    hv_NetworkDrive[hv_Index] = 1;
                                }
                            }
                        }
                    }
                    hv_ImageFilesTmp.Dispose();
                    hv_ImageFilesTmp = new HTuple();
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Directories.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_FileExists.Dispose();
                            HOperatorSet.FileExists(hv_Directories.TupleSelect(hv_Index), out hv_FileExists);
                        }
                        if ((int)(hv_FileExists) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_AllFiles.Dispose();
                                HOperatorSet.ListFiles(hv_Directories.TupleSelect(hv_Index), (new HTuple("files")).TupleConcat(
                                    hv_Options), out hv_AllFiles);
                            }
                            hv_ImageFilesTmp.Dispose();
                            hv_ImageFilesTmp = new HTuple();
                            for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Extensions_COPY_INP_TMP.TupleLength()
                                )) - 1); hv_i = (int)hv_i + 1)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_Selection.Dispose();
                                    HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP.TupleSelect(
                                        hv_i))) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_ImageFilesTmp = hv_ImageFilesTmp.TupleConcat(
                                            hv_Selection);
                                        hv_ImageFilesTmp.Dispose();
                                        hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                    }
                                }
                            }
                            {
                                HTuple ExpTmpOutVar_0;
                                HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("\\\\")).TupleConcat(
                                    "replace_all"), "/", out ExpTmpOutVar_0);
                                hv_ImageFilesTmp.Dispose();
                                hv_ImageFilesTmp = ExpTmpOutVar_0;
                            }
                            if ((int)(hv_NetworkDrive.TupleSelect(hv_Index)) != 0)
                            {
                                {
                                    HTuple ExpTmpOutVar_0;
                                    HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                        "replace_all"), "/", out ExpTmpOutVar_0);
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpOutVar_0;
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_ImageFilesTmp = "/" + hv_ImageFilesTmp;
                                        hv_ImageFilesTmp.Dispose();
                                        hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                    }
                                }
                            }
                            else
                            {
                                {
                                    HTuple ExpTmpOutVar_0;
                                    HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                        "replace_all"), "/", out ExpTmpOutVar_0);
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpOutVar_0;
                                }
                            }
                            break;
                        }
                    }
                    //Concatenate the output image paths.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ImageFiles = hv_ImageFiles.TupleConcat(
                                hv_ImageFilesTmp);
                            hv_ImageFiles.Dispose();
                            hv_ImageFiles = ExpTmpLocalVar_ImageFiles;
                        }
                    }
                }

                hv_Extensions_COPY_INP_TMP.Dispose();
                hv_ImageDirectoryIndex.Dispose();
                hv_ImageFilesTmp.Dispose();
                hv_CurrentImageDirectory.Dispose();
                hv_HalconImages.Dispose();
                hv_OS.Dispose();
                hv_Directories.Dispose();
                hv_Index.Dispose();
                hv_Length.Dispose();
                hv_NetworkDrive.Dispose();
                hv_Substring.Dispose();
                hv_FileExists.Dispose();
                hv_AllFiles.Dispose();
                hv_i.Dispose();
                hv_Selection.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Extensions_COPY_INP_TMP.Dispose();
                hv_ImageDirectoryIndex.Dispose();
                hv_ImageFilesTmp.Dispose();
                hv_CurrentImageDirectory.Dispose();
                hv_HalconImages.Dispose();
                hv_OS.Dispose();
                hv_Directories.Dispose();
                hv_Index.Dispose();
                hv_Length.Dispose();
                hv_NetworkDrive.Dispose();
                hv_Substring.Dispose();
                hv_FileExists.Dispose();
                hv_AllFiles.Dispose();
                hv_i.Dispose();
                hv_Selection.Dispose();

                throw HDevExpDefaultException;
            }
        }
        // Local procedures 
        // Local procedures 
        public void Calibration_mask(HTuple hv_Calibration_image_path,ref HTuple hv_CalibDataID, HWindow hWindow1, HWindow hWindow2, HWindow hWindow3,
            out HTuple hv_Errors)
        {



            // Local iconic variables 

            HObject ho_ImageCalibration0 = null, ho_ContoursCali0 = null;
            HObject ho_ImageCalibration1 = null, ho_ContoursCali1 = null;
            HObject ho_ImageCalibration2 = null, ho_ContoursCali2 = null;

            // Local control variables 

            HTuple hv_ImageFiles0 = new HTuple(), hv_ImageFiles1 = new HTuple();
            HTuple hv_ImageFiles2 = new HTuple(), hv_len1 = new HTuple();
            HTuple hv_len2 = new HTuple(), hv_len3 = new HTuple();
            HTuple hv_I = new HTuple(), hv_Exception = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageCalibration0);
            HOperatorSet.GenEmptyObj(out ho_ContoursCali0);
            HOperatorSet.GenEmptyObj(out ho_ImageCalibration1);
            HOperatorSet.GenEmptyObj(out ho_ContoursCali1);
            HOperatorSet.GenEmptyObj(out ho_ImageCalibration2);
            HOperatorSet.GenEmptyObj(out ho_ContoursCali2);
            hv_Errors = new HTuple();
            try
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ImageFiles0.Dispose();
                    list_image_files(hv_Calibration_image_path + "0", "default", new HTuple(), out hv_ImageFiles0);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ImageFiles1.Dispose();
                    list_image_files(hv_Calibration_image_path + "1", "default", new HTuple(), out hv_ImageFiles1);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ImageFiles2.Dispose();
                    list_image_files(hv_Calibration_image_path + "2", "default", new HTuple(), out hv_ImageFiles2);
                }
                hv_len1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_len1 = new HTuple(hv_ImageFiles0.TupleLength()
                        );
                }
                hv_len2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_len2 = new HTuple(hv_ImageFiles1.TupleLength()
                        );
                }
                hv_len3.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_len3 = new HTuple(hv_ImageFiles2.TupleLength()
                        );
                }
                if ((int)((new HTuple(hv_len1.TupleEqual(hv_len2))).TupleAnd(new HTuple(hv_len2.TupleEqual(
                    hv_len3)))) != 0)
                {
                    //遍历每帧图像，从三个相机读取
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_ImageFiles0.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        //相机0（图像路径类似 '1_1.bmp'）
                        try
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_ImageCalibration0.Dispose();
                                HOperatorSet.ReadImage(out ho_ImageCalibration0, hv_ImageFiles0.TupleSelect(
                                    hv_I));
                            }
                            HOperatorSet.FindCalibObject(ho_ImageCalibration0, hv_CalibDataID, 0,
                                0, hv_I, new HTuple(), new HTuple());
                            ho_ContoursCali0.Dispose();
                            HOperatorSet.GetCalibDataObservContours(out ho_ContoursCali0, hv_CalibDataID,
                                "marks", 0, 0, hv_I);
                            
                          //  GlobalStaticData.displayConvert.SetHalconScalingZoom(ho_ContoursCali0.Clone(), hWindow1);
                            hWindow1.DispObj(ho_ImageCalibration0);
                            hWindow1.SetColor("red");
                            hWindow1.DispObj(ho_ContoursCali0);
                            Thread.Sleep(1000);
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        }

                    }

                    //遍历每帧图像，从三个相机读取
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_ImageFiles1.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        //相机1
                        try
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_ImageCalibration1.Dispose();
                                HOperatorSet.ReadImage(out ho_ImageCalibration1, hv_ImageFiles1.TupleSelect(
                                    hv_I));
                            }
                            HOperatorSet.FindCalibObject(ho_ImageCalibration1, hv_CalibDataID, 1,
                                0, hv_I, new HTuple(), new HTuple());
                            //get_calib_data_observ_points (CalibDataID, 1, 0, I-1, Row4, Column4, Index6, Pose3)
                            ho_ContoursCali1.Dispose();
                            HOperatorSet.GetCalibDataObservContours(out ho_ContoursCali1, hv_CalibDataID,
                                "marks", 1, 0, hv_I);
                          //  GlobalStaticData.displayConvert.SetHalconScalingZoom(ho_ContoursCali1.Clone(), hWindow1);
                            hWindow2.DispObj(ho_ImageCalibration1);
                            hWindow2.SetColor("red");
                            hWindow2.DispObj(ho_ContoursCali1);
                            Thread.Sleep(1000);
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        }

                    }

                    //遍历每帧图像，从三个相机读取
                    for (hv_I = 1; (int)hv_I <= (int)((new HTuple(hv_ImageFiles2.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        //相机2
                        try
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_ImageCalibration2.Dispose();
                                HOperatorSet.ReadImage(out ho_ImageCalibration2, hv_ImageFiles2.TupleSelect(
                                    hv_I));
                            }
                            HOperatorSet.FindCalibObject(ho_ImageCalibration2, hv_CalibDataID, 2,
                                0, hv_I, new HTuple(), new HTuple());
                            //get_calib_data_observ_points (CalibDataID, 2, 0, I-1, Row5, Column5, Index7, Pose4)
                            ho_ContoursCali2.Dispose();
                            HOperatorSet.GetCalibDataObservContours(out ho_ContoursCali2, hv_CalibDataID,
                                "marks", 2, 0, hv_I);
                           // GlobalStaticData.displayConvert.SetHalconScalingZoom(ho_ContoursCali2.Clone(), hWindow1);
                            hWindow3.DispObj(ho_ImageCalibration2);
                            hWindow3.SetColor("red");
                            hWindow3.DispObj(ho_ContoursCali2);
                            Thread.Sleep(1000);
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        }

                    }
                    hv_Errors.Dispose();
                    HOperatorSet.CalibrateCameras(hv_CalibDataID, out hv_Errors);
                }
                else
                {
                    hv_Errors.Dispose();
                    hv_Errors = -9999;
                }
                ho_ImageCalibration0.Dispose();
                ho_ContoursCali0.Dispose();
                ho_ImageCalibration1.Dispose();
                ho_ContoursCali1.Dispose();
                ho_ImageCalibration2.Dispose();
                ho_ContoursCali2.Dispose();

                hv_ImageFiles0.Dispose();
                hv_ImageFiles1.Dispose();
                hv_ImageFiles2.Dispose();
                hv_len1.Dispose();
                hv_len2.Dispose();
                hv_len3.Dispose();
                hv_I.Dispose();
                hv_Exception.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_ImageCalibration0.Dispose();
                ho_ContoursCali0.Dispose();
                ho_ImageCalibration1.Dispose();
                ho_ContoursCali1.Dispose();
                ho_ImageCalibration2.Dispose();
                ho_ContoursCali2.Dispose();

                hv_ImageFiles0.Dispose();
                hv_ImageFiles1.Dispose();
                hv_ImageFiles2.Dispose();
                hv_len1.Dispose();
                hv_len2.Dispose();
                hv_len3.Dispose();
                hv_I.Dispose();
                hv_Exception.Dispose();

                //throw HDevExpDefaultException;
            }
        }

        public void Calibration_model_Init(HTuple hv_Focus, HTuple hv_Sx, HTuple hv_Sy,
        HTuple hv_WidthBack, HTuple hv_HeightBack, HTuple hv_CalibObjDescr, out HTuple hv_CalibDataID)
        {



            // Local control variables 

            HTuple hv_StartCamParam0 = new HTuple();
            // Initialize local and output iconic variables 
            hv_CalibDataID = new HTuple();
            try
            {
                //初始化一个相机模型（单个相机内参）
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_StartCamParam0.Dispose();
                    gen_cam_par_area_scan_division(hv_Focus, 0, hv_Sx, hv_Sy, hv_WidthBack / 2, hv_HeightBack / 2,
                        hv_WidthBack, hv_HeightBack, out hv_StartCamParam0);
                }
                //创建一个标定数据模型，3个相机，1个标定板
                hv_CalibDataID.Dispose();
                HOperatorSet.CreateCalibData("calibration_object", 3, 1, out hv_CalibDataID);
                //设置3个相机的初始参数
                HOperatorSet.SetCalibDataCamParam(hv_CalibDataID, "all", new HTuple(), hv_StartCamParam0);
                //设置标定板的描述文件

                HOperatorSet.SetCalibDataCalibObject(hv_CalibDataID, 0, hv_CalibObjDescr);
                //禁止标定参数sx的优化（只标定fy）
                HOperatorSet.SetCalibData(hv_CalibDataID, "camera", 0, "excluded_settings",
                    "sx");
                HOperatorSet.SetCalibData(hv_CalibDataID, "camera", 1, "excluded_settings",
                    "sx");
                HOperatorSet.SetCalibData(hv_CalibDataID, "camera", 2, "excluded_settings",
                    "sx");
                //As the two cameras are mounted rigidly and stationary and the
                //object is moved linearly in front of the cameras, only one
                //common motion vector needs to be determined.
                //设置公共运动向量：多相机共享标定板移动
                HOperatorSet.SetCalibData(hv_CalibDataID, "model", "general", "common_motion_vector",
                    "true");

                hv_StartCamParam0.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_StartCamParam0.Dispose();

                throw HDevExpDefaultException;
            }
        }
        public void Coordinate_Transformation_Result_1(HTuple hv_X, HTuple hv_Y, HTuple hv_Z,
         HTuple hv_Z_Offset, HTuple hv_PlanePose, HTuple hv_World2CamMat0, HTuple hv_CamParamData0,
         out HTuple hv_RowImage, out HTuple hv_ColImage, out HTuple hv_xM_mm, out HTuple hv_yM_mm,
         out HTuple hv_zM_mm)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Pose = new HTuple(), hv_PoseCompose = new HTuple();
            HTuple hv_PoseNewOrigin = new HTuple(), hv_WorldToBoard = new HTuple();
            HTuple hv_BoardToWorld = new HTuple(), hv_X_Corrected = new HTuple();
            HTuple hv_Y_Corrected = new HTuple(), hv_Z_Corrected = new HTuple();
            HTuple hv_X_cam0 = new HTuple(), hv_Y_cam0 = new HTuple();
            HTuple hv_Z_cam0 = new HTuple(), hv_Exception = new HTuple();
            // Initialize local and output iconic variables 
            hv_RowImage = new HTuple();
            hv_ColImage = new HTuple();
            hv_xM_mm = new HTuple();
            hv_yM_mm = new HTuple();
            hv_zM_mm = new HTuple();
            try
            {
                //生成变换矩阵，将三维点从世界系变换到标定板坐标系下
                try
                {
                    hv_Pose.Dispose();
                    HOperatorSet.CreatePose(0, 0, 0, 180, 0, 180, "Rp+T", "gba", "point", out hv_Pose);
                    hv_PoseCompose.Dispose();
                    HOperatorSet.PoseCompose(hv_PlanePose, hv_Pose, out hv_PoseCompose);
                    hv_PoseNewOrigin.Dispose();
                    HOperatorSet.SetOriginPose(hv_PoseCompose, 0, 0, hv_Z_Offset, out hv_PoseNewOrigin);
                    hv_WorldToBoard.Dispose();
                    HOperatorSet.PoseToHomMat3d(hv_PoseNewOrigin, out hv_WorldToBoard);
                    hv_BoardToWorld.Dispose();
                    HOperatorSet.HomMat3dInvert(hv_WorldToBoard, out hv_BoardToWorld);
                    //应用坐标系变换，使Z轴与桌面垂直
                    hv_X_Corrected.Dispose(); hv_Y_Corrected.Dispose(); hv_Z_Corrected.Dispose();
                    HOperatorSet.AffineTransPoint3d(hv_BoardToWorld, hv_X, hv_Y, hv_Z, out hv_X_Corrected,
                        out hv_Y_Corrected, out hv_Z_Corrected);
                    //把坐标反投影回相机
                    //get_calib_data (CalibDataID, 'camera', 0, 'pose', CamPose0)
                    //pose_to_hom_mat3d (CamPose0, World2Cam0)
                    //世界 -> 相机0坐
                    hv_X_cam0.Dispose(); hv_Y_cam0.Dispose(); hv_Z_cam0.Dispose();
                    HOperatorSet.AffineTransPoint3d(hv_World2CamMat0, hv_X, hv_Y, hv_Z, out hv_X_cam0,
                        out hv_Y_cam0, out hv_Z_cam0);
                    //get_calib_data (CalibDataID, 'camera', 0, 'params', CamParam0)
                    hv_RowImage.Dispose(); hv_ColImage.Dispose();
                    HOperatorSet.Project3dPoint(hv_X_cam0, hv_Y_cam0, hv_Z_cam0, hv_CamParamData0,
                        out hv_RowImage, out hv_ColImage);
                    //gen_cross_contour_xld (Cross2, RowImage, ColImage, 100, 0.785398)
                    //转为毫米
                    hv_xM_mm.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_xM_mm = hv_X_Corrected * 1000;
                    }
                    hv_yM_mm.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_yM_mm = hv_Y_Corrected * 1000;
                    }
                    hv_zM_mm.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_zM_mm = hv_Z_Corrected * 1000;
                    }
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                }


                hv_Pose.Dispose();
                hv_PoseCompose.Dispose();
                hv_PoseNewOrigin.Dispose();
                hv_WorldToBoard.Dispose();
                hv_BoardToWorld.Dispose();
                hv_X_Corrected.Dispose();
                hv_Y_Corrected.Dispose();
                hv_Z_Corrected.Dispose();
                hv_X_cam0.Dispose();
                hv_Y_cam0.Dispose();
                hv_Z_cam0.Dispose();
                hv_Exception.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Pose.Dispose();
                hv_PoseCompose.Dispose();
                hv_PoseNewOrigin.Dispose();
                hv_WorldToBoard.Dispose();
                hv_BoardToWorld.Dispose();
                hv_X_Corrected.Dispose();
                hv_Y_Corrected.Dispose();
                hv_Z_Corrected.Dispose();
                hv_X_cam0.Dispose();
                hv_Y_cam0.Dispose();
                hv_Z_cam0.Dispose();
                hv_Exception.Dispose();

                throw HDevExpDefaultException;
            }
        }


        public void Coordinate_Transformation_Result(HTuple hv_X, HTuple hv_Y, HTuple hv_Z,
    HTuple hv_Z_Offset, HTuple hv_Rz_Offset, HTuple hv_PlanePose, HTuple hv_World2CamMat0,
    HTuple hv_CamParamData0, out HTuple hv_RowImage, out HTuple hv_ColImage, out HTuple hv_xM_mm,
    out HTuple hv_yM_mm, out HTuple hv_zM_mm)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Pose = new HTuple(), hv_PoseCompose = new HTuple();
            HTuple hv_PoseNewOrigin = new HTuple(), hv_WorldToBoard = new HTuple();
            HTuple hv_BoardToWorld = new HTuple(), hv_X_Corrected = new HTuple();
            HTuple hv_Y_Corrected = new HTuple(), hv_Z_Corrected = new HTuple();
            HTuple hv_X_cam0 = new HTuple(), hv_Y_cam0 = new HTuple();
            HTuple hv_Z_cam0 = new HTuple(), hv_Exception = new HTuple();
            // Initialize local and output iconic variables 
            hv_RowImage = new HTuple();
            hv_ColImage = new HTuple();
            hv_xM_mm = new HTuple();
            hv_yM_mm = new HTuple();
            hv_zM_mm = new HTuple();
            try
            {
                //生成变换矩阵，将三维点从世界系变换到标定板坐标系下
                try
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Pose.Dispose();
                        HOperatorSet.CreatePose(0, 0, 0, 180, 0, 180 + hv_Rz_Offset, "Rp+T", "gba",
                            "point", out hv_Pose);
                    }
                    hv_PoseCompose.Dispose();
                    HOperatorSet.PoseCompose(hv_PlanePose, hv_Pose, out hv_PoseCompose);
                    hv_PoseNewOrigin.Dispose();
                    HOperatorSet.SetOriginPose(hv_PoseCompose, 0, 0, hv_Z_Offset, out hv_PoseNewOrigin);
                    hv_WorldToBoard.Dispose();
                    HOperatorSet.PoseToHomMat3d(hv_PoseNewOrigin, out hv_WorldToBoard);
                    hv_BoardToWorld.Dispose();
                    HOperatorSet.HomMat3dInvert(hv_WorldToBoard, out hv_BoardToWorld);
                    //应用坐标系变换，使Z轴与桌面垂直
                    hv_X_Corrected.Dispose(); hv_Y_Corrected.Dispose(); hv_Z_Corrected.Dispose();
                    HOperatorSet.AffineTransPoint3d(hv_BoardToWorld, hv_X, hv_Y, hv_Z, out hv_X_Corrected,
                        out hv_Y_Corrected, out hv_Z_Corrected);
                    //把坐标反投影回相机
                    //get_calib_data (CalibDataID, 'camera', 0, 'pose', CamPose0)
                    //pose_to_hom_mat3d (CamPose0, World2Cam0)
                    //世界 -> 相机0坐
                    hv_X_cam0.Dispose(); hv_Y_cam0.Dispose(); hv_Z_cam0.Dispose();
                    HOperatorSet.AffineTransPoint3d(hv_World2CamMat0, hv_X, hv_Y, hv_Z, out hv_X_cam0,
                        out hv_Y_cam0, out hv_Z_cam0);
                    //get_calib_data (CalibDataID, 'camera', 0, 'params', CamParam0)
                    hv_RowImage.Dispose(); hv_ColImage.Dispose();
                    HOperatorSet.Project3dPoint(hv_X_cam0, hv_Y_cam0, hv_Z_cam0, hv_CamParamData0,
                        out hv_RowImage, out hv_ColImage);
                    //gen_cross_contour_xld (Cross2, RowImage, ColImage, 100, 0.785398)
                    //转为毫米
                    hv_xM_mm.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_xM_mm = hv_X_Corrected * 1000;
                    }
                    hv_yM_mm.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_yM_mm = hv_Y_Corrected * 1000;
                    }
                    hv_zM_mm.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_zM_mm = hv_Z_Corrected * 1000;
                    }
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                }


                hv_Pose.Dispose();
                hv_PoseCompose.Dispose();
                hv_PoseNewOrigin.Dispose();
                hv_WorldToBoard.Dispose();
                hv_BoardToWorld.Dispose();
                hv_X_Corrected.Dispose();
                hv_Y_Corrected.Dispose();
                hv_Z_Corrected.Dispose();
                hv_X_cam0.Dispose();
                hv_Y_cam0.Dispose();
                hv_Z_cam0.Dispose();
                hv_Exception.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Pose.Dispose();
                hv_PoseCompose.Dispose();
                hv_PoseNewOrigin.Dispose();
                hv_WorldToBoard.Dispose();
                hv_BoardToWorld.Dispose();
                hv_X_Corrected.Dispose();
                hv_Y_Corrected.Dispose();
                hv_Z_Corrected.Dispose();
                hv_X_cam0.Dispose();
                hv_Y_cam0.Dispose();
                hv_Z_cam0.Dispose();
                hv_Exception.Dispose();

                throw HDevExpDefaultException;
            }
        }
        public void Find_coordinate_pairs1(HTuple hv_Rows0, HTuple hv_Rows1, HTuple hv_Rows2,
         HTuple hv_Cols0, HTuple hv_Cols1, HTuple hv_Cols2, HTuple hv_ProjectDiff, HTuple hv_StereoModelID,
         HTuple hv_CamParamData0, HTuple hv_CamParamData1, HTuple hv_CamParamData2, HTuple hv_InvertToCamMat0,
         HTuple hv_InvertToCamMat1, HTuple hv_InvertToCamMat2, out HTuple hv_ValidTriples,
         out HTuple hv_allRows, out HTuple hv_allCols, out HTuple hv_allCams, out HTuple hv_allIndices)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Used0 = new HTuple(), hv_Used1 = new HTuple();
            HTuple hv_Used2 = new HTuple(), hv_Index = new HTuple();
            HTuple hv_i = new HTuple(), hv_Row0 = new HTuple(), hv_Col0 = new HTuple();
            HTuple hv_j = new HTuple(), hv_Row1 = new HTuple(), hv_Col1 = new HTuple();
            HTuple hv_CovWP = new HTuple(), hv_PointIndexOut = new HTuple();
            HTuple hv_Qx_cam0 = new HTuple(), hv_Qy_cam0 = new HTuple();
            HTuple hv_Qz_cam0 = new HTuple(), hv_Qx_cam1 = new HTuple();
            HTuple hv_Qy_cam1 = new HTuple(), hv_Qz_cam1 = new HTuple();
            HTuple hv_R0c = new HTuple(), hv_C0c = new HTuple(), hv_R1c = new HTuple();
            HTuple hv_C1c = new HTuple(), hv_Err0 = new HTuple(), hv_Err1 = new HTuple();
            HTuple hv_k = new HTuple(), hv_Row2 = new HTuple(), hv_Col2 = new HTuple();
            HTuple hv_Qx02 = new HTuple(), hv_Qy02 = new HTuple();
            HTuple hv_Qz02 = new HTuple(), hv_Qx_cam2 = new HTuple();
            HTuple hv_Qy_cam2 = new HTuple(), hv_Qz_cam2 = new HTuple();
            HTuple hv_R2c = new HTuple(), hv_C2c = new HTuple(), hv_Err2 = new HTuple();
            HTuple hv_Diff02 = new HTuple(), hv_MinDiff02 = new HTuple();
            HTuple hv_BestIdx0 = new HTuple(), hv_MinDiff12 = new HTuple();
            HTuple hv_BestIdx1 = new HTuple(), hv_Qx12 = new HTuple();
            HTuple hv_Qy12 = new HTuple(), hv_Qz12 = new HTuple();
            HTuple hv_Diff12 = new HTuple();
            // Initialize local and output iconic variables 
            hv_ValidTriples = new HTuple();
            hv_allRows = new HTuple();
            hv_allCols = new HTuple();
            hv_allCams = new HTuple();
            hv_allIndices = new HTuple();
            try
            {
                //初始化点使用标记
                //Used0 := gen_tuple_const(|Rows0|, false)
                //Used1 := gen_tuple_const(|Rows1|, false)
                //Used2 := gen_tuple_const(|Rows2|, false)

                //Index := 0
                //保存三相机一致配对结果
                //ValidTriples := []
                //allRows := []
                //allCols := []
                //allCams := []
                //allIndices := []
                //遍历点匹配组合，使用两个相机重建三维点
                //用于误差检查的投影与计算 reprojection error
                //判断误差小于阈值的三相机一致点加入最终三维重建列表






                //for i := 0 to |Rows0|-1 by 1
                //*     if (Used0[i])
                //continue
                //*     endif

                //*     Row0 := Rows0[i]
                //*     Col0 := Cols0[i]

                //for j := 0 to |Rows1|-1 by 1
                //*         if (Used1[j])
                //continue
                //*         endif

                //*         Row1 := Rows1[j]
                //*         Col1 := Cols1[j]

                //*         reconstruct_points_stereo (StereoModelID, [Row0,Row1],[Col0,Col1], [], [0,1], [0,0], Qx01, Qy01, Qz01, CovWP, PointIndexOut)
                //*         affine_trans_point_3d (InvertToCamMat0, Qx01, Qy01, Qz01, Qx_cam0, Qy_cam0, Qz_cam0)
                //*         affine_trans_point_3d (InvertToCamMat1, Qx01, Qy01, Qz01, Qx_cam1, Qy_cam1, Qz_cam1)

                //project_3d_point (Qx_cam0, Qy_cam0, Qz_cam0, CamParamData0, R0c, C0c)
                //*         project_3d_point (Qx_cam1, Qy_cam1, Qz_cam1, CamParamData1, R1c, C1c)
                //它表示你用两个相机重建出一个 3D 点后，重新投影回相机0的图像，如果它正好落在你检测到的 blob 上，那就是对的；如果投影误差太大，就说明这两个点配错了或者有误差，不能用于3D重建。
                //*         Err0 := sqrt((R0c - Row0)*(R0c - Row0) + (C0c - Col0)*(C0c - Col0))
                //*         Err1 := sqrt((R1c - Row1)*(R1c - Row1) + (C1c - Col1)*(C1c - Col1))
                //*         Diff01 := Err0 + Err1

                //*         if (Diff01 < ProjectDiff)
                //for k := 0 to |Rows2|-1 by 1
                //*                 if (Used2[k])
                //continue
                //*                 endif

                //*                 Row2 := Rows2[k]
                //*                 Col2 := Cols2[k]

                //*                 reconstruct_points_stereo (StereoModelID, [Row0,Row2], [Col0,Col2], [], [0,2], [0,0], Qx02, Qy02, Qz02, CovWP, PointIndexOut)
                //*                 affine_trans_point_3d (InvertToCamMat0, Qx02, Qy02, Qz02, Qx_cam0, Qy_cam0, Qz_cam0)
                //*                 affine_trans_point_3d (InvertToCamMat2, Qx02, Qy02, Qz02, Qx_cam2, Qy_cam2, Qz_cam2)

                //*                 project_3d_point (Qx_cam0, Qy_cam0, Qz_cam0, CamParamData0, R0c, C0c)
                //*                 project_3d_point (Qx_cam2, Qy_cam2, Qz_cam2, CamParamData2, R2c, C2c)

                //*                 Err0 := sqrt((R0c - Row0)*(R0c - Row0) + (C0c - Col0)*(C0c - Col0))
                //*                 Err2 := sqrt((R2c - Row2)*(R2c - Row2) + (C2c - Col2)*(C2c - Col2))
                //*                 Diff02 := Err0 + Err2

                //*                 if (Diff02 < ProjectDiff)
                //找到有效三目匹配
                //*                     ValidTriples := [ValidTriples, [Row0,Col0,Row1,Col1,Row2,Col2]]

                //*                     allRows := [allRows, Row0, Row1, Row2]
                //*                     allCols := [allCols, Col0, Col1, Col2]
                //allCams := [allCams, 0, 1, 2]
                //allIndices := [allIndices, Index, Index, Index]
                //Index := Index + 1

                //标记三点为已用
                //*                     Used0[i] := true
                //*                     Used1[j] := true
                //*                     Used2[k] := true

                //break
                //找到一个配对就跳出k循环
                //*                 endif
                //endfor
                //*         endif
                //endfor
                //endfor




                //初始化使用标记
                hv_Used0.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used0 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows0.TupleLength()), 0);
                }
                hv_Used1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used1 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows1.TupleLength()), 0);
                }
                hv_Used2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used2 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows2.TupleLength()), 0);
                }

                hv_Index.Dispose();
                hv_Index = 0;
                hv_ValidTriples.Dispose();
                hv_ValidTriples = new HTuple();
                hv_allRows.Dispose();
                hv_allRows = new HTuple();
                hv_allCols.Dispose();
                hv_allCols = new HTuple();
                hv_allCams.Dispose();
                hv_allCams = new HTuple();
                hv_allIndices.Dispose();
                hv_allIndices = new HTuple();

                //以相机2为基准循环
                for (hv_k = 0; (int)hv_k <= (int)((new HTuple(hv_Rows2.TupleLength())) - 1); hv_k = (int)hv_k + 1)
                {
                    if ((int)(hv_Used2.TupleSelect(hv_k)) != 0)
                    {
                        continue;
                    }

                    hv_Row2.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Row2 = hv_Rows2.TupleSelect(
                            hv_k);
                    }
                    hv_Col2.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Col2 = hv_Cols2.TupleSelect(
                            hv_k);
                    }

                    //=== 与相机0组合重建 ===
                    hv_MinDiff02.Dispose();
                    hv_MinDiff02 = 99999;
                    hv_BestIdx0.Dispose();
                    hv_BestIdx0 = -1;
                    for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Rows0.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                    {
                        if ((int)(hv_Used0.TupleSelect(hv_i)) != 0)
                        {
                            continue;
                        }

                        hv_Row0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row0 = hv_Rows0.TupleSelect(
                                hv_i);
                        }
                        hv_Col0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Col0 = hv_Cols0.TupleSelect(
                                hv_i);
                        }

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Qx02.Dispose(); hv_Qy02.Dispose(); hv_Qz02.Dispose(); hv_CovWP.Dispose(); hv_PointIndexOut.Dispose();
                            HOperatorSet.ReconstructPointsStereo(hv_StereoModelID, hv_Row0.TupleConcat(
                                hv_Row2), hv_Col0.TupleConcat(hv_Col2), new HTuple(), (new HTuple(0)).TupleConcat(
                                2), (new HTuple(0)).TupleConcat(0), out hv_Qx02, out hv_Qy02, out hv_Qz02,
                                out hv_CovWP, out hv_PointIndexOut);
                        }
                        hv_Qx_cam0.Dispose(); hv_Qy_cam0.Dispose(); hv_Qz_cam0.Dispose();
                        HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat0, hv_Qx02, hv_Qy02, hv_Qz02,
                            out hv_Qx_cam0, out hv_Qy_cam0, out hv_Qz_cam0);
                        hv_Qx_cam2.Dispose(); hv_Qy_cam2.Dispose(); hv_Qz_cam2.Dispose();
                        HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat2, hv_Qx02, hv_Qy02, hv_Qz02,
                            out hv_Qx_cam2, out hv_Qy_cam2, out hv_Qz_cam2);

                        hv_R0c.Dispose(); hv_C0c.Dispose();
                        HOperatorSet.Project3dPoint(hv_Qx_cam0, hv_Qy_cam0, hv_Qz_cam0, hv_CamParamData0,
                            out hv_R0c, out hv_C0c);
                        hv_R2c.Dispose(); hv_C2c.Dispose();
                        HOperatorSet.Project3dPoint(hv_Qx_cam2, hv_Qy_cam2, hv_Qz_cam2, hv_CamParamData2,
                            out hv_R2c, out hv_C2c);

                        hv_Err0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Err0 = ((((hv_R0c - hv_Row0) * (hv_R0c - hv_Row0)) + ((hv_C0c - hv_Col0) * (hv_C0c - hv_Col0)))).TupleSqrt()
                                ;
                        }
                        hv_Err2.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Err2 = ((((hv_R2c - hv_Row2) * (hv_R2c - hv_Row2)) + ((hv_C2c - hv_Col2) * (hv_C2c - hv_Col2)))).TupleSqrt()
                                ;
                        }
                        hv_Diff02.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Diff02 = hv_Err0 + hv_Err2;
                        }

                        if ((int)(new HTuple(hv_Diff02.TupleLess(hv_MinDiff02))) != 0)
                        {
                            hv_MinDiff02.Dispose();
                            hv_MinDiff02 = new HTuple(hv_Diff02);
                            hv_BestIdx0.Dispose();
                            hv_BestIdx0 = new HTuple(hv_i);
                        }
                    }

                    if ((int)(new HTuple(hv_MinDiff02.TupleGreater(hv_ProjectDiff))) != 0)
                    {
                        continue;
                    }

                    //=== 与相机1组合重建 ===
                    hv_MinDiff12.Dispose();
                    hv_MinDiff12 = 99999;
                    hv_BestIdx1.Dispose();
                    hv_BestIdx1 = -1;
                    for (hv_j = 0; (int)hv_j <= (int)((new HTuple(hv_Rows1.TupleLength())) - 1); hv_j = (int)hv_j + 1)
                    {
                        if ((int)(hv_Used1.TupleSelect(hv_j)) != 0)
                        {
                            continue;
                        }

                        hv_Row1.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row1 = hv_Rows1.TupleSelect(
                                hv_j);
                        }
                        hv_Col1.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Col1 = hv_Cols1.TupleSelect(
                                hv_j);
                        }

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Qx12.Dispose(); hv_Qy12.Dispose(); hv_Qz12.Dispose(); hv_CovWP.Dispose(); hv_PointIndexOut.Dispose();
                            HOperatorSet.ReconstructPointsStereo(hv_StereoModelID, hv_Row1.TupleConcat(
                                hv_Row2), hv_Col1.TupleConcat(hv_Col2), new HTuple(), (new HTuple(1)).TupleConcat(
                                2), (new HTuple(0)).TupleConcat(0), out hv_Qx12, out hv_Qy12, out hv_Qz12,
                                out hv_CovWP, out hv_PointIndexOut);
                        }
                        hv_Qx_cam1.Dispose(); hv_Qy_cam1.Dispose(); hv_Qz_cam1.Dispose();
                        HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat1, hv_Qx12, hv_Qy12, hv_Qz12,
                            out hv_Qx_cam1, out hv_Qy_cam1, out hv_Qz_cam1);
                        hv_Qx_cam2.Dispose(); hv_Qy_cam2.Dispose(); hv_Qz_cam2.Dispose();
                        HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat2, hv_Qx12, hv_Qy12, hv_Qz12,
                            out hv_Qx_cam2, out hv_Qy_cam2, out hv_Qz_cam2);

                        hv_R1c.Dispose(); hv_C1c.Dispose();
                        HOperatorSet.Project3dPoint(hv_Qx_cam1, hv_Qy_cam1, hv_Qz_cam1, hv_CamParamData1,
                            out hv_R1c, out hv_C1c);
                        hv_R2c.Dispose(); hv_C2c.Dispose();
                        HOperatorSet.Project3dPoint(hv_Qx_cam2, hv_Qy_cam2, hv_Qz_cam2, hv_CamParamData2,
                            out hv_R2c, out hv_C2c);

                        hv_Err1.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Err1 = ((((hv_R1c - hv_Row1) * (hv_R1c - hv_Row1)) + ((hv_C1c - hv_Col1) * (hv_C1c - hv_Col1)))).TupleSqrt()
                                ;
                        }
                        hv_Err2.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Err2 = ((((hv_R2c - hv_Row2) * (hv_R2c - hv_Row2)) + ((hv_C2c - hv_Col2) * (hv_C2c - hv_Col2)))).TupleSqrt()
                                ;
                        }
                        hv_Diff12.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Diff12 = hv_Err1 + hv_Err2;
                        }

                        if ((int)(new HTuple(hv_Diff12.TupleLess(hv_MinDiff12))) != 0)
                        {
                            hv_MinDiff12.Dispose();
                            hv_MinDiff12 = new HTuple(hv_Diff12);
                            hv_BestIdx1.Dispose();
                            hv_BestIdx1 = new HTuple(hv_j);
                        }
                    }

                    if ((int)(new HTuple(hv_MinDiff12.TupleGreater(hv_ProjectDiff))) != 0)
                    {
                        continue;
                    }

                    //=== 三相机匹配成功 ===
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ValidTriples = hv_ValidTriples.TupleConcat(
                                ((((((((((hv_Rows0.TupleSelect(hv_BestIdx0))).TupleConcat(hv_Cols0.TupleSelect(
                                hv_BestIdx0)))).TupleConcat(hv_Rows1.TupleSelect(hv_BestIdx1)))).TupleConcat(
                                hv_Cols1.TupleSelect(hv_BestIdx1)))).TupleConcat(hv_Row2))).TupleConcat(
                                hv_Col2));
                            hv_ValidTriples.Dispose();
                            hv_ValidTriples = ExpTmpLocalVar_ValidTriples;
                        }
                    }

                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_allRows = ((((hv_allRows.TupleConcat(
                                hv_Rows0.TupleSelect(hv_BestIdx0)))).TupleConcat(hv_Rows1.TupleSelect(
                                hv_BestIdx1)))).TupleConcat(hv_Row2);
                            hv_allRows.Dispose();
                            hv_allRows = ExpTmpLocalVar_allRows;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_allCols = ((((hv_allCols.TupleConcat(
                                hv_Cols0.TupleSelect(hv_BestIdx0)))).TupleConcat(hv_Cols1.TupleSelect(
                                hv_BestIdx1)))).TupleConcat(hv_Col2);
                            hv_allCols.Dispose();
                            hv_allCols = ExpTmpLocalVar_allCols;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_allCams = hv_allCams.TupleConcat(
                                ((new HTuple(0)).TupleConcat(1)).TupleConcat(2));
                            hv_allCams.Dispose();
                            hv_allCams = ExpTmpLocalVar_allCams;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_allIndices = ((((hv_allIndices.TupleConcat(
                                hv_Index))).TupleConcat(hv_Index))).TupleConcat(hv_Index);
                            hv_allIndices.Dispose();
                            hv_allIndices = ExpTmpLocalVar_allIndices;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Index = hv_Index + 1;
                            hv_Index.Dispose();
                            hv_Index = ExpTmpLocalVar_Index;
                        }
                    }

                    if (hv_Used0 == null)
                        hv_Used0 = new HTuple();
                    hv_Used0[hv_BestIdx0] = 1;
                    if (hv_Used1 == null)
                        hv_Used1 = new HTuple();
                    hv_Used1[hv_BestIdx1] = 1;
                    if (hv_Used2 == null)
                        hv_Used2 = new HTuple();
                    hv_Used2[hv_k] = 1;
                }

                //初始化标记


                hv_Used0.Dispose();
                hv_Used1.Dispose();
                hv_Used2.Dispose();
                hv_Index.Dispose();
                hv_i.Dispose();
                hv_Row0.Dispose();
                hv_Col0.Dispose();
                hv_j.Dispose();
                hv_Row1.Dispose();
                hv_Col1.Dispose();
                hv_CovWP.Dispose();
                hv_PointIndexOut.Dispose();
                hv_Qx_cam0.Dispose();
                hv_Qy_cam0.Dispose();
                hv_Qz_cam0.Dispose();
                hv_Qx_cam1.Dispose();
                hv_Qy_cam1.Dispose();
                hv_Qz_cam1.Dispose();
                hv_R0c.Dispose();
                hv_C0c.Dispose();
                hv_R1c.Dispose();
                hv_C1c.Dispose();
                hv_Err0.Dispose();
                hv_Err1.Dispose();
                hv_k.Dispose();
                hv_Row2.Dispose();
                hv_Col2.Dispose();
                hv_Qx02.Dispose();
                hv_Qy02.Dispose();
                hv_Qz02.Dispose();
                hv_Qx_cam2.Dispose();
                hv_Qy_cam2.Dispose();
                hv_Qz_cam2.Dispose();
                hv_R2c.Dispose();
                hv_C2c.Dispose();
                hv_Err2.Dispose();
                hv_Diff02.Dispose();
                hv_MinDiff02.Dispose();
                hv_BestIdx0.Dispose();
                hv_MinDiff12.Dispose();
                hv_BestIdx1.Dispose();
                hv_Qx12.Dispose();
                hv_Qy12.Dispose();
                hv_Qz12.Dispose();
                hv_Diff12.Dispose();

                return;



            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Used0.Dispose();
                hv_Used1.Dispose();
                hv_Used2.Dispose();
                hv_Index.Dispose();
                hv_i.Dispose();
                hv_Row0.Dispose();
                hv_Col0.Dispose();
                hv_j.Dispose();
                hv_Row1.Dispose();
                hv_Col1.Dispose();
                hv_CovWP.Dispose();
                hv_PointIndexOut.Dispose();
                hv_Qx_cam0.Dispose();
                hv_Qy_cam0.Dispose();
                hv_Qz_cam0.Dispose();
                hv_Qx_cam1.Dispose();
                hv_Qy_cam1.Dispose();
                hv_Qz_cam1.Dispose();
                hv_R0c.Dispose();
                hv_C0c.Dispose();
                hv_R1c.Dispose();
                hv_C1c.Dispose();
                hv_Err0.Dispose();
                hv_Err1.Dispose();
                hv_k.Dispose();
                hv_Row2.Dispose();
                hv_Col2.Dispose();
                hv_Qx02.Dispose();
                hv_Qy02.Dispose();
                hv_Qz02.Dispose();
                hv_Qx_cam2.Dispose();
                hv_Qy_cam2.Dispose();
                hv_Qz_cam2.Dispose();
                hv_R2c.Dispose();
                hv_C2c.Dispose();
                hv_Err2.Dispose();
                hv_Diff02.Dispose();
                hv_MinDiff02.Dispose();
                hv_BestIdx0.Dispose();
                hv_MinDiff12.Dispose();
                hv_BestIdx1.Dispose();
                hv_Qx12.Dispose();
                hv_Qy12.Dispose();
                hv_Qz12.Dispose();
                hv_Diff12.Dispose();

                throw HDevExpDefaultException;
            }
        }


        public void Find_coordinate_pairs2(HTuple hv_Rows0, HTuple hv_Rows1, HTuple hv_Rows2,
       HTuple hv_Cols0, HTuple hv_Cols1, HTuple hv_Cols2, HTuple hv_ProjectDiff, HTuple hv_ZTolerance,
       HTuple hv_StereoModelID, HTuple hv_PlanePose, HTuple hv_CamParamData0, HTuple hv_CamParamData1,
       HTuple hv_CamParamData2, HTuple hv_InvertToCamMat0, HTuple hv_InvertToCamMat1,
       HTuple hv_InvertToCamMat2, out HTuple hv_ValidTriples, out HTuple hv_allRows,
       out HTuple hv_allCols, out HTuple hv_allCams, out HTuple hv_allIndices)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Used0 = new HTuple(), hv_Used1 = new HTuple();
            HTuple hv_Used2 = new HTuple(), hv_Index = new HTuple();
            HTuple hv_i = new HTuple(), hv_Row0 = new HTuple(), hv_Col0 = new HTuple();
            HTuple hv_j = new HTuple(), hv_Row1 = new HTuple(), hv_Col1 = new HTuple();
            HTuple hv_CovWP = new HTuple(), hv_PointIndexOut = new HTuple();
            HTuple hv_Qx_cam0 = new HTuple(), hv_Qy_cam0 = new HTuple();
            HTuple hv_Qz_cam0 = new HTuple(), hv_Qx_cam1 = new HTuple();
            HTuple hv_Qy_cam1 = new HTuple(), hv_Qz_cam1 = new HTuple();
            HTuple hv_R0c = new HTuple(), hv_C0c = new HTuple(), hv_R1c = new HTuple();
            HTuple hv_C1c = new HTuple(), hv_Err0 = new HTuple(), hv_Err1 = new HTuple();
            HTuple hv_k = new HTuple(), hv_Row2 = new HTuple(), hv_Col2 = new HTuple();
            HTuple hv_Qx02 = new HTuple(), hv_Qy02 = new HTuple();
            HTuple hv_Qz02 = new HTuple(), hv_Qx_cam2 = new HTuple();
            HTuple hv_Qy_cam2 = new HTuple(), hv_Qz_cam2 = new HTuple();
            HTuple hv_R2c = new HTuple(), hv_C2c = new HTuple(), hv_Err2 = new HTuple();
            HTuple hv_Diff02 = new HTuple(), hv_Qx12 = new HTuple();
            HTuple hv_Qy12 = new HTuple(), hv_Qz12 = new HTuple();
            HTuple hv_Diff12 = new HTuple(), hv_BoardToWorld = new HTuple();
            HTuple hv_WorldToBoard = new HTuple(), hv_FoundTriple = new HTuple();
            HTuple hv_Qx_cam2b = new HTuple(), hv_Qy_cam2b = new HTuple();
            HTuple hv_Qz_cam2b = new HTuple(), hv_R2c_b = new HTuple();
            HTuple hv_C2c_b = new HTuple(), hv_Err2b = new HTuple();
            HTuple hv_Qx02_b = new HTuple(), hv_Qy02_b = new HTuple();
            HTuple hv_Qz02_b = new HTuple(), hv_Qx12_b = new HTuple();
            HTuple hv_Qy12_b = new HTuple(), hv_Qz12_b = new HTuple();
            HTuple hv_ZDiff = new HTuple();
            // Initialize local and output iconic variables 
            hv_ValidTriples = new HTuple();
            hv_allRows = new HTuple();
            hv_allCols = new HTuple();
            hv_allCams = new HTuple();
            hv_allIndices = new HTuple();
            try
            {
                //初始化点使用标记
                //Used0 := gen_tuple_const(|Rows0|, false)
                //Used1 := gen_tuple_const(|Rows1|, false)
                //Used2 := gen_tuple_const(|Rows2|, false)

                //Index := 0
                //保存三相机一致配对结果
                //ValidTriples := []
                //allRows := []
                //allCols := []
                //allCams := []
                //allIndices := []
                //遍历点匹配组合，使用两个相机重建三维点
                //用于误差检查的投影与计算 reprojection error
                //判断误差小于阈值的三相机一致点加入最终三维重建列表






                //for i := 0 to |Rows0|-1 by 1
                //if (Used0[i])
                //continue
                //endif

                //Row0 := Rows0[i]
                //Col0 := Cols0[i]

                //for j := 0 to |Rows1|-1 by 1
                //if (Used1[j])
                //continue
                //endif

                //Row1 := Rows1[j]
                //Col1 := Cols1[j]

                //reconstruct_points_stereo (StereoModelID, [Row0,Row1], [Col0,Col1], [], [0,1], [0,0], Qx01, Qy01, Qz01, CovWP, PointIndexOut)
                //affine_trans_point_3d (InvertToCamMat0, Qx01, Qy01, Qz01, Qx_cam0, Qy_cam0, Qz_cam0)
                //affine_trans_point_3d (InvertToCamMat1, Qx01, Qy01, Qz01, Qx_cam1, Qy_cam1, Qz_cam1)

                //project_3d_point (Qx_cam0, Qy_cam0, Qz_cam0, CamParamData0, R0c, C0c)
                //project_3d_point (Qx_cam1, Qy_cam1, Qz_cam1, CamParamData1, R1c, C1c)
                //它表示你用两个相机重建出一个 3D 点后，重新投影回相机0的图像，如果它正好落在你检测到的 blob 上，那就是对的；如果投影误差太大，就说明这两个点配错了或者有误差，不能用于3D重建。
                //Err0 := sqrt((R0c - Row0)*(R0c - Row0) + (C0c - Col0)*(C0c - Col0))
                //Err1 := sqrt((R1c - Row1)*(R1c - Row1) + (C1c - Col1)*(C1c - Col1))
                //Diff01 := Err0 + Err1

                //if (Diff01 < ProjectDiff)
                //for k := 0 to |Rows2|-1 by 1
                //if (Used2[k])
                //continue
                //endif

                //Row2 := Rows2[k]
                //Col2 := Cols2[k]

                //reconstruct_points_stereo (StereoModelID, [Row0,Row2], [Col0,Col2], [], [0,2], [0,0], Qx02, Qy02, Qz02, CovWP, PointIndexOut)
                //affine_trans_point_3d (InvertToCamMat0, Qx02, Qy02, Qz02, Qx_cam0, Qy_cam0, Qz_cam0)
                //affine_trans_point_3d (InvertToCamMat2, Qx02, Qy02, Qz02, Qx_cam2, Qy_cam2, Qz_cam2)

                //project_3d_point (Qx_cam0, Qy_cam0, Qz_cam0, CamParamData0, R0c, C0c)
                //project_3d_point (Qx_cam2, Qy_cam2, Qz_cam2, CamParamData2, R2c, C2c)

                //Err0 := sqrt((R0c - Row0)*(R0c - Row0) + (C0c - Col0)*(C0c - Col0))
                //Err2 := sqrt((R2c - Row2)*(R2c - Row2) + (C2c - Col2)*(C2c - Col2))
                //Diff02 := Err0 + Err2

                //if (Diff02 < ProjectDiff)
                //找到有效三目匹配
                //ValidTriples := [ValidTriples, [Row0,Col0,Row1,Col1,Row2,Col2]]

                //allRows := [allRows, Row0, Row1, Row2]
                //allCols := [allCols, Col0, Col1, Col2]
                //allCams := [allCams, 0, 1, 2]
                //allIndices := [allIndices, Index, Index, Index]
                //Index := Index + 1

                //标记三点为已用
                //Used0[i] := true
                //Used1[j] := true
                //Used2[k] := true

                //break
                //找到一个配对就跳出k循环
                //endif
                //endfor
                //endif
                //endfor
                //endfor




                //初始化使用标记
                //Used0 := gen_tuple_const(|Rows0|, false)
                //Used1 := gen_tuple_const(|Rows1|, false)
                //Used2 := gen_tuple_const(|Rows2|, false)

                //Index := 0
                //ValidTriples := []
                //allRows := []
                //allCols := []
                //allCams := []
                //allIndices := []

                //以相机2为基准循环
                //for k := 0 to |Rows2|-1 by 1
                //*         if (Used2[k])
                //continue
                //*         endif

                //*         Row2 := Rows2[k]
                //*         Col2 := Cols2[k]

                //=== 与相机0组合重建 ===
                //MinDiff02 := 99999
                //BestIdx0 := -1
                //for i := 0 to |Rows0|-1 by 1
                //*             if (Used0[i])
                //continue
                //*             endif

                //*             Row0 := Rows0[i]
                //*             Col0 := Cols0[i]

                //*             reconstruct_points_stereo (StereoModelID, [Row0,Row2], [Col0,Col2], [], [0,2], [0,0], Qx02, Qy02, Qz02, CovWP, PointIndexOut)
                //*             affine_trans_point_3d (InvertToCamMat0, Qx02, Qy02, Qz02, Qx_cam0, Qy_cam0, Qz_cam0)
                //*             affine_trans_point_3d (InvertToCamMat2, Qx02, Qy02, Qz02, Qx_cam2, Qy_cam2, Qz_cam2)

                //*             project_3d_point (Qx_cam0, Qy_cam0, Qz_cam0, CamParamData0, R0c, C0c)
                //project_3d_point (Qx_cam2, Qy_cam2, Qz_cam2, CamParamData2, R2c, C2c)

                //*             Err0 := sqrt((R0c - Row0)*(R0c - Row0) + (C0c - Col0)*(C0c - Col0))
                //*             Err2 := sqrt((R2c - Row2)*(R2c - Row2) + (C2c - Col2)*(C2c - Col2))
                //*             Diff02 := Err0 + Err2

                //*             if (Diff02 < MinDiff02)
                //*                 MinDiff02 := Diff02
                //*                 BestIdx0 := i
                //*             endif
                //endfor

                //*         if (MinDiff02 > ProjectDiff)
                //continue
                //*         endif

                //=== 与相机1组合重建 ===
                //MinDiff12 := 99999
                //BestIdx1 := -1
                //for j := 0 to |Rows1|-1 by 1
                //*             if (Used1[j])
                //continue
                //*             endif

                //*             Row1 := Rows1[j]
                //*             Col1 := Cols1[j]

                //*             reconstruct_points_stereo (StereoModelID, [Row1,Row2], [Col1,Col2], [], [1,2], [0,0], Qx12, Qy12, Qz12, CovWP, PointIndexOut)
                //*             affine_trans_point_3d (InvertToCamMat1, Qx12, Qy12, Qz12, Qx_cam1, Qy_cam1, Qz_cam1)
                //*             affine_trans_point_3d (InvertToCamMat2, Qx12, Qy12, Qz12, Qx_cam2, Qy_cam2, Qz_cam2)

                //*             project_3d_point (Qx_cam1, Qy_cam1, Qz_cam1, CamParamData1, R1c, C1c)
                //*             project_3d_point (Qx_cam2, Qy_cam2, Qz_cam2, CamParamData2, R2c, C2c)

                //*             Err1 := sqrt((R1c - Row1)*(R1c - Row1) + (C1c - Col1)*(C1c - Col1))
                //*             Err2 := sqrt((R2c - Row2)*(R2c - Row2) + (C2c - Col2)*(C2c - Col2))
                //*             Diff12 := Err1 + Err2

                //*             if (Diff12 < MinDiff12)
                //*                 MinDiff12 := Diff12
                //*                 BestIdx1 := j
                //*             endif
                //endfor

                //*         if (MinDiff12 > ProjectDiff)
                //continue
                //*         endif

                //=== 三相机匹配成功 ===
                //*         ValidTriples := [ValidTriples, [Rows0[BestIdx0], Cols0[BestIdx0], Rows1[BestIdx1], Cols1[BestIdx1], Row2, Col2]]

                //*         allRows := [allRows, Rows0[BestIdx0], Rows1[BestIdx1], Row2]
                //*         allCols := [allCols, Cols0[BestIdx0], Cols1[BestIdx1], Col2]
                //allCams := [allCams, 0, 1, 2]
                //allIndices := [allIndices, Index, Index, Index]
                //Index := Index + 1

                //*         Used0[BestIdx0] := true
                //*         Used1[BestIdx1] := true
                //*         Used2[k] := true
                //endfor

                //初始化使用标志
                //---------- 参数初始化 ----------
                hv_Used0.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used0 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows0.TupleLength()), 0);
                }
                hv_Used1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used1 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows1.TupleLength()), 0);
                }
                hv_Used2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used2 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows2.TupleLength()), 0);
                }

                hv_Index.Dispose();
                hv_Index = 0;
                hv_ValidTriples.Dispose();
                hv_ValidTriples = new HTuple();
                hv_allRows.Dispose();
                hv_allRows = new HTuple();
                hv_allCols.Dispose();
                hv_allCols = new HTuple();
                hv_allCams.Dispose();
                hv_allCams = new HTuple();
                hv_allIndices.Dispose();
                hv_allIndices = new HTuple();

                //---------- 世界坐标系变换 ----------
                hv_BoardToWorld.Dispose();
                HOperatorSet.PoseToHomMat3d(hv_PlanePose, out hv_BoardToWorld);
                hv_WorldToBoard.Dispose();
                HOperatorSet.HomMat3dInvert(hv_BoardToWorld, out hv_WorldToBoard);

                //---------- 主循环：以相机2为基准 ----------
                for (hv_k = 0; (int)hv_k <= (int)((new HTuple(hv_Rows2.TupleLength())) - 1); hv_k = (int)hv_k + 1)
                {
                    if ((int)(hv_Used2.TupleSelect(hv_k)) != 0)
                    {
                        continue;
                    }

                    hv_Row2.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Row2 = hv_Rows2.TupleSelect(
                            hv_k);
                    }
                    hv_Col2.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Col2 = hv_Cols2.TupleSelect(
                            hv_k);
                    }
                    hv_FoundTriple.Dispose();
                    hv_FoundTriple = 0;

                    //---------- 尝试所有 cam0 ----------
                    for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Rows0.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                    {
                        if ((int)(hv_Used0.TupleSelect(hv_i)) != 0)
                        {
                            continue;
                        }

                        hv_Row0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row0 = hv_Rows0.TupleSelect(
                                hv_i);
                        }
                        hv_Col0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Col0 = hv_Cols0.TupleSelect(
                                hv_i);
                        }

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Qx02.Dispose(); hv_Qy02.Dispose(); hv_Qz02.Dispose(); hv_CovWP.Dispose(); hv_PointIndexOut.Dispose();
                            HOperatorSet.ReconstructPointsStereo(hv_StereoModelID, hv_Row0.TupleConcat(
                                hv_Row2), hv_Col0.TupleConcat(hv_Col2), new HTuple(), (new HTuple(0)).TupleConcat(
                                2), (new HTuple(0)).TupleConcat(0), out hv_Qx02, out hv_Qy02, out hv_Qz02,
                                out hv_CovWP, out hv_PointIndexOut);
                        }
                        hv_Qx_cam0.Dispose(); hv_Qy_cam0.Dispose(); hv_Qz_cam0.Dispose();
                        HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat0, hv_Qx02, hv_Qy02, hv_Qz02,
                            out hv_Qx_cam0, out hv_Qy_cam0, out hv_Qz_cam0);
                        hv_Qx_cam2.Dispose(); hv_Qy_cam2.Dispose(); hv_Qz_cam2.Dispose();
                        HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat2, hv_Qx02, hv_Qy02, hv_Qz02,
                            out hv_Qx_cam2, out hv_Qy_cam2, out hv_Qz_cam2);

                        hv_R0c.Dispose(); hv_C0c.Dispose();
                        HOperatorSet.Project3dPoint(hv_Qx_cam0, hv_Qy_cam0, hv_Qz_cam0, hv_CamParamData0,
                            out hv_R0c, out hv_C0c);
                        hv_R2c.Dispose(); hv_C2c.Dispose();
                        HOperatorSet.Project3dPoint(hv_Qx_cam2, hv_Qy_cam2, hv_Qz_cam2, hv_CamParamData2,
                            out hv_R2c, out hv_C2c);

                        hv_Err0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Err0 = ((((hv_R0c - hv_Row0) * (hv_R0c - hv_Row0)) + ((hv_C0c - hv_Col0) * (hv_C0c - hv_Col0)))).TupleSqrt()
                                ;
                        }
                        hv_Err2.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Err2 = ((((hv_R2c - hv_Row2) * (hv_R2c - hv_Row2)) + ((hv_C2c - hv_Col2) * (hv_C2c - hv_Col2)))).TupleSqrt()
                                ;
                        }
                        hv_Diff02.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Diff02 = hv_Err0 + hv_Err2;
                        }

                        if ((int)(new HTuple(hv_Diff02.TupleGreater(hv_ProjectDiff))) != 0)
                        {
                            continue;
                        }

                        //---------- Cam0组合合格后尝试Cam1 ----------
                        for (hv_j = 0; (int)hv_j <= (int)((new HTuple(hv_Rows1.TupleLength())) - 1); hv_j = (int)hv_j + 1)
                        {
                            if ((int)(hv_Used1.TupleSelect(hv_j)) != 0)
                            {
                                continue;
                            }

                            hv_Row1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Row1 = hv_Rows1.TupleSelect(
                                    hv_j);
                            }
                            hv_Col1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Col1 = hv_Cols1.TupleSelect(
                                    hv_j);
                            }

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Qx12.Dispose(); hv_Qy12.Dispose(); hv_Qz12.Dispose(); hv_CovWP.Dispose(); hv_PointIndexOut.Dispose();
                                HOperatorSet.ReconstructPointsStereo(hv_StereoModelID, hv_Row1.TupleConcat(
                                    hv_Row2), hv_Col1.TupleConcat(hv_Col2), new HTuple(), (new HTuple(1)).TupleConcat(
                                    2), (new HTuple(0)).TupleConcat(0), out hv_Qx12, out hv_Qy12, out hv_Qz12,
                                    out hv_CovWP, out hv_PointIndexOut);
                            }
                            hv_Qx_cam1.Dispose(); hv_Qy_cam1.Dispose(); hv_Qz_cam1.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat1, hv_Qx12, hv_Qy12,
                                hv_Qz12, out hv_Qx_cam1, out hv_Qy_cam1, out hv_Qz_cam1);
                            hv_Qx_cam2b.Dispose(); hv_Qy_cam2b.Dispose(); hv_Qz_cam2b.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat2, hv_Qx12, hv_Qy12,
                                hv_Qz12, out hv_Qx_cam2b, out hv_Qy_cam2b, out hv_Qz_cam2b);

                            hv_R1c.Dispose(); hv_C1c.Dispose();
                            HOperatorSet.Project3dPoint(hv_Qx_cam1, hv_Qy_cam1, hv_Qz_cam1, hv_CamParamData1,
                                out hv_R1c, out hv_C1c);
                            hv_R2c_b.Dispose(); hv_C2c_b.Dispose();
                            HOperatorSet.Project3dPoint(hv_Qx_cam2b, hv_Qy_cam2b, hv_Qz_cam2b, hv_CamParamData2,
                                out hv_R2c_b, out hv_C2c_b);

                            hv_Err1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Err1 = ((((hv_R1c - hv_Row1) * (hv_R1c - hv_Row1)) + ((hv_C1c - hv_Col1) * (hv_C1c - hv_Col1)))).TupleSqrt()
                                    ;
                            }
                            hv_Err2b.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Err2b = ((((hv_R2c_b - hv_Row2) * (hv_R2c_b - hv_Row2)) + ((hv_C2c_b - hv_Col2) * (hv_C2c_b - hv_Col2)))).TupleSqrt()
                                    ;
                            }
                            hv_Diff12.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Diff12 = hv_Err1 + hv_Err2b;
                            }

                            if ((int)(new HTuple(hv_Diff12.TupleGreater(hv_ProjectDiff))) != 0)
                            {
                                continue;
                            }

                            //---------- 转换到标定板坐标系 ----------
                            hv_Qx02_b.Dispose(); hv_Qy02_b.Dispose(); hv_Qz02_b.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_WorldToBoard, hv_Qx02, hv_Qy02, hv_Qz02,
                                out hv_Qx02_b, out hv_Qy02_b, out hv_Qz02_b);
                            hv_Qx12_b.Dispose(); hv_Qy12_b.Dispose(); hv_Qz12_b.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_WorldToBoard, hv_Qx12, hv_Qy12, hv_Qz12,
                                out hv_Qx12_b, out hv_Qy12_b, out hv_Qz12_b);

                            hv_ZDiff.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_ZDiff = ((hv_Qz02_b - hv_Qz12_b)).TupleAbs()
                                    ;
                            }

                            //---------- Z一致性检查 ----------
                            if ((int)(new HTuple(hv_ZDiff.TupleLessEqual(hv_ZTolerance))) != 0)
                            {
                                //三相机匹配成功
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_ValidTriples = hv_ValidTriples.TupleConcat(
                                            ((((((((hv_Row0.TupleConcat(hv_Col0))).TupleConcat(hv_Row1))).TupleConcat(
                                            hv_Col1))).TupleConcat(hv_Row2))).TupleConcat(hv_Col2));
                                        hv_ValidTriples.Dispose();
                                        hv_ValidTriples = ExpTmpLocalVar_ValidTriples;
                                    }
                                }

                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_allRows = ((((hv_allRows.TupleConcat(
                                            hv_Row0))).TupleConcat(hv_Row1))).TupleConcat(hv_Row2);
                                        hv_allRows.Dispose();
                                        hv_allRows = ExpTmpLocalVar_allRows;
                                    }
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_allCols = ((((hv_allCols.TupleConcat(
                                            hv_Col0))).TupleConcat(hv_Col1))).TupleConcat(hv_Col2);
                                        hv_allCols.Dispose();
                                        hv_allCols = ExpTmpLocalVar_allCols;
                                    }
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_allCams = hv_allCams.TupleConcat(
                                            ((new HTuple(0)).TupleConcat(1)).TupleConcat(2));
                                        hv_allCams.Dispose();
                                        hv_allCams = ExpTmpLocalVar_allCams;
                                    }
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_allIndices = ((((hv_allIndices.TupleConcat(
                                            hv_Index))).TupleConcat(hv_Index))).TupleConcat(hv_Index);
                                        hv_allIndices.Dispose();
                                        hv_allIndices = ExpTmpLocalVar_allIndices;
                                    }
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_Index = hv_Index + 1;
                                        hv_Index.Dispose();
                                        hv_Index = ExpTmpLocalVar_Index;
                                    }
                                }

                                if (hv_Used0 == null)
                                    hv_Used0 = new HTuple();
                                hv_Used0[hv_i] = 1;
                                if (hv_Used1 == null)
                                    hv_Used1 = new HTuple();
                                hv_Used1[hv_j] = 1;
                                if (hv_Used2 == null)
                                    hv_Used2 = new HTuple();
                                hv_Used2[hv_k] = 1;
                                hv_FoundTriple.Dispose();
                                hv_FoundTriple = 1;
                                break;
                            }
                        }

                        //若找到合格三相机匹配，跳出cam0循环
                        if ((int)(hv_FoundTriple) != 0)
                        {
                            break;
                        }

                    }
                }


                hv_Used0.Dispose();
                hv_Used1.Dispose();
                hv_Used2.Dispose();
                hv_Index.Dispose();
                hv_i.Dispose();
                hv_Row0.Dispose();
                hv_Col0.Dispose();
                hv_j.Dispose();
                hv_Row1.Dispose();
                hv_Col1.Dispose();
                hv_CovWP.Dispose();
                hv_PointIndexOut.Dispose();
                hv_Qx_cam0.Dispose();
                hv_Qy_cam0.Dispose();
                hv_Qz_cam0.Dispose();
                hv_Qx_cam1.Dispose();
                hv_Qy_cam1.Dispose();
                hv_Qz_cam1.Dispose();
                hv_R0c.Dispose();
                hv_C0c.Dispose();
                hv_R1c.Dispose();
                hv_C1c.Dispose();
                hv_Err0.Dispose();
                hv_Err1.Dispose();
                hv_k.Dispose();
                hv_Row2.Dispose();
                hv_Col2.Dispose();
                hv_Qx02.Dispose();
                hv_Qy02.Dispose();
                hv_Qz02.Dispose();
                hv_Qx_cam2.Dispose();
                hv_Qy_cam2.Dispose();
                hv_Qz_cam2.Dispose();
                hv_R2c.Dispose();
                hv_C2c.Dispose();
                hv_Err2.Dispose();
                hv_Diff02.Dispose();
                hv_Qx12.Dispose();
                hv_Qy12.Dispose();
                hv_Qz12.Dispose();
                hv_Diff12.Dispose();
                hv_BoardToWorld.Dispose();
                hv_WorldToBoard.Dispose();
                hv_FoundTriple.Dispose();
                hv_Qx_cam2b.Dispose();
                hv_Qy_cam2b.Dispose();
                hv_Qz_cam2b.Dispose();
                hv_R2c_b.Dispose();
                hv_C2c_b.Dispose();
                hv_Err2b.Dispose();
                hv_Qx02_b.Dispose();
                hv_Qy02_b.Dispose();
                hv_Qz02_b.Dispose();
                hv_Qx12_b.Dispose();
                hv_Qy12_b.Dispose();
                hv_Qz12_b.Dispose();
                hv_ZDiff.Dispose();

                return;



            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Used0.Dispose();
                hv_Used1.Dispose();
                hv_Used2.Dispose();
                hv_Index.Dispose();
                hv_i.Dispose();
                hv_Row0.Dispose();
                hv_Col0.Dispose();
                hv_j.Dispose();
                hv_Row1.Dispose();
                hv_Col1.Dispose();
                hv_CovWP.Dispose();
                hv_PointIndexOut.Dispose();
                hv_Qx_cam0.Dispose();
                hv_Qy_cam0.Dispose();
                hv_Qz_cam0.Dispose();
                hv_Qx_cam1.Dispose();
                hv_Qy_cam1.Dispose();
                hv_Qz_cam1.Dispose();
                hv_R0c.Dispose();
                hv_C0c.Dispose();
                hv_R1c.Dispose();
                hv_C1c.Dispose();
                hv_Err0.Dispose();
                hv_Err1.Dispose();
                hv_k.Dispose();
                hv_Row2.Dispose();
                hv_Col2.Dispose();
                hv_Qx02.Dispose();
                hv_Qy02.Dispose();
                hv_Qz02.Dispose();
                hv_Qx_cam2.Dispose();
                hv_Qy_cam2.Dispose();
                hv_Qz_cam2.Dispose();
                hv_R2c.Dispose();
                hv_C2c.Dispose();
                hv_Err2.Dispose();
                hv_Diff02.Dispose();
                hv_Qx12.Dispose();
                hv_Qy12.Dispose();
                hv_Qz12.Dispose();
                hv_Diff12.Dispose();
                hv_BoardToWorld.Dispose();
                hv_WorldToBoard.Dispose();
                hv_FoundTriple.Dispose();
                hv_Qx_cam2b.Dispose();
                hv_Qy_cam2b.Dispose();
                hv_Qz_cam2b.Dispose();
                hv_R2c_b.Dispose();
                hv_C2c_b.Dispose();
                hv_Err2b.Dispose();
                hv_Qx02_b.Dispose();
                hv_Qy02_b.Dispose();
                hv_Qz02_b.Dispose();
                hv_Qx12_b.Dispose();
                hv_Qy12_b.Dispose();
                hv_Qz12_b.Dispose();
                hv_ZDiff.Dispose();

               // throw HDevExpDefaultException;
            }
        }

        public void Find_coordinate_pairs3(HTuple hv_Rows0, HTuple hv_Rows1, HTuple hv_Rows2,
           HTuple hv_Cols0, HTuple hv_Cols1, HTuple hv_Cols2, HTuple hv_ProjectDiff, HTuple hv_ZTolerance,
           HTuple hv_XYTolerance, HTuple hv_StereoModelID, HTuple hv_PlanePose, HTuple hv_CamParamData0,
           HTuple hv_CamParamData1, HTuple hv_CamParamData2, HTuple hv_InvertToCamMat0,
           HTuple hv_InvertToCamMat1, HTuple hv_InvertToCamMat2, out HTuple hv_ValidTriples,
           out HTuple hv_allRows, out HTuple hv_allCols, out HTuple hv_allCams, out HTuple hv_allIndices)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Used0 = new HTuple(), hv_Used1 = new HTuple();
            HTuple hv_Used2 = new HTuple(), hv_Index = new HTuple();
            HTuple hv_i = new HTuple(), hv_Row0 = new HTuple(), hv_Col0 = new HTuple();
            HTuple hv_j = new HTuple(), hv_Row1 = new HTuple(), hv_Col1 = new HTuple();
            HTuple hv_CovWP = new HTuple(), hv_PointIndexOut = new HTuple();
            HTuple hv_Err0 = new HTuple(), hv_Err1 = new HTuple();
            HTuple hv_k = new HTuple(), hv_Row2 = new HTuple(), hv_Col2 = new HTuple();
            HTuple hv_Qx02 = new HTuple(), hv_Qy02 = new HTuple();
            HTuple hv_Qz02 = new HTuple(), hv_Err2 = new HTuple();
            HTuple hv_Qx12 = new HTuple(), hv_Qy12 = new HTuple();
            HTuple hv_Qz12 = new HTuple(), hv_BoardToWorld = new HTuple();
            HTuple hv_WorldToBoard = new HTuple(), hv_BestScore = new HTuple();
            HTuple hv_BestI = new HTuple(), hv_BestJ = new HTuple();
            HTuple hv_BestQx = new HTuple(), hv_BestQy = new HTuple();
            HTuple hv_BestQz = new HTuple(), hv_Qx02_b = new HTuple();
            HTuple hv_Qy02_b = new HTuple(), hv_Qz02_b = new HTuple();
            HTuple hv_Qx12_b = new HTuple(), hv_Qy12_b = new HTuple();
            HTuple hv_Qz12_b = new HTuple(), hv_ZDiff = new HTuple();
            HTuple hv_XYDiff = new HTuple(), hv_X0 = new HTuple();
            HTuple hv_Y0 = new HTuple(), hv_Z0 = new HTuple(), hv_X1 = new HTuple();
            HTuple hv_Y1 = new HTuple(), hv_Z1 = new HTuple(), hv_X2 = new HTuple();
            HTuple hv_Y2 = new HTuple(), hv_Z2 = new HTuple(), hv_R0p = new HTuple();
            HTuple hv_C0p = new HTuple(), hv_R1p = new HTuple(), hv_C1p = new HTuple();
            HTuple hv_R2p = new HTuple(), hv_C2p = new HTuple(), hv_Score = new HTuple();
            // Initialize local and output iconic variables 
            hv_ValidTriples = new HTuple();
            hv_allRows = new HTuple();
            hv_allCols = new HTuple();
            hv_allCams = new HTuple();
            hv_allIndices = new HTuple();
            try
            {
                //初始化点使用标记
                //Used0 := gen_tuple_const(|Rows0|, false)
                //Used1 := gen_tuple_const(|Rows1|, false)
                //Used2 := gen_tuple_const(|Rows2|, false)

                //Index := 0
                //保存三相机一致配对结果
                //ValidTriples := []
                //allRows := []
                //allCols := []
                //allCams := []
                //allIndices := []
                //遍历点匹配组合，使用两个相机重建三维点
                //用于误差检查的投影与计算 reprojection error
                //判断误差小于阈值的三相机一致点加入最终三维重建列表






                //for i := 0 to |Rows0|-1 by 1
                //if (Used0[i])
                //continue
                //endif

                //Row0 := Rows0[i]
                //Col0 := Cols0[i]

                //for j := 0 to |Rows1|-1 by 1
                //if (Used1[j])
                //continue
                //endif

                //Row1 := Rows1[j]
                //Col1 := Cols1[j]

                //reconstruct_points_stereo (StereoModelID, [Row0,Row1], [Col0,Col1], [], [0,1], [0,0], Qx01, Qy01, Qz01, CovWP, PointIndexOut)
                //affine_trans_point_3d (InvertToCamMat0, Qx01, Qy01, Qz01, Qx_cam0, Qy_cam0, Qz_cam0)
                //affine_trans_point_3d (InvertToCamMat1, Qx01, Qy01, Qz01, Qx_cam1, Qy_cam1, Qz_cam1)

                //project_3d_point (Qx_cam0, Qy_cam0, Qz_cam0, CamParamData0, R0c, C0c)
                //project_3d_point (Qx_cam1, Qy_cam1, Qz_cam1, CamParamData1, R1c, C1c)
                //它表示你用两个相机重建出一个 3D 点后，重新投影回相机0的图像，如果它正好落在你检测到的 blob 上，那就是对的；如果投影误差太大，就说明这两个点配错了或者有误差，不能用于3D重建。
                //Err0 := sqrt((R0c - Row0)*(R0c - Row0) + (C0c - Col0)*(C0c - Col0))
                //Err1 := sqrt((R1c - Row1)*(R1c - Row1) + (C1c - Col1)*(C1c - Col1))
                //Diff01 := Err0 + Err1

                //if (Diff01 < ProjectDiff)
                //for k := 0 to |Rows2|-1 by 1
                //if (Used2[k])
                //continue
                //endif

                //Row2 := Rows2[k]
                //Col2 := Cols2[k]

                //reconstruct_points_stereo (StereoModelID, [Row0,Row2], [Col0,Col2], [], [0,2], [0,0], Qx02, Qy02, Qz02, CovWP, PointIndexOut)
                //affine_trans_point_3d (InvertToCamMat0, Qx02, Qy02, Qz02, Qx_cam0, Qy_cam0, Qz_cam0)
                //affine_trans_point_3d (InvertToCamMat2, Qx02, Qy02, Qz02, Qx_cam2, Qy_cam2, Qz_cam2)

                //project_3d_point (Qx_cam0, Qy_cam0, Qz_cam0, CamParamData0, R0c, C0c)
                //project_3d_point (Qx_cam2, Qy_cam2, Qz_cam2, CamParamData2, R2c, C2c)

                //Err0 := sqrt((R0c - Row0)*(R0c - Row0) + (C0c - Col0)*(C0c - Col0))
                //Err2 := sqrt((R2c - Row2)*(R2c - Row2) + (C2c - Col2)*(C2c - Col2))
                //Diff02 := Err0 + Err2

                //if (Diff02 < ProjectDiff)
                //找到有效三目匹配
                //ValidTriples := [ValidTriples, [Row0,Col0,Row1,Col1,Row2,Col2]]

                //allRows := [allRows, Row0, Row1, Row2]
                //allCols := [allCols, Col0, Col1, Col2]
                //allCams := [allCams, 0, 1, 2]
                //allIndices := [allIndices, Index, Index, Index]
                //Index := Index + 1

                //标记三点为已用
                //Used0[i] := true
                //Used1[j] := true
                //Used2[k] := true

                //break
                //找到一个配对就跳出k循环
                //endif
                //endfor
                //endif
                //endfor
                //endfor




                //初始化使用标记
                //Used0 := gen_tuple_const(|Rows0|, false)
                //Used1 := gen_tuple_const(|Rows1|, false)
                //Used2 := gen_tuple_const(|Rows2|, false)

                //Index := 0
                //ValidTriples := []
                //allRows := []
                //allCols := []
                //allCams := []
                //allIndices := []

                //以相机2为基准循环
                //for k := 0 to |Rows2|-1 by 1
                //if (Used2[k])
                //continue
                //endif

                //Row2 := Rows2[k]
                //Col2 := Cols2[k]

                //=== 与相机0组合重建 ===
                //MinDiff02 := 99999
                //BestIdx0 := -1
                //for i := 0 to |Rows0|-1 by 1
                //if (Used0[i])
                //continue
                //endif

                //Row0 := Rows0[i]
                //Col0 := Cols0[i]

                //reconstruct_points_stereo (StereoModelID, [Row0,Row2], [Col0,Col2], [], [0,2], [0,0], Qx02, Qy02, Qz02, CovWP, PointIndexOut)
                //affine_trans_point_3d (InvertToCamMat0, Qx02, Qy02, Qz02, Qx_cam0, Qy_cam0, Qz_cam0)
                //affine_trans_point_3d (InvertToCamMat2, Qx02, Qy02, Qz02, Qx_cam2, Qy_cam2, Qz_cam2)

                //project_3d_point (Qx_cam0, Qy_cam0, Qz_cam0, CamParamData0, R0c, C0c)
                //project_3d_point (Qx_cam2, Qy_cam2, Qz_cam2, CamParamData2, R2c, C2c)

                //Err0 := sqrt((R0c - Row0)*(R0c - Row0) + (C0c - Col0)*(C0c - Col0))
                //Err2 := sqrt((R2c - Row2)*(R2c - Row2) + (C2c - Col2)*(C2c - Col2))
                //Diff02 := Err0 + Err2

                //if (Diff02 < MinDiff02)
                //MinDiff02 := Diff02
                //BestIdx0 := i
                //endif
                //endfor

                //if (MinDiff02 > ProjectDiff)
                //continue
                //endif

                //=== 与相机1组合重建 ===
                //MinDiff12 := 99999
                //BestIdx1 := -1
                //for j := 0 to |Rows1|-1 by 1
                //if (Used1[j])
                //continue
                //endif

                //Row1 := Rows1[j]
                //Col1 := Cols1[j]

                //reconstruct_points_stereo (StereoModelID, [Row1,Row2], [Col1,Col2], [], [1,2], [0,0], Qx12, Qy12, Qz12, CovWP, PointIndexOut)
                //affine_trans_point_3d (InvertToCamMat1, Qx12, Qy12, Qz12, Qx_cam1, Qy_cam1, Qz_cam1)
                //affine_trans_point_3d (InvertToCamMat2, Qx12, Qy12, Qz12, Qx_cam2, Qy_cam2, Qz_cam2)

                //project_3d_point (Qx_cam1, Qy_cam1, Qz_cam1, CamParamData1, R1c, C1c)
                //project_3d_point (Qx_cam2, Qy_cam2, Qz_cam2, CamParamData2, R2c, C2c)

                //Err1 := sqrt((R1c - Row1)*(R1c - Row1) + (C1c - Col1)*(C1c - Col1))
                //Err2 := sqrt((R2c - Row2)*(R2c - Row2) + (C2c - Col2)*(C2c - Col2))
                //Diff12 := Err1 + Err2

                //if (Diff12 < MinDiff12)
                //MinDiff12 := Diff12
                //BestIdx1 := j
                //endif
                //endfor

                //if (MinDiff12 > ProjectDiff)
                //continue
                //endif

                //=== 三相机匹配成功 ===
                //ValidTriples := [ValidTriples, [Rows0[BestIdx0], Cols0[BestIdx0], Rows1[BestIdx1], Cols1[BestIdx1], Row2, Col2]]

                //allRows := [allRows, Rows0[BestIdx0], Rows1[BestIdx1], Row2]
                //allCols := [allCols, Cols0[BestIdx0], Cols1[BestIdx1], Col2]
                //allCams := [allCams, 0, 1, 2]
                //allIndices := [allIndices, Index, Index, Index]
                //Index := Index + 1

                //Used0[BestIdx0] := true
                //Used1[BestIdx1] := true
                //Used2[k] := true
                //endfor

                //初始化使用标志
                //---------- 参数初始化 ----------
                hv_Used0.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used0 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows0.TupleLength()), 0);
                }
                hv_Used1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used1 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows1.TupleLength()), 0);
                }
                hv_Used2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used2 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows2.TupleLength()), 0);
                }

                hv_Index.Dispose();
                hv_Index = 0;
                hv_ValidTriples.Dispose();
                hv_ValidTriples = new HTuple();
                hv_allRows.Dispose();
                hv_allRows = new HTuple();
                hv_allCols.Dispose();
                hv_allCols = new HTuple();
                hv_allCams.Dispose();
                hv_allCams = new HTuple();
                hv_allIndices.Dispose();
                hv_allIndices = new HTuple();

                //---------- 世界坐标系变换 ----------
                hv_BoardToWorld.Dispose();
                HOperatorSet.PoseToHomMat3d(hv_PlanePose, out hv_BoardToWorld);
                hv_WorldToBoard.Dispose();
                HOperatorSet.HomMat3dInvert(hv_BoardToWorld, out hv_WorldToBoard);

                //---------- 主循环：以相机2为基准 ----------
                for (hv_k = 0; (int)hv_k <= (int)((new HTuple(hv_Rows2.TupleLength())) - 1); hv_k = (int)hv_k + 1)
                {
                    if ((int)(hv_Used2.TupleSelect(hv_k)) != 0)
                    {
                        continue;
                    }

                    hv_Row2.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Row2 = hv_Rows2.TupleSelect(
                            hv_k);
                    }
                    hv_Col2.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Col2 = hv_Cols2.TupleSelect(
                            hv_k);
                    }

                    //<<< 改动点 1：最优解缓存 >>>
                    hv_BestScore.Dispose();
                    hv_BestScore = 1e9;
                    hv_BestI.Dispose();
                    hv_BestI = -1;
                    hv_BestJ.Dispose();
                    hv_BestJ = -1;
                    hv_BestQx.Dispose();
                    hv_BestQx = 0;
                    hv_BestQy.Dispose();
                    hv_BestQy = 0;
                    hv_BestQz.Dispose();
                    hv_BestQz = 0;

                    //---------- 遍历 cam0 ----------
                    for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Rows0.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                    {
                        if ((int)(hv_Used0.TupleSelect(hv_i)) != 0)
                        {
                            continue;
                        }

                        hv_Row0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row0 = hv_Rows0.TupleSelect(
                                hv_i);
                        }
                        hv_Col0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Col0 = hv_Cols0.TupleSelect(
                                hv_i);
                        }

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Qx02.Dispose(); hv_Qy02.Dispose(); hv_Qz02.Dispose(); hv_CovWP.Dispose(); hv_PointIndexOut.Dispose();
                            HOperatorSet.ReconstructPointsStereo(hv_StereoModelID, hv_Row0.TupleConcat(
                                hv_Row2), hv_Col0.TupleConcat(hv_Col2), new HTuple(), (new HTuple(0)).TupleConcat(
                                2), (new HTuple(0)).TupleConcat(0), out hv_Qx02, out hv_Qy02, out hv_Qz02,
                                out hv_CovWP, out hv_PointIndexOut);
                        }

                        //---------- 遍历 cam1 ----------
                        for (hv_j = 0; (int)hv_j <= (int)((new HTuple(hv_Rows1.TupleLength())) - 1); hv_j = (int)hv_j + 1)
                        {
                            if ((int)(hv_Used1.TupleSelect(hv_j)) != 0)
                            {
                                continue;
                            }

                            hv_Row1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Row1 = hv_Rows1.TupleSelect(
                                    hv_j);
                            }
                            hv_Col1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Col1 = hv_Cols1.TupleSelect(
                                    hv_j);
                            }

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Qx12.Dispose(); hv_Qy12.Dispose(); hv_Qz12.Dispose(); hv_CovWP.Dispose(); hv_PointIndexOut.Dispose();
                                HOperatorSet.ReconstructPointsStereo(hv_StereoModelID, hv_Row1.TupleConcat(
                                    hv_Row2), hv_Col1.TupleConcat(hv_Col2), new HTuple(), (new HTuple(1)).TupleConcat(
                                    2), (new HTuple(0)).TupleConcat(0), out hv_Qx12, out hv_Qy12, out hv_Qz12,
                                    out hv_CovWP, out hv_PointIndexOut);
                            }

                            //---------- 转到标定板坐标 ----------
                            hv_Qx02_b.Dispose(); hv_Qy02_b.Dispose(); hv_Qz02_b.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_WorldToBoard, hv_Qx02, hv_Qy02, hv_Qz02,
                                out hv_Qx02_b, out hv_Qy02_b, out hv_Qz02_b);
                            hv_Qx12_b.Dispose(); hv_Qy12_b.Dispose(); hv_Qz12_b.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_WorldToBoard, hv_Qx12, hv_Qy12, hv_Qz12,
                                out hv_Qx12_b, out hv_Qy12_b, out hv_Qz12_b);

                            hv_ZDiff.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_ZDiff = ((hv_Qz02_b - hv_Qz12_b)).TupleAbs()
                                    ;
                            }
                            if ((int)(new HTuple(hv_ZDiff.TupleGreater(hv_ZTolerance))) != 0)
                            {
                                continue;
                            }

                            hv_XYDiff.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_XYDiff = ((((hv_Qx02_b - hv_Qx12_b) * (hv_Qx02_b - hv_Qx12_b)) + ((hv_Qy02_b - hv_Qy12_b) * (hv_Qy02_b - hv_Qy12_b)))).TupleSqrt()
                                    ;
                            }
                            if ((int)(new HTuple(hv_XYDiff.TupleGreater(hv_XYTolerance))) != 0)
                            {
                                continue;
                            }

                            //=====================================================
                            //<<< 改动点 2：只用 Q02，一个 3D 点投影三相机 >>>
                            //=====================================================
                            hv_X0.Dispose(); hv_Y0.Dispose(); hv_Z0.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat0, hv_Qx02, hv_Qy02,
                                hv_Qz02, out hv_X0, out hv_Y0, out hv_Z0);
                            hv_X1.Dispose(); hv_Y1.Dispose(); hv_Z1.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat1, hv_Qx02, hv_Qy02,
                                hv_Qz02, out hv_X1, out hv_Y1, out hv_Z1);
                            hv_X2.Dispose(); hv_Y2.Dispose(); hv_Z2.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat2, hv_Qx02, hv_Qy02,
                                hv_Qz02, out hv_X2, out hv_Y2, out hv_Z2);

                            hv_R0p.Dispose(); hv_C0p.Dispose();
                            HOperatorSet.Project3dPoint(hv_X0, hv_Y0, hv_Z0, hv_CamParamData0, out hv_R0p,
                                out hv_C0p);
                            hv_R1p.Dispose(); hv_C1p.Dispose();
                            HOperatorSet.Project3dPoint(hv_X1, hv_Y1, hv_Z1, hv_CamParamData1, out hv_R1p,
                                out hv_C1p);
                            hv_R2p.Dispose(); hv_C2p.Dispose();
                            HOperatorSet.Project3dPoint(hv_X2, hv_Y2, hv_Z2, hv_CamParamData2, out hv_R2p,
                                out hv_C2p);

                            hv_Err0.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Err0 = ((((hv_R0p - hv_Row0) * (hv_R0p - hv_Row0)) + ((hv_C0p - hv_Col0) * (hv_C0p - hv_Col0)))).TupleSqrt()
                                    ;
                            }
                            hv_Err1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Err1 = ((((hv_R1p - hv_Row1) * (hv_R1p - hv_Row1)) + ((hv_C1p - hv_Col1) * (hv_C1p - hv_Col1)))).TupleSqrt()
                                    ;
                            }
                            hv_Err2.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Err2 = ((((hv_R2p - hv_Row2) * (hv_R2p - hv_Row2)) + ((hv_C2p - hv_Col2) * (hv_C2p - hv_Col2)))).TupleSqrt()
                                    ;
                            }

                            if ((int)(new HTuple((((hv_Err0 + hv_Err1) + hv_Err2)).TupleGreater(hv_ProjectDiff))) != 0)
                            {
                                continue;
                            }

                            //<<< 改动点 3：评分，而不是 break >>>
                            hv_Score.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Score = (((hv_Err0 + hv_Err1) + hv_Err2) + (2.0 * hv_ZDiff)) + (1.0 * hv_XYDiff);
                            }

                            if ((int)(new HTuple(hv_Score.TupleLess(hv_BestScore))) != 0)
                            {
                                hv_BestScore.Dispose();
                                hv_BestScore = new HTuple(hv_Score);
                                hv_BestI.Dispose();
                                hv_BestI = new HTuple(hv_i);
                                hv_BestJ.Dispose();
                                hv_BestJ = new HTuple(hv_j);
                                hv_BestQx.Dispose();
                                hv_BestQx = new HTuple(hv_Qx02);
                                hv_BestQy.Dispose();
                                hv_BestQy = new HTuple(hv_Qy02);
                                hv_BestQz.Dispose();
                                hv_BestQz = new HTuple(hv_Qz02);
                            }

                        }
                    }

                    //---------- 统一提交最优解 ----------
                    if ((int)((new HTuple(hv_BestI.TupleGreaterEqual(0))).TupleAnd(new HTuple(hv_BestJ.TupleGreaterEqual(
                        0)))) != 0)
                    {
                        if (hv_Used0 == null)
                            hv_Used0 = new HTuple();
                        hv_Used0[hv_BestI] = 1;
                        if (hv_Used1 == null)
                            hv_Used1 = new HTuple();
                        hv_Used1[hv_BestJ] = 1;
                        if (hv_Used2 == null)
                            hv_Used2 = new HTuple();
                        hv_Used2[hv_k] = 1;

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_ValidTriples = hv_ValidTriples.TupleConcat(
                                    ((((((((((hv_Rows0.TupleSelect(hv_BestI))).TupleConcat(hv_Cols0.TupleSelect(
                                    hv_BestI)))).TupleConcat(hv_Rows1.TupleSelect(hv_BestJ)))).TupleConcat(
                                    hv_Cols1.TupleSelect(hv_BestJ)))).TupleConcat(hv_Row2))).TupleConcat(
                                    hv_Col2));
                                hv_ValidTriples.Dispose();
                                hv_ValidTriples = ExpTmpLocalVar_ValidTriples;
                            }
                        }

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_allRows = ((((hv_allRows.TupleConcat(
                                    hv_Rows0.TupleSelect(hv_BestI)))).TupleConcat(hv_Rows1.TupleSelect(
                                    hv_BestJ)))).TupleConcat(hv_Row2);
                                hv_allRows.Dispose();
                                hv_allRows = ExpTmpLocalVar_allRows;
                            }
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_allCols = ((((hv_allCols.TupleConcat(
                                    hv_Cols0.TupleSelect(hv_BestI)))).TupleConcat(hv_Cols1.TupleSelect(
                                    hv_BestJ)))).TupleConcat(hv_Col2);
                                hv_allCols.Dispose();
                                hv_allCols = ExpTmpLocalVar_allCols;
                            }
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_allCams = hv_allCams.TupleConcat(
                                    ((new HTuple(0)).TupleConcat(1)).TupleConcat(2));
                                hv_allCams.Dispose();
                                hv_allCams = ExpTmpLocalVar_allCams;
                            }
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_allIndices = ((((hv_allIndices.TupleConcat(
                                    hv_Index))).TupleConcat(hv_Index))).TupleConcat(hv_Index);
                                hv_allIndices.Dispose();
                                hv_allIndices = ExpTmpLocalVar_allIndices;
                            }
                        }

                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_Index = hv_Index + 1;
                                hv_Index.Dispose();
                                hv_Index = ExpTmpLocalVar_Index;
                            }
                        }
                    }

                }


                hv_Used0.Dispose();
                hv_Used1.Dispose();
                hv_Used2.Dispose();
                hv_Index.Dispose();
                hv_i.Dispose();
                hv_Row0.Dispose();
                hv_Col0.Dispose();
                hv_j.Dispose();
                hv_Row1.Dispose();
                hv_Col1.Dispose();
                hv_CovWP.Dispose();
                hv_PointIndexOut.Dispose();
                hv_Err0.Dispose();
                hv_Err1.Dispose();
                hv_k.Dispose();
                hv_Row2.Dispose();
                hv_Col2.Dispose();
                hv_Qx02.Dispose();
                hv_Qy02.Dispose();
                hv_Qz02.Dispose();
                hv_Err2.Dispose();
                hv_Qx12.Dispose();
                hv_Qy12.Dispose();
                hv_Qz12.Dispose();
                hv_BoardToWorld.Dispose();
                hv_WorldToBoard.Dispose();
                hv_BestScore.Dispose();
                hv_BestI.Dispose();
                hv_BestJ.Dispose();
                hv_BestQx.Dispose();
                hv_BestQy.Dispose();
                hv_BestQz.Dispose();
                hv_Qx02_b.Dispose();
                hv_Qy02_b.Dispose();
                hv_Qz02_b.Dispose();
                hv_Qx12_b.Dispose();
                hv_Qy12_b.Dispose();
                hv_Qz12_b.Dispose();
                hv_ZDiff.Dispose();
                hv_XYDiff.Dispose();
                hv_X0.Dispose();
                hv_Y0.Dispose();
                hv_Z0.Dispose();
                hv_X1.Dispose();
                hv_Y1.Dispose();
                hv_Z1.Dispose();
                hv_X2.Dispose();
                hv_Y2.Dispose();
                hv_Z2.Dispose();
                hv_R0p.Dispose();
                hv_C0p.Dispose();
                hv_R1p.Dispose();
                hv_C1p.Dispose();
                hv_R2p.Dispose();
                hv_C2p.Dispose();
                hv_Score.Dispose();

                return;



            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Used0.Dispose();
                hv_Used1.Dispose();
                hv_Used2.Dispose();
                hv_Index.Dispose();
                hv_i.Dispose();
                hv_Row0.Dispose();
                hv_Col0.Dispose();
                hv_j.Dispose();
                hv_Row1.Dispose();
                hv_Col1.Dispose();
                hv_CovWP.Dispose();
                hv_PointIndexOut.Dispose();
                hv_Err0.Dispose();
                hv_Err1.Dispose();
                hv_k.Dispose();
                hv_Row2.Dispose();
                hv_Col2.Dispose();
                hv_Qx02.Dispose();
                hv_Qy02.Dispose();
                hv_Qz02.Dispose();
                hv_Err2.Dispose();
                hv_Qx12.Dispose();
                hv_Qy12.Dispose();
                hv_Qz12.Dispose();
                hv_BoardToWorld.Dispose();
                hv_WorldToBoard.Dispose();
                hv_BestScore.Dispose();
                hv_BestI.Dispose();
                hv_BestJ.Dispose();
                hv_BestQx.Dispose();
                hv_BestQy.Dispose();
                hv_BestQz.Dispose();
                hv_Qx02_b.Dispose();
                hv_Qy02_b.Dispose();
                hv_Qz02_b.Dispose();
                hv_Qx12_b.Dispose();
                hv_Qy12_b.Dispose();
                hv_Qz12_b.Dispose();
                hv_ZDiff.Dispose();
                hv_XYDiff.Dispose();
                hv_X0.Dispose();
                hv_Y0.Dispose();
                hv_Z0.Dispose();
                hv_X1.Dispose();
                hv_Y1.Dispose();
                hv_Z1.Dispose();
                hv_X2.Dispose();
                hv_Y2.Dispose();
                hv_Z2.Dispose();
                hv_R0p.Dispose();
                hv_C0p.Dispose();
                hv_R1p.Dispose();
                hv_C1p.Dispose();
                hv_R2p.Dispose();
                hv_C2p.Dispose();
                hv_Score.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void Find_coordinate_pairs(HTuple hv_Rows0, HTuple hv_Rows1, HTuple hv_Rows2,
            HTuple hv_Cols0, HTuple hv_Cols1, HTuple hv_Cols2, HTuple hv_ProjectDiff, HTuple hv_ZTolerance,
            HTuple hv_XYTolerance, HTuple hv_StereoModelID, HTuple hv_PlanePose, HTuple hv_CamParamData0,
            HTuple hv_CamParamData1, HTuple hv_CamParamData2, HTuple hv_InvertToCamMat0,
            HTuple hv_InvertToCamMat1, HTuple hv_InvertToCamMat2, out HTuple hv_ValidTriples,
            out HTuple hv_allRows, out HTuple hv_allCols, out HTuple hv_allCams, out HTuple hv_allIndices)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Used0 = new HTuple(), hv_Used1 = new HTuple();
            HTuple hv_Used2 = new HTuple(), hv_Index = new HTuple();
            HTuple hv_BoardToWorld = new HTuple(), hv_WorldToBoard = new HTuple();
            HTuple hv_k = new HTuple(), hv_Row2 = new HTuple(), hv_Col2 = new HTuple();
            HTuple hv_i = new HTuple(), hv_Row0 = new HTuple(), hv_Col0 = new HTuple();
            HTuple hv_Qx02 = new HTuple(), hv_Qy02 = new HTuple();
            HTuple hv_Qz02 = new HTuple(), hv_CovWP = new HTuple();
            HTuple hv_PointIndexOut = new HTuple(), hv_j = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Col1 = new HTuple();
            HTuple hv_Qx12 = new HTuple(), hv_Qy12 = new HTuple();
            HTuple hv_Qz12 = new HTuple(), hv_Qx02_b = new HTuple();
            HTuple hv_Qy02_b = new HTuple(), hv_Qz02_b = new HTuple();
            HTuple hv_Qx12_b = new HTuple(), hv_Qy12_b = new HTuple();
            HTuple hv_Qz12_b = new HTuple(), hv_X0 = new HTuple();
            HTuple hv_Y0 = new HTuple(), hv_Z0 = new HTuple(), hv_X1 = new HTuple();
            HTuple hv_Y1 = new HTuple(), hv_Z1 = new HTuple(), hv_X2 = new HTuple();
            HTuple hv_Y2 = new HTuple(), hv_Z2 = new HTuple(), hv_R0p = new HTuple();
            HTuple hv_C0p = new HTuple(), hv_R1p = new HTuple(), hv_C1p = new HTuple();
            HTuple hv_R2p = new HTuple(), hv_C2p = new HTuple(), hv_Err0 = new HTuple();
            HTuple hv_Err1 = new HTuple(), hv_Err2 = new HTuple();
            HTuple hv_Score = new HTuple(), hv_CandidateScores = new HTuple();
            HTuple hv_CandidateI = new HTuple(), hv_CandidateJ = new HTuple();
            HTuple hv_CandidateK = new HTuple(), hv_CandidateQx = new HTuple();
            HTuple hv_CandidateQy = new HTuple(), hv_CandidateQz = new HTuple();
            HTuple hv_Qx01 = new HTuple(), hv_Qy01 = new HTuple();
            HTuple hv_Qz01 = new HTuple(), hv_Qx01_b = new HTuple();
            HTuple hv_Qy01_b = new HTuple(), hv_Qz01_b = new HTuple();
            HTuple hv_Zmean = new HTuple(), hv_PlaneDist = new HTuple();
            HTuple hv_Dist01_02 = new HTuple(), hv_Dist01_12 = new HTuple();
            HTuple hv_Dist02_12 = new HTuple(), hv_AvgQx = new HTuple();
            HTuple hv_AvgQy = new HTuple(), hv_AvgQz = new HTuple();
            HTuple hv_TotalErr = new HTuple(), hv_SortedIdx = new HTuple();
            HTuple hv_s = new HTuple(), hv_idx = new HTuple();
            // Initialize local and output iconic variables 
            hv_ValidTriples = new HTuple();
            hv_allRows = new HTuple();
            hv_allCols = new HTuple();
            hv_allCams = new HTuple();
            hv_allIndices = new HTuple();
            try
            {

                //***********************************************已经在用的版本***********************************************************
                //初始化使用标志
                //---------- 参数初始化 ----------
                //Used0 := gen_tuple_const(|Rows0|, false)
                //Used1 := gen_tuple_const(|Rows1|, false)
                //Used2 := gen_tuple_const(|Rows2|, false)

                //Index := 0
                //ValidTriples := []
                //allRows := []
                //allCols := []
                //allCams := []
                //allIndices := []

                //---------- 世界坐标系变换 ----------
                //pose_to_hom_mat3d (PlanePose, BoardToWorld)
                //hom_mat3d_invert (BoardToWorld, WorldToBoard)

                //---------- 主循环：以相机2为基准 ----------
                //for k := 0 to |Rows2|-1 by 1
                //if (Used2[k])
                //continue
                //endif

                //Row2 := Rows2[k]
                //Col2 := Cols2[k]

                //<<< 改动点 1：最优解缓存 >>>
                //BestScore := 1e9
                //BestI := -1
                //BestJ := -1
                //BestQx := 0
                //BestQy := 0
                //BestQz := 0

                //---------- 遍历 cam0 ----------
                //for i := 0 to |Rows0|-1 by 1
                //if (Used0[i])
                //continue
                //endif

                //Row0 := Rows0[i]
                //Col0 := Cols0[i]

                //reconstruct_points_stereo (StereoModelID, [Row0,Row2], [Col0,Col2], [], [0,2], [0,0], Qx02, Qy02, Qz02, CovWP, PointIndexOut)

                //---------- 遍历 cam1 ----------
                //for j := 0 to |Rows1|-1 by 1
                //if (Used1[j])
                //continue
                //endif

                //Row1 := Rows1[j]
                //Col1 := Cols1[j]

                //reconstruct_points_stereo (StereoModelID, [Row1,Row2], [Col1,Col2], [], [1,2], [0,0], Qx12, Qy12, Qz12, CovWP, PointIndexOut)

                //---------- 转到标定板坐标 ----------
                //affine_trans_point_3d (WorldToBoard, Qx02, Qy02, Qz02, Qx02_b, Qy02_b, Qz02_b)
                //affine_trans_point_3d (WorldToBoard, Qx12, Qy12, Qz12, Qx12_b, Qy12_b, Qz12_b)

                //ZDiff := abs(Qz02_b - Qz12_b)
                //if (ZDiff > ZTolerance)
                //continue
                //endif

                //XYDiff := sqrt((Qx02_b-Qx12_b)*(Qx02_b-Qx12_b) + (Qy02_b-Qy12_b)*(Qy02_b-Qy12_b))
                //if (XYDiff > XYTolerance)
                //continue
                //endif

                //=====================================================
                //<<< 改动点 2：只用 Q02，一个 3D 点投影三相机 >>>
                //=====================================================
                //affine_trans_point_3d (InvertToCamMat0, Qx02, Qy02, Qz02, X0, Y0, Z0)
                //affine_trans_point_3d (InvertToCamMat1, Qx02, Qy02, Qz02, X1, Y1, Z1)
                //affine_trans_point_3d (InvertToCamMat2, Qx02, Qy02, Qz02, X2, Y2, Z2)

                //project_3d_point (X0, Y0, Z0, CamParamData0, R0p, C0p)
                //project_3d_point (X1, Y1, Z1, CamParamData1, R1p, C1p)
                //project_3d_point (X2, Y2, Z2, CamParamData2, R2p, C2p)

                //Err0 := sqrt((R0p-Row0)*(R0p-Row0) + (C0p-Col0)*(C0p-Col0))
                //Err1 := sqrt((R1p-Row1)*(R1p-Row1) + (C1p-Col1)*(C1p-Col1))
                //Err2 := sqrt((R2p-Row2)*(R2p-Row2) + (C2p-Col2)*(C2p-Col2))

                //if (Err0+Err1+Err2 > ProjectDiff)
                //continue
                //endif

                //<<< 改动点 3：评分，而不是 break >>>
                //Score := Err0 + Err1 + Err2                        + 2.0*ZDiff                        + 1.0*XYDiff

                //if (Score < BestScore)
                //BestScore := Score
                //BestI := i
                //BestJ := j
                //BestQx := Qx02
                //BestQy := Qy02
                //BestQz := Qz02
                //endif

                //endfor
                //endfor

                //---------- 统一提交最优解 ----------
                //if (BestI >= 0 and BestJ >= 0)
                //Used0[BestI] := true
                //Used1[BestJ] := true
                //Used2[k] := true

                //ValidTriples := [ValidTriples, [Rows0[BestI], Cols0[BestI],  Rows1[BestJ], Cols1[BestJ], Row2, Col2]]

                //allRows := [allRows, Rows0[BestI], Rows1[BestJ], Row2]
                //allCols := [allCols, Cols0[BestI], Cols1[BestJ], Col2]
                //allCams := [allCams, 0, 1, 2]
                //allIndices := [allIndices, Index, Index, Index]

                //Index := Index + 1
                //endif

                //endfor
                //****************************************************************************************************************************************
                //=========================================================
                //初始化
                //=========================================================
                hv_CandidateScores.Dispose();
                hv_CandidateScores = new HTuple();
                hv_CandidateI.Dispose();
                hv_CandidateI = new HTuple();
                hv_CandidateJ.Dispose();
                hv_CandidateJ = new HTuple();
                hv_CandidateK.Dispose();
                hv_CandidateK = new HTuple();
                hv_CandidateQx.Dispose();
                hv_CandidateQx = new HTuple();
                hv_CandidateQy.Dispose();
                hv_CandidateQy = new HTuple();
                hv_CandidateQz.Dispose();
                hv_CandidateQz = new HTuple();

                //=========================================================
                //世界坐标系变换
                //=========================================================
                hv_BoardToWorld.Dispose();
                HOperatorSet.PoseToHomMat3d(hv_PlanePose, out hv_BoardToWorld);
                hv_WorldToBoard.Dispose();
                HOperatorSet.HomMat3dInvert(hv_BoardToWorld, out hv_WorldToBoard);

                //=========================================================
                //遍历所有组合（不再立即Used）
                //=========================================================
                for (hv_k = 0; (int)hv_k <= (int)((new HTuple(hv_Rows2.TupleLength())) - 1); hv_k = (int)hv_k + 1)
                {

                    hv_Row2.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Row2 = hv_Rows2.TupleSelect(
                            hv_k);
                    }
                    hv_Col2.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Col2 = hv_Cols2.TupleSelect(
                            hv_k);
                    }

                    for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Rows0.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                    {

                        hv_Row0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row0 = hv_Rows0.TupleSelect(
                                hv_i);
                        }
                        hv_Col0.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Col0 = hv_Cols0.TupleSelect(
                                hv_i);
                        }

                        //-------------------------------------------------
                        //Q02
                        //-------------------------------------------------
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Qx02.Dispose(); hv_Qy02.Dispose(); hv_Qz02.Dispose(); hv_CovWP.Dispose(); hv_PointIndexOut.Dispose();
                            HOperatorSet.ReconstructPointsStereo(hv_StereoModelID, hv_Row0.TupleConcat(
                                hv_Row2), hv_Col0.TupleConcat(hv_Col2), new HTuple(), (new HTuple(0)).TupleConcat(
                                2), (new HTuple(0)).TupleConcat(0), out hv_Qx02, out hv_Qy02, out hv_Qz02,
                                out hv_CovWP, out hv_PointIndexOut);
                        }

                        for (hv_j = 0; (int)hv_j <= (int)((new HTuple(hv_Rows1.TupleLength())) - 1); hv_j = (int)hv_j + 1)
                        {

                            hv_Row1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Row1 = hv_Rows1.TupleSelect(
                                    hv_j);
                            }
                            hv_Col1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Col1 = hv_Cols1.TupleSelect(
                                    hv_j);
                            }

                            //-------------------------------------------------
                            //Q12
                            //-------------------------------------------------
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Qx12.Dispose(); hv_Qy12.Dispose(); hv_Qz12.Dispose(); hv_CovWP.Dispose(); hv_PointIndexOut.Dispose();
                                HOperatorSet.ReconstructPointsStereo(hv_StereoModelID, hv_Row1.TupleConcat(
                                    hv_Row2), hv_Col1.TupleConcat(hv_Col2), new HTuple(), (new HTuple(1)).TupleConcat(
                                    2), (new HTuple(0)).TupleConcat(0), out hv_Qx12, out hv_Qy12, out hv_Qz12,
                                    out hv_CovWP, out hv_PointIndexOut);
                            }

                            //-------------------------------------------------
                            //Q01
                            //新增：第三组重建
                            //-------------------------------------------------
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Qx01.Dispose(); hv_Qy01.Dispose(); hv_Qz01.Dispose(); hv_CovWP.Dispose(); hv_PointIndexOut.Dispose();
                                HOperatorSet.ReconstructPointsStereo(hv_StereoModelID, hv_Row0.TupleConcat(
                                    hv_Row1), hv_Col0.TupleConcat(hv_Col1), new HTuple(), (new HTuple(0)).TupleConcat(
                                    1), (new HTuple(0)).TupleConcat(0), out hv_Qx01, out hv_Qy01, out hv_Qz01,
                                    out hv_CovWP, out hv_PointIndexOut);
                            }

                            //=====================================================
                            //转到标定板坐标系
                            //=====================================================
                            hv_Qx02_b.Dispose(); hv_Qy02_b.Dispose(); hv_Qz02_b.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_WorldToBoard, hv_Qx02, hv_Qy02, hv_Qz02,
                                out hv_Qx02_b, out hv_Qy02_b, out hv_Qz02_b);

                            hv_Qx12_b.Dispose(); hv_Qy12_b.Dispose(); hv_Qz12_b.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_WorldToBoard, hv_Qx12, hv_Qy12, hv_Qz12,
                                out hv_Qx12_b, out hv_Qy12_b, out hv_Qz12_b);

                            hv_Qx01_b.Dispose(); hv_Qy01_b.Dispose(); hv_Qz01_b.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_WorldToBoard, hv_Qx01, hv_Qy01, hv_Qz01,
                                out hv_Qx01_b, out hv_Qy01_b, out hv_Qz01_b);

                            //=====================================================
                            //平面约束（新增）
                            //=====================================================
                            hv_Zmean.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Zmean = ((hv_Qz02_b + hv_Qz12_b) + hv_Qz01_b) / 3.0;
                            }
                            hv_PlaneDist.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_PlaneDist = (((((hv_Qz02_b - hv_Zmean)).TupleAbs()
                                    ) + (((hv_Qz12_b - hv_Zmean)).TupleAbs())) + (((hv_Qz01_b - hv_Zmean)).TupleAbs()
                                    )) / 3.0;
                            }
                            if ((int)(new HTuple(hv_PlaneDist.TupleGreater(hv_ZTolerance))) != 0)
                            {
                                continue;
                            }
                            //PlaneDist := (abs(Qz02_b) + abs(Qz12_b) + abs(Qz01_b)) / 3.0
                            //PlaneTolerance

                            //if (PlaneDist > ZTolerance)
                            //continue
                            //endif

                            //=====================================================
                            //三组一致性验证（新增）
                            //=====================================================
                            hv_Dist01_02.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Dist01_02 = (((((hv_Qx01_b - hv_Qx02_b) * (hv_Qx01_b - hv_Qx02_b)) + ((hv_Qy01_b - hv_Qy02_b) * (hv_Qy01_b - hv_Qy02_b))) + ((hv_Qz01_b - hv_Qz02_b) * (hv_Qz01_b - hv_Qz02_b)))).TupleSqrt()
                                    ;
                            }

                            hv_Dist01_12.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Dist01_12 = (((((hv_Qx01_b - hv_Qx12_b) * (hv_Qx01_b - hv_Qx12_b)) + ((hv_Qy01_b - hv_Qy12_b) * (hv_Qy01_b - hv_Qy12_b))) + ((hv_Qz01_b - hv_Qz12_b) * (hv_Qz01_b - hv_Qz12_b)))).TupleSqrt()
                                    ;
                            }

                            hv_Dist02_12.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Dist02_12 = (((((hv_Qx02_b - hv_Qx12_b) * (hv_Qx02_b - hv_Qx12_b)) + ((hv_Qy02_b - hv_Qy12_b) * (hv_Qy02_b - hv_Qy12_b))) + ((hv_Qz02_b - hv_Qz12_b) * (hv_Qz02_b - hv_Qz12_b)))).TupleSqrt()
                                    ;
                            }

                            if ((int)((new HTuple((new HTuple(hv_Dist01_02.TupleGreater(hv_XYTolerance))).TupleOr(
                                new HTuple(hv_Dist01_12.TupleGreater(hv_XYTolerance))))).TupleOr(
                                new HTuple(hv_Dist02_12.TupleGreater(hv_XYTolerance)))) != 0)
                            {
                                continue;
                            }

                            //=====================================================
                            //使用平均点（更稳定）
                            //=====================================================
                            hv_AvgQx.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_AvgQx = ((hv_Qx01 + hv_Qx02) + hv_Qx12) / 3.0;
                            }
                            hv_AvgQy.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_AvgQy = ((hv_Qy01 + hv_Qy02) + hv_Qy12) / 3.0;
                            }
                            hv_AvgQz.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_AvgQz = ((hv_Qz01 + hv_Qz02) + hv_Qz12) / 3.0;
                            }

                            //=====================================================
                            //三相机重投影验证
                            //=====================================================
                            hv_X0.Dispose(); hv_Y0.Dispose(); hv_Z0.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat0, hv_AvgQx, hv_AvgQy,
                                hv_AvgQz, out hv_X0, out hv_Y0, out hv_Z0);
                            hv_X1.Dispose(); hv_Y1.Dispose(); hv_Z1.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat1, hv_AvgQx, hv_AvgQy,
                                hv_AvgQz, out hv_X1, out hv_Y1, out hv_Z1);
                            hv_X2.Dispose(); hv_Y2.Dispose(); hv_Z2.Dispose();
                            HOperatorSet.AffineTransPoint3d(hv_InvertToCamMat2, hv_AvgQx, hv_AvgQy,
                                hv_AvgQz, out hv_X2, out hv_Y2, out hv_Z2);

                            hv_R0p.Dispose(); hv_C0p.Dispose();
                            HOperatorSet.Project3dPoint(hv_X0, hv_Y0, hv_Z0, hv_CamParamData0, out hv_R0p,
                                out hv_C0p);
                            hv_R1p.Dispose(); hv_C1p.Dispose();
                            HOperatorSet.Project3dPoint(hv_X1, hv_Y1, hv_Z1, hv_CamParamData1, out hv_R1p,
                                out hv_C1p);
                            hv_R2p.Dispose(); hv_C2p.Dispose();
                            HOperatorSet.Project3dPoint(hv_X2, hv_Y2, hv_Z2, hv_CamParamData2, out hv_R2p,
                                out hv_C2p);

                            hv_Err0.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Err0 = ((((hv_R0p - hv_Row0) * (hv_R0p - hv_Row0)) + ((hv_C0p - hv_Col0) * (hv_C0p - hv_Col0)))).TupleSqrt()
                                    ;
                            }

                            hv_Err1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Err1 = ((((hv_R1p - hv_Row1) * (hv_R1p - hv_Row1)) + ((hv_C1p - hv_Col1) * (hv_C1p - hv_Col1)))).TupleSqrt()
                                    ;
                            }

                            hv_Err2.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Err2 = ((((hv_R2p - hv_Row2) * (hv_R2p - hv_Row2)) + ((hv_C2p - hv_Col2) * (hv_C2p - hv_Col2)))).TupleSqrt()
                                    ;
                            }

                            hv_TotalErr.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_TotalErr = (hv_Err0 + hv_Err1) + hv_Err2;
                            }

                            if ((int)(new HTuple(hv_TotalErr.TupleGreater(hv_ProjectDiff))) != 0)
                            {
                                continue;
                            }

                            //=====================================================
                            //最终评分
                            //=====================================================
                            hv_Score.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Score = ((((5.0 * hv_TotalErr) + (3.0 * hv_PlaneDist)) + hv_Dist01_02) + hv_Dist01_12) + hv_Dist02_12;
                            }

                            //=====================================================
                            //保存候选（不立即Used）
                            //=====================================================
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_CandidateScores = hv_CandidateScores.TupleConcat(
                                        hv_Score);
                                    hv_CandidateScores.Dispose();
                                    hv_CandidateScores = ExpTmpLocalVar_CandidateScores;
                                }
                            }

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_CandidateI = hv_CandidateI.TupleConcat(
                                        hv_i);
                                    hv_CandidateI.Dispose();
                                    hv_CandidateI = ExpTmpLocalVar_CandidateI;
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_CandidateJ = hv_CandidateJ.TupleConcat(
                                        hv_j);
                                    hv_CandidateJ.Dispose();
                                    hv_CandidateJ = ExpTmpLocalVar_CandidateJ;
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_CandidateK = hv_CandidateK.TupleConcat(
                                        hv_k);
                                    hv_CandidateK.Dispose();
                                    hv_CandidateK = ExpTmpLocalVar_CandidateK;
                                }
                            }

                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_CandidateQx = hv_CandidateQx.TupleConcat(
                                        hv_AvgQx);
                                    hv_CandidateQx.Dispose();
                                    hv_CandidateQx = ExpTmpLocalVar_CandidateQx;
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_CandidateQy = hv_CandidateQy.TupleConcat(
                                        hv_AvgQy);
                                    hv_CandidateQy.Dispose();
                                    hv_CandidateQy = ExpTmpLocalVar_CandidateQy;
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_CandidateQz = hv_CandidateQz.TupleConcat(
                                        hv_AvgQz);
                                    hv_CandidateQz.Dispose();
                                    hv_CandidateQz = ExpTmpLocalVar_CandidateQz;
                                }
                            }

                        }
                    }
                }

                //=========================================================
                //全局排序（核心优化）
                //=========================================================
                hv_SortedIdx.Dispose();
                HOperatorSet.TupleSortIndex(hv_CandidateScores, out hv_SortedIdx);

                hv_Used0.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used0 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows0.TupleLength()), 0);
                }
                hv_Used1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used1 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows1.TupleLength()), 0);
                }
                hv_Used2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Used2 = HTuple.TupleGenConst(
                        new HTuple(hv_Rows2.TupleLength()), 0);
                }

                hv_allRows.Dispose();
                hv_allRows = new HTuple();
                hv_allCols.Dispose();
                hv_allCols = new HTuple();
                hv_allCams.Dispose();
                hv_allCams = new HTuple();
                hv_allIndices.Dispose();
                hv_allIndices = new HTuple();

                hv_Index.Dispose();
                hv_Index = 0;

                for (hv_s = 0; (int)hv_s <= (int)((new HTuple(hv_SortedIdx.TupleLength())) - 1); hv_s = (int)hv_s + 1)
                {

                    hv_idx.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_idx = hv_SortedIdx.TupleSelect(
                            hv_s);
                    }

                    hv_i.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_i = hv_CandidateI.TupleSelect(
                            hv_idx);
                    }
                    hv_j.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_j = hv_CandidateJ.TupleSelect(
                            hv_idx);
                    }
                    hv_k.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_k = hv_CandidateK.TupleSelect(
                            hv_idx);
                    }

                    if ((int)((new HTuple(((hv_Used0.TupleSelect(hv_i))).TupleOr(hv_Used1.TupleSelect(
                        hv_j)))).TupleOr(hv_Used2.TupleSelect(hv_k))) != 0)
                    {
                        continue;
                    }

                    if (hv_Used0 == null)
                        hv_Used0 = new HTuple();
                    hv_Used0[hv_i] = 1;
                    if (hv_Used1 == null)
                        hv_Used1 = new HTuple();
                    hv_Used1[hv_j] = 1;
                    if (hv_Used2 == null)
                        hv_Used2 = new HTuple();
                    hv_Used2[hv_k] = 1;

                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_allRows = ((((hv_allRows.TupleConcat(
                                hv_Rows0.TupleSelect(hv_i)))).TupleConcat(hv_Rows1.TupleSelect(hv_j)))).TupleConcat(
                                hv_Rows2.TupleSelect(hv_k));
                            hv_allRows.Dispose();
                            hv_allRows = ExpTmpLocalVar_allRows;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_allCols = ((((hv_allCols.TupleConcat(
                                hv_Cols0.TupleSelect(hv_i)))).TupleConcat(hv_Cols1.TupleSelect(hv_j)))).TupleConcat(
                                hv_Cols2.TupleSelect(hv_k));
                            hv_allCols.Dispose();
                            hv_allCols = ExpTmpLocalVar_allCols;
                        }
                    }

                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_allCams = hv_allCams.TupleConcat(
                                ((new HTuple(0)).TupleConcat(1)).TupleConcat(2));
                            hv_allCams.Dispose();
                            hv_allCams = ExpTmpLocalVar_allCams;
                        }
                    }

                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_allIndices = ((((hv_allIndices.TupleConcat(
                                hv_Index))).TupleConcat(hv_Index))).TupleConcat(hv_Index);
                            hv_allIndices.Dispose();
                            hv_allIndices = ExpTmpLocalVar_allIndices;
                        }
                    }

                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Index = hv_Index + 1;
                            hv_Index.Dispose();
                            hv_Index = ExpTmpLocalVar_Index;
                        }
                    }

                }

                hv_Used0.Dispose();
                hv_Used1.Dispose();
                hv_Used2.Dispose();
                hv_Index.Dispose();
                hv_BoardToWorld.Dispose();
                hv_WorldToBoard.Dispose();
                hv_k.Dispose();
                hv_Row2.Dispose();
                hv_Col2.Dispose();
                hv_i.Dispose();
                hv_Row0.Dispose();
                hv_Col0.Dispose();
                hv_Qx02.Dispose();
                hv_Qy02.Dispose();
                hv_Qz02.Dispose();
                hv_CovWP.Dispose();
                hv_PointIndexOut.Dispose();
                hv_j.Dispose();
                hv_Row1.Dispose();
                hv_Col1.Dispose();
                hv_Qx12.Dispose();
                hv_Qy12.Dispose();
                hv_Qz12.Dispose();
                hv_Qx02_b.Dispose();
                hv_Qy02_b.Dispose();
                hv_Qz02_b.Dispose();
                hv_Qx12_b.Dispose();
                hv_Qy12_b.Dispose();
                hv_Qz12_b.Dispose();
                hv_X0.Dispose();
                hv_Y0.Dispose();
                hv_Z0.Dispose();
                hv_X1.Dispose();
                hv_Y1.Dispose();
                hv_Z1.Dispose();
                hv_X2.Dispose();
                hv_Y2.Dispose();
                hv_Z2.Dispose();
                hv_R0p.Dispose();
                hv_C0p.Dispose();
                hv_R1p.Dispose();
                hv_C1p.Dispose();
                hv_R2p.Dispose();
                hv_C2p.Dispose();
                hv_Err0.Dispose();
                hv_Err1.Dispose();
                hv_Err2.Dispose();
                hv_Score.Dispose();
                hv_CandidateScores.Dispose();
                hv_CandidateI.Dispose();
                hv_CandidateJ.Dispose();
                hv_CandidateK.Dispose();
                hv_CandidateQx.Dispose();
                hv_CandidateQy.Dispose();
                hv_CandidateQz.Dispose();
                hv_Qx01.Dispose();
                hv_Qy01.Dispose();
                hv_Qz01.Dispose();
                hv_Qx01_b.Dispose();
                hv_Qy01_b.Dispose();
                hv_Qz01_b.Dispose();
                hv_Zmean.Dispose();
                hv_PlaneDist.Dispose();
                hv_Dist01_02.Dispose();
                hv_Dist01_12.Dispose();
                hv_Dist02_12.Dispose();
                hv_AvgQx.Dispose();
                hv_AvgQy.Dispose();
                hv_AvgQz.Dispose();
                hv_TotalErr.Dispose();
                hv_SortedIdx.Dispose();
                hv_s.Dispose();
                hv_idx.Dispose();

                return;



            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Used0.Dispose();
                hv_Used1.Dispose();
                hv_Used2.Dispose();
                hv_Index.Dispose();
                hv_BoardToWorld.Dispose();
                hv_WorldToBoard.Dispose();
                hv_k.Dispose();
                hv_Row2.Dispose();
                hv_Col2.Dispose();
                hv_i.Dispose();
                hv_Row0.Dispose();
                hv_Col0.Dispose();
                hv_Qx02.Dispose();
                hv_Qy02.Dispose();
                hv_Qz02.Dispose();
                hv_CovWP.Dispose();
                hv_PointIndexOut.Dispose();
                hv_j.Dispose();
                hv_Row1.Dispose();
                hv_Col1.Dispose();
                hv_Qx12.Dispose();
                hv_Qy12.Dispose();
                hv_Qz12.Dispose();
                hv_Qx02_b.Dispose();
                hv_Qy02_b.Dispose();
                hv_Qz02_b.Dispose();
                hv_Qx12_b.Dispose();
                hv_Qy12_b.Dispose();
                hv_Qz12_b.Dispose();
                hv_X0.Dispose();
                hv_Y0.Dispose();
                hv_Z0.Dispose();
                hv_X1.Dispose();
                hv_Y1.Dispose();
                hv_Z1.Dispose();
                hv_X2.Dispose();
                hv_Y2.Dispose();
                hv_Z2.Dispose();
                hv_R0p.Dispose();
                hv_C0p.Dispose();
                hv_R1p.Dispose();
                hv_C1p.Dispose();
                hv_R2p.Dispose();
                hv_C2p.Dispose();
                hv_Err0.Dispose();
                hv_Err1.Dispose();
                hv_Err2.Dispose();
                hv_Score.Dispose();
                hv_CandidateScores.Dispose();
                hv_CandidateI.Dispose();
                hv_CandidateJ.Dispose();
                hv_CandidateK.Dispose();
                hv_CandidateQx.Dispose();
                hv_CandidateQy.Dispose();
                hv_CandidateQz.Dispose();
                hv_Qx01.Dispose();
                hv_Qy01.Dispose();
                hv_Qz01.Dispose();
                hv_Qx01_b.Dispose();
                hv_Qy01_b.Dispose();
                hv_Qz01_b.Dispose();
                hv_Zmean.Dispose();
                hv_PlaneDist.Dispose();
                hv_Dist01_02.Dispose();
                hv_Dist01_12.Dispose();
                hv_Dist02_12.Dispose();
                hv_AvgQx.Dispose();
                hv_AvgQy.Dispose();
                hv_AvgQz.Dispose();
                hv_TotalErr.Dispose();
                hv_SortedIdx.Dispose();
                hv_s.Dispose();
                hv_idx.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public bool Load_calibration_data(HTuple hv_Calibration_data, out HTuple hv_CalibDataID,
            out HTuple hv_CamParamData0, out HTuple hv_CamPose0, out HTuple hv_CamParamData1,
            out HTuple hv_CamPose1, out HTuple hv_CamParamData2, out HTuple hv_CamPose2,
            out HTuple hv_World2CamMat0, out HTuple hv_InvertToCamMat0, out HTuple hv_InvertToCamMat1,
            out HTuple hv_InvertToCamMat2, out HTuple hv_PlanePose, out HTuple hv_cameraSetupModelID,
            out HTuple hv_StereoModelID)
        {



            // Local control variables 

            HTuple hv_Error = new HTuple(), hv_World2CamMat1 = new HTuple();
            HTuple hv_World2CamMat2 = new HTuple();
            // Initialize local and output iconic variables 
            hv_CalibDataID = new HTuple();
            hv_CamParamData0 = new HTuple();
            hv_CamPose0 = new HTuple();
            hv_CamParamData1 = new HTuple();
            hv_CamPose1 = new HTuple();
            hv_CamParamData2 = new HTuple();
            hv_CamPose2 = new HTuple();
            hv_World2CamMat0 = new HTuple();
            hv_InvertToCamMat0 = new HTuple();
            hv_InvertToCamMat1 = new HTuple();
            hv_InvertToCamMat2 = new HTuple();
            hv_PlanePose = new HTuple();
            hv_cameraSetupModelID = new HTuple();
            hv_StereoModelID = new HTuple();
            try
            {
                //世界坐标系原点，Z轴垂直

                hv_CalibDataID.Dispose();
                HOperatorSet.ReadCalibData(hv_Calibration_data, out hv_CalibDataID);
                hv_Error.Dispose();
                HOperatorSet.CalibrateCameras(hv_CalibDataID, out hv_Error);
                //Get the calibration results.
                hv_CamParamData0.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 0, "params", out hv_CamParamData0);
                hv_CamPose0.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 0, "pose", out hv_CamPose0);
                hv_CamParamData1.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 1, "params", out hv_CamParamData1);
                hv_CamPose1.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 1, "pose", out hv_CamPose1);
                hv_CamParamData2.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 2, "params", out hv_CamParamData2);
                hv_CamPose2.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 2, "pose", out hv_CamPose2);

                //获取从世界坐标到相机坐标的变换矩阵
                hv_World2CamMat0.Dispose();
                HOperatorSet.PoseToHomMat3d(hv_CamPose0, out hv_World2CamMat0);
                hv_InvertToCamMat0.Dispose();
                HOperatorSet.HomMat3dInvert(hv_World2CamMat0, out hv_InvertToCamMat0);
                hv_World2CamMat1.Dispose();
                HOperatorSet.PoseToHomMat3d(hv_CamPose1, out hv_World2CamMat1);
                hv_InvertToCamMat1.Dispose();
                HOperatorSet.HomMat3dInvert(hv_World2CamMat1, out hv_InvertToCamMat1);
                hv_World2CamMat2.Dispose();
                HOperatorSet.PoseToHomMat3d(hv_CamPose2, out hv_World2CamMat2);
                hv_InvertToCamMat2.Dispose();
                HOperatorSet.HomMat3dInvert(hv_World2CamMat2, out hv_InvertToCamMat2);

                hv_cameraSetupModelID.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "model", "general", "camera_setup_model",
                    out hv_cameraSetupModelID);
                //取参考标定板姿态（一般第0块的第0帧）
                hv_PlanePose.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "calib_obj_pose", (new HTuple(0)).TupleConcat(
                    0), "pose", out hv_PlanePose);

                hv_StereoModelID.Dispose();
                HOperatorSet.CreateStereoModel(hv_cameraSetupModelID, "points_3d", new HTuple(),
                    new HTuple(), out hv_StereoModelID);

                hv_Error.Dispose();
                hv_World2CamMat1.Dispose();
                hv_World2CamMat2.Dispose();

                return true;
            }
            catch (Exception ex)
            {

                hv_Error.Dispose();
                hv_World2CamMat1.Dispose();
                hv_World2CamMat2.Dispose();
                return false;

                
            }
        }


        public bool Write_calibration_data(HTuple hv_CalibDataID, HTuple hv_Calibration_data,
            out HTuple hv_CamParamData0, out HTuple hv_CamPose0, out HTuple hv_CamParamData1,
            out HTuple hv_CamPose1, out HTuple hv_CamParamData2, out HTuple hv_CamPose2,
            out HTuple hv_World2CamMat0, out HTuple hv_InvertToCamMat0, out HTuple hv_InvertToCamMat1,
            out HTuple hv_InvertToCamMat2, out HTuple hv_PlanePose, out HTuple hv_cameraSetupModelID,
            out HTuple hv_StereoModelID)
        {



            // Local control variables 

            HTuple hv_World2CamMat1 = new HTuple(), hv_World2CamMat2 = new HTuple();
            // Initialize local and output iconic variables 
            hv_CamParamData0 = new HTuple();
            hv_CamPose0 = new HTuple();
            hv_CamParamData1 = new HTuple();
            hv_CamPose1 = new HTuple();
            hv_CamParamData2 = new HTuple();
            hv_CamPose2 = new HTuple();
            hv_World2CamMat0 = new HTuple();
            hv_InvertToCamMat0 = new HTuple();
            hv_InvertToCamMat1 = new HTuple();
            hv_InvertToCamMat2 = new HTuple();
            hv_PlanePose = new HTuple();
            hv_cameraSetupModelID = new HTuple();
            hv_StereoModelID = new HTuple();
            try
            {
                //世界坐标系原点，Z轴垂直
                HOperatorSet.WriteCalibData(hv_CalibDataID, hv_Calibration_data);
                //Get the calibration results.
                hv_CamParamData0.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 0, "params", out hv_CamParamData0);
                hv_CamPose0.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 0, "pose", out hv_CamPose0);
                hv_CamParamData1.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 1, "params", out hv_CamParamData1);
                hv_CamPose1.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 1, "pose", out hv_CamPose1);
                hv_CamParamData2.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 2, "params", out hv_CamParamData2);
                hv_CamPose2.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 2, "pose", out hv_CamPose2);

                //获取从世界坐标到相机坐标的变换矩阵
                hv_World2CamMat0.Dispose();
                HOperatorSet.PoseToHomMat3d(hv_CamPose0, out hv_World2CamMat0);
                hv_InvertToCamMat0.Dispose();
                HOperatorSet.HomMat3dInvert(hv_World2CamMat0, out hv_InvertToCamMat0);
                hv_World2CamMat1.Dispose();
                HOperatorSet.PoseToHomMat3d(hv_CamPose1, out hv_World2CamMat1);
                hv_InvertToCamMat1.Dispose();
                HOperatorSet.HomMat3dInvert(hv_World2CamMat1, out hv_InvertToCamMat1);
                hv_World2CamMat2.Dispose();
                HOperatorSet.PoseToHomMat3d(hv_CamPose2, out hv_World2CamMat2);
                hv_InvertToCamMat2.Dispose();
                HOperatorSet.HomMat3dInvert(hv_World2CamMat2, out hv_InvertToCamMat2);

                hv_cameraSetupModelID.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "model", "general", "camera_setup_model",
                    out hv_cameraSetupModelID);
                //取参考标定板姿态（一般第0块的第0帧）
                hv_PlanePose.Dispose();
                HOperatorSet.GetCalibData(hv_CalibDataID, "calib_obj_pose", (new HTuple(0)).TupleConcat(
                    0), "pose", out hv_PlanePose);

                hv_StereoModelID.Dispose();
                HOperatorSet.CreateStereoModel(hv_cameraSetupModelID, "points_3d", new HTuple(),
                    new HTuple(), out hv_StereoModelID);

                hv_World2CamMat1.Dispose();
                hv_World2CamMat2.Dispose();

                return true;
            }
            catch (HalconException HDevExpDefaultException)
            {
             
                hv_World2CamMat1.Dispose();
                hv_World2CamMat2.Dispose();

                return false;
            }
        }
    }
}
