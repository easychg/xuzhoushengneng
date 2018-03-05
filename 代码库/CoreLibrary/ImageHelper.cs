using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CoreLibrary
{
    public class ImageHelper
    {
        /// <summary>
        /// 图片转为base64编码
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        public static string ImgToBase64String(string filePath)
        {
            try
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(filePath);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception e)
            {
            }
            return "";
        }
        /// <summary>
        /// base64编码的文本转为图片
        /// </summary>
        /// <param name="photo">base64编码</param>
        /// <param name="filePath">保存绝对路径</param>
        /// <returns></returns>
        public static bool Base64ToImage(string photo, string filePath, string dirpath)
        {
            try
            {
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                byte[] arr = Convert.FromBase64String(photo);
                MemoryStream ms = new MemoryStream(arr);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                bmp.Save(filePath);
                ms.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
