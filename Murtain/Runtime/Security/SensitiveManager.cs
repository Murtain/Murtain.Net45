using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Runtime.Security
{
    public class SensitiveManager
    {
        public static T PartialHidden<T>(T obj)
            where T : class
        {
            if (obj == null)
                return default(T);

            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string) && property.IsDefined(typeof(SensitiveAttribute), false))
                {
                    var sensitiveAttribute = property.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(SensitiveAttribute));

                    if (sensitiveAttribute != null)
                    {
                        SensitiveType type = (SensitiveType)sensitiveAttribute.ConstructorArguments[0].Value;
                        switch (type)
                        {
                            case SensitiveType.ACCOUNT:
                            case SensitiveType.BANK_ACCOUNT:
                            case SensitiveType.BANK_MAME:
                            case SensitiveType.BANK_MOBILE:
                            case SensitiveType.CERT_NAME:
                            case SensitiveType.CERT_NO:
                            case SensitiveType.MOBILE:
                            case SensitiveType.PASSWORD:
                                var value = property.GetValue(obj).ToString();
                                string temp = string.Empty;

                                if (value.Length < 4 && value.Length > 1)
                                    temp = value.Substring(0, 1) + "**";
                                if (value.Length > 4)
                                {
                                    temp = value.Substring(0, 2);
                                    for (int i = 0; i < value.Length - 4; i++)
                                    {
                                        temp += "*";
                                    }
                                    temp += value.Substring(value.Length - 2);
                                }
                                property.SetValue(obj, temp);
                                break;
                            case SensitiveType.EMAIL:
                                var v = property.GetValue(obj).ToString();
                                v = v.Substring(0, v.IndexOf('@'));

                                string tempEmail = string.Empty;

                                if (v.Length < 4 && v.Length > 1)
                                    temp = v.Substring(0, 1) + "**";
                                if (v.Length > 4)
                                {
                                    temp = v.Substring(0, 2);
                                    for (int i = 0; i < v.Length - 4; i++)
                                    {
                                        temp += "*";
                                    }
                                    temp += v.Substring(v.Length - 2);
                                }
                                property.SetValue(obj, tempEmail);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return obj;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SensitiveAttribute : Attribute
    {
        public SensitiveType SensitiveType { get; set; }
        public SensitiveAttribute(SensitiveType type)
        {
            this.SensitiveType = type;
        }
    }

    public enum SensitiveType
    {
        /// <summary>
        /// 证件姓名
        /// </summary>
        CERT_NAME,
        /// <summary>
        /// 证件号码
        /// </summary>
        CERT_NO,
        /// <summary>
        /// 手机号码
        /// </summary>
        MOBILE,
        /// <summary>
        /// 银行卡账号
        /// </summary>
        BANK_ACCOUNT,
        /// <summary>
        /// 银行卡名称
        /// </summary>
        BANK_MAME,
        /// <summary>
        /// 银行预留手机号
        /// </summary>
        BANK_MOBILE,
        /// <summary>
        /// 密码
        /// </summary>
        PASSWORD,
        /// <summary>
        /// 邮箱
        /// </summary>
        EMAIL,
        /// <summary>
        /// 账号
        /// </summary>
        ACCOUNT,
    }
}
