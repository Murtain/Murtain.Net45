using System.Collections.Generic;

namespace Murtain.Domain.Services
{
    public interface IRequest
    {
        /// <summary>
        /// 获取签名参数
        /// </summary>
        /// <returns></returns>
        IDictionary<string, object> GetParametersDictionary();
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <returns></returns>
        string GetSign();
        /// <summary>
        /// 注册请求签名
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        /// <param name="client"></param>
        void SignRequest(string key, string secret);
        /// <summary>
        /// 获取请求服务
        /// </summary>
        /// <returns></returns>
        string GetMothod();
    }
}