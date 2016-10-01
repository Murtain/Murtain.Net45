using Murtain.Runtime.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Services
{
    public abstract class RequestBase : IValidate
    {
        /// <summary>
        /// 系统分配的应用程序标识
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// API版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 当前操作用户
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 获取请求签名
        /// </summary>
        /// <returns></returns>
        public string GetSign()
        {
            return Sign;
        }
        /// <summary>
        /// 获取签名参数
        /// </summary>
        /// <returns></returns>
        public virtual IDictionary<string, object> GetParametersDictionary()
        {
            string parameters = JsonConvert.SerializeObject(this);
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(parameters);

            dic.Remove("Sign");
            return dic;
        }
        /// <summary>
        /// 注册请求
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        /// <param name="client"></param>
        public virtual void SignRequest(string key, string secret)
        {
            AppKey = key;

            Timestamp = string.IsNullOrEmpty(Timestamp) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : this.Timestamp;
            Version = string.IsNullOrEmpty(Version) ? "1.0" : this.Version;

            Sign = SignHelper.SignRequest(GetParametersDictionary(), secret);
        }
        /// <summary>
        /// 获取请求服务
        /// </summary>
        /// <returns></returns>
        public abstract string GetMothod();
    }
}
