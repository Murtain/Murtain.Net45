using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Object"/>.
    /// </summary>
    public static class ObjectExtensions
    {

        public static DateTime DBNull = "1900-01-01 00:00:00".TryDateTime();

        /// <summary>
        /// 转换为short
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <returns></returns>
        public static short TryShort(this object value)
        {
            return TryShort(value, short.MinValue);
        }
        /// <summary>
        /// 转换为short
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static short TryShort(this object value, short defValue)
        {
            short result = 0;
            return short.TryParse(value + "", out result) ? result : defValue;
        }
        /// <summary>
        /// 转换为short
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static short? TryShort(this object value, short? defValue)
        {
            short result = 0;
            return short.TryParse(value + "", out result) ? result : defValue;
        }
        /// <summary>
        /// 转换为Int，默认值：int.MinValue
        /// </summary>
        public static int TryInt(this object value)
        {
            return TryInt(value, int.MinValue);
        }
        /// <summary>
        /// 转换为Int
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int TryInt(this object value, int defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Int
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int? TryInt(this object value, int? defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Double，默认值：double.MinValue
        /// </summary>
        public static double TryDouble(this object value)
        {
            return TryDouble(value, double.MinValue);
        }
        /// <summary>
        /// 转换为Double
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double TryDouble(this object value, double defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Double
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double? TryDouble(this object value, double? defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Decimal，默认值：decimal.MinValue
        /// </summary>
        public static decimal TryDecimal(this object value)
        {
            return TryDecimal(value, decimal.MinValue);
        }
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static decimal TryDecimal(this object value, decimal defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static decimal? TryDecimal(this object value, decimal? defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为long，默认值：long.MinValue
        /// </summary>
        public static long TryLong(this object value)
        {
            return TryLong(value, long.MinValue);
        }
        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static long TryLong(this object value, long defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static long? TryLong(this object value, long? defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Boolean，默认值：false
        /// </summary>
        public static bool TryBool(this object value)
        {
            return TryBool(value, false);
        }
        /// <summary>
        /// 转换为Boolean
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool TryBool(this object value, bool defValue)
        {
            bool temp = false;
            return bool.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Boolean
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool? TryBool(this object value, bool? defValue)
        {
            bool temp = false;
            return bool.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换byte[]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] TryByte(this object s)
        {
            try
            {
                return Encoding.UTF8.GetBytes(s.TryString());
            }
            catch (Exception)
            {
                byte[] temp = null;
                return temp;
            }

        }
        /// <summary>
        /// 转换为Guid，默认值Guid.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid TryGuid(this object guid)
        {
            return TryGuid(guid, Guid.Empty);
        }
        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid TryGuid(this object guid, Guid defvalue)
        {
            return guid == null ? defvalue : Guid.Parse(guid.ToString());
        }
        /// <summary>
        /// 转换为String，默认值String.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TryString(this object s)
        {
            return TryString(s, "");
        }
        /// <summary>
        /// 转换为String
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TryString(this object s, string defvalue)
        {
            return s == null ? defvalue : s.ToString();
        }
        /// <summary>
        /// 转换为dynamic，主要是匿名对象
        /// </summary>
        public static dynamic TryDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            Type type = value.GetType();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor property in properties)
            {
                var val = property.GetValue(value);
                if (property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
                {
                    dynamic dval = val.TryDynamic();
                    expando.Add(property.Name, dval);
                }
                else
                {
                    expando.Add(property.Name, val);
                }
            }
            return expando as ExpandoObject;
        }
        /// <summary>
        /// 转换成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T TryEmun<T>(this string s)
        {
            return (T)(Enum.Parse(typeof(T), s));
        }
        /// <summary>
        /// 转换为DateTime，默认值：DateTimeExtension.DBNull
        /// </summary>
        public static DateTime TryDateTime(this Object strText)
        {
            return TryDateTime(strText, DBNull);
        }
        /// <summary>
        /// 格式化DateTime
        /// </summary>
        /// <param name="strText">DateTime Value</param>
        /// <returns></returns>
        public static string TryDefaultDateTime(this Object strText)
        {
            if (strText == null || ((DateTime)strText).Year == DBNull.Year)
            {
                return null;
            }
            return TryDateTime(strText, DBNull).ToString();
        }
        /// <summary>
        /// 转换为DateTime
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static DateTime TryDateTime(this Object strText, DateTime defValue)
        {
            DateTime temp = DBNull;
            return DateTime.TryParse(strText + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(obj, options);
        }
        /// <summary>
        /// Used to simplify and beautify casting an object to a type. 
        /// </summary>
        /// <typeparam name="T">Type to be casted</typeparam>
        /// <param name="obj">Object to cast</param>
        /// <returns>Casted object</returns>
        public static T As<T>(this object obj)
            where T : class
        {
            return (T)obj;
        }

        /// <summary>
        /// Converts given object to a value type using <see cref="Convert.ChangeType(object,System.TypeCode)"/> method.
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <typeparam name="T">Type of the target object</typeparam>
        /// <returns>Converted object</returns>
        public static T To<T>(this object obj)
            where T : struct
        {
            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Check if an item is in a list.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="list">List of items</param>
        /// <typeparam name="T">Type of the items</typeparam>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
