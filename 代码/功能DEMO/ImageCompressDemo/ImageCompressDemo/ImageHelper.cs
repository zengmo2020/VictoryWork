using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageCompressDemo
{
    public static class ImageHelper
    {
        #region 类型转换
        /// <summary>
        /// Bitmap转byte[]
        /// </summary>
        /// <param name="bitmap">Bitmap</param>
        /// <returns></returns>
        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()[4];
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
            using (MemoryStream tmpStream = new MemoryStream())
            {
                bitmap.Save(tmpStream, codec, parameters);
                byte[] bytes = new byte[tmpStream.Length];
                tmpStream.Seek(0, SeekOrigin.Begin);
                tmpStream.Read(bytes, 0, (int)tmpStream.Length);
                tmpStream.Close();
                return bytes;
            }
        }

        /// <summary>
        /// byte[]转Bitmap
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Bitmap BytesToBitmap(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return new Bitmap(stream);
            }
        }

        /// <summary>
        /// byte[]转Image
        /// </summary>
        /// <param name="bytes">二进制图片流</param>
        /// <returns>Image</returns>
        public static Image BytesToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var image = Image.FromStream(ms);
                ms.Flush();
                return image;
            }
        }

        /// Image转byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(this Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                byte[] bytes = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }
        #endregion

        #region 功能

        /// <summary>
        /// 裁剪图片
        /// </summary>
        //public static Bitmap CropImage(Bitmap source, int x, int y, int cropWidth, int cropHeight)
        //{

        //}

        /// <summary>
        /// 裁剪图片
        /// </summary>
        public static byte[] CropImage(byte[] content, int x, int y, int cropWidth, int cropHeight)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                var targetWidth = cropWidth;//目标图片宽度
                var targetHeight = cropHeight;//目标图片高度

                using (Bitmap sourceBitmap = new Bitmap(stream))
                {
                    using (Bitmap newBitMap = new Bitmap(targetWidth, targetHeight))
                    {
                        newBitMap.SetResolution(sourceBitmap.HorizontalResolution, sourceBitmap.VerticalResolution);
                        using (Graphics g = Graphics.FromImage(newBitMap))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.DrawImage(sourceBitmap, new Rectangle(0, 0, newBitMap.Width, newBitMap.Height), new Rectangle(x, y, cropWidth, cropHeight), GraphicsUnit.Pixel);

                            return BitmapToBytes(newBitMap);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 图片尺寸缩放
        /// </summary>
        /// <returns></returns>
        public static Bitmap ResizeImage(Image img, int maxWidth, int maxHeight)
        {
            int srcWidth = img.Width;
            int srcHeight = img.Height;
            if (srcWidth > srcHeight) //如果宽度超过高度以宽度为准来缩放
            {
                if (srcWidth > maxWidth) //如果图片宽度超过限制
                {
                    var toImgWidth = maxWidth; //图片缩放后的宽度
                    var ratio = (float)toImgWidth / (float)srcWidth; //缩放比
                    var toImgHeight = (int)(srcHeight * ratio); //图片缩放后的高度
                    using (var newImg = new Bitmap(img, int.Parse(toImgWidth.ToString()), int.Parse(toImgHeight.ToString())))
                    {
                        return newImg;
                    }
                }
            }
            else
            {
                if (srcHeight > maxHeight)
                {
                    var toImgHeight = maxHeight;
                    var ratio = (float)toImgHeight / (float)srcHeight; //缩放比
                    var toImgWidth = (int)(srcWidth * ratio); //图片缩放后的宽度
                    using (var newImg = new Bitmap(img, toImgWidth, toImgHeight))
                    {
                        return newImg;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 图片缩放(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="srcBitmap">传入的Bitmap对象</param>
        /// <param name="destStream">缩放后的Stream对象</param>
        /// <param name="level">缩放等级，0到100，0 最差质量，100 最佳</param>
        private static void Compress(Bitmap srcBitmap, Stream destStream, long level)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            //Get an ImageCodecInfo object that represents the JPEG codec.
            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            myEncoder = Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            myEncoderParameters = new EncoderParameters(1);
            // Save the bitmap as a JPEG file with 给定的 quality level
            myEncoderParameter = new EncoderParameter(myEncoder, level);
            myEncoderParameters.Param[0] = myEncoderParameter;
            srcBitmap.Save(destStream, myImageCodecInfo, myEncoderParameters);
        }

        /// <summary>
        /// 图片缩放(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="srcBitMap">传入的Bitmap对象</param>
        /// <param name="destFile">缩放后的图片保存路径</param>
        /// <param name="level">缩放等级，0到100，0 最差质量，100 最佳</param>
        public static void Compress(Bitmap srcBitMap, string destFile, long level)
        {
            Stream s = new FileStream(destFile, FileMode.Create);
            Compress(srcBitMap, s, level);
            s.Close();
        }

        /// <summary>
        /// 编码信息
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();

            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        #endregion
    }
}
