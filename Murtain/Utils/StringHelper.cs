using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Utils
{
    public sealed partial class StringHelper
    {
        /// <summary>
        /// 生成6位随机数
        /// </summary>
        /// <returns></returns>
        public static string GenerateCaptcha()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
