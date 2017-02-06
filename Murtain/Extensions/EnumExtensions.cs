using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Murtain.Web.Attributes;

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

        public static string TryDescription(this Enum enumValue)
        {
            string str = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            DescriptionAttribute da = (DescriptionAttribute)objs[0];
            return da.Description;
        }

        public static HttpStatusCode TryHttpStatusCode(this Enum enumValue)
        {
            string str = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(HttpStatusAttribute), false);
            if (objs == null || objs.Length == 0)
                return HttpStatusCode.InternalServerError;
            HttpStatusAttribute httpStatus = (HttpStatusAttribute)objs[0];
            return httpStatus.HttpStatusCode;
        }
    }
}
