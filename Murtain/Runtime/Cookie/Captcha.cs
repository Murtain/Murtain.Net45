using Murtain.Runtime.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Runtime.Cookie
{
    public class GraphicCaptchaManager
    {
        public static byte[] GetBytes(string cookieName)
        {

            var img = new Bitmap(125, 42);
            var g = Graphics.FromImage(img);
            try
            {
                var r = new Random();
                var MyColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                g.Clear(Color.White);
                for (var i = 0; i < 10; i++)
                {
                    MyColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                    img.SetPixel(r.Next(125), r.Next(42), MyColor);  //产生10个随机颜色的杂点
                }
                var str = "";    //存储产生的5位验证码
                for (var i = 1; i <= 5; i++)
                {
                    var s = GetString()[r.Next(GetString().Length)];
                    MyColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                    var myBrush = new SolidBrush(MyColor);
                    g.DrawString(s, new Font("Algerian", r.Next(20, 32), FontStyle.Bold), myBrush, 20 * (i - 1), r.Next(10));
                    str += s;
                }

                //设置cookie
                CookieManager.CreateCookie(cookieName, CryptoManager.EncryptDES(str), DateTime.MaxValue);

                for (var i = 1; i <= 3; i++)
                {
                    MyColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                    var p = new Pen(MyColor, 1);
                    g.DrawLine(p, r.Next(125), r.Next(42), r.Next(125), r.Next(42));
                    g.DrawLine(p, r.Next(170), r.Next(180), r.Next(125), r.Next(125));
                }

                //保存图片数据
                using (var stream = new MemoryStream())
                {
                    img.Save(stream, ImageFormat.Jpeg);
                    //输出图片流
                    return stream.ToArray();
                }
            }
            finally
            {
                g.Dispose();
                img.Dispose();
            }

        }

        /// <summary>
        /// 返回除字母O之外的25个字母和数字0之外的9个数字
        /// </summary>
        /// <returns></returns>
        private static string[] GetString()
        {
            string[] Arr = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            return Arr;
        }
    }
}
