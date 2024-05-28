using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompressDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var img = System.Drawing.Image.FromFile(@"C:\Users\zsl\Desktop\ImageCompressDemo\ImageCompressDemo\bin\Debug\微信图片_20180717110637.jpg");
            var newImg = ImageHelper.ResizeImage(img, 200, 200);
            if (newImg != null)
            {
                var filePath = @"C:\Users\zsl\Desktop\ImageCompressDemo\ImageCompressDemo\bin\Debug\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                var filePath2= @"E:\Project_git\PDGGZPPlatform\src\FileManagement\PDGGZPPlatform.FileManagement\Images\基本信息\20180827\touxiang1.jpg";
                newImg.Save(filePath);  //保存压缩后的图片
            }
        }
    }
}
