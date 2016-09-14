using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Services
{
    /// <summary>
    /// API响应包装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ApiResponseWrapper<T> where T : class
    {
        /// <summary>
        /// 业务实体内容
        /// </summary>
        public T Body { get; set; }
        /// <summary>
        /// 响应状态
        /// </summary>
        public string ResponseStatus { get; set; }
        /// <summary>
        /// 消耗时间
        /// </summary>
        public string ElapseTime { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 响应内容包装
        /// </summary>
        public ApiResponseWrapper()
        {
        }
        /// <summary>
        /// 响应内容包装
        /// </summary>
        /// <param name="responseStatus"></param>
        public ApiResponseWrapper(string responseStatus)
        {
            this.ResponseStatus = responseStatus;
        }
        /// <summary>
        /// 响应内容包装
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ApiResponseWrapper<T> ActionResponse(T input, string elapse)
        {
            JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();

            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            JsonSerializerSettings.Converters.Add(timeConverter);

            return new ApiResponseWrapper<T>
            {
                Body = input,
                ElapseTime = elapse,
                ResponseStatus = HttpStatusCode.OK.ToString()
            };
        }
    }
}
