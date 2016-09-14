using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Enum"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举常数名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TryName<T>(this T value)
        {
            Type type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            if (type == null || value == null || !type.IsEnum)
            {
                return string.Empty;
            }
            return System.Enum.GetName(type, value);
        }
    }
}
