using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using Murtain.Exceptions;
using Murtain.Logging;
using Murtain.Web.Models;
using Murtain.Web.Exceptions;
using Murtain.SDK;
using Murtain.SDK.Models;
using System.ComponentModel;
using Murtain.SDK.Attributes;
using Newtonsoft.Json;
using Murtain.Web.ContractResolver;

namespace Murtain.Web.Attributes
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new SnakeCaseContractResolver()
        };

        public override void OnException(HttpActionExecutedContext context)
        {
            var request = context.Request.RequestUri.AbsolutePath;

            var response = new ResponseContentModel(WebApiExceptionReturnCode.INTERNAL_SERVER_ERROR, request);

            if (context.Exception is NotImplementedException)
            {
                response = new ResponseContentModel(WebApiExceptionReturnCode.NOT_IMPLEMENTED, request);
            }
            if (context.Exception is WebException)
            {
                response = new ResponseContentModel(WebApiExceptionReturnCode.GATEWAY_TIMEOUT, request);
            }
            if (context.Exception is UserFriendlyException)
            {
                var exception = context.Exception as UserFriendlyException;
                response = new ResponseContentModel(exception.Code, exception.Message, request);
            }

            context.Response = new HttpResponseMessage(response.HttpStatusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(response, serializerSettings), Encoding.UTF8, "application/json")
            };
        }
    }

    public enum WebApiExceptionReturnCode
    {

        /// <summary>
        /// 服务器上发生一般性错误
        /// </summary>
        [Description("服务器上发生一般性错误")]
        [HttpCorresponding(HttpStatusCode.InternalServerError)]
        INTERNAL_SERVER_ERROR,
        /// <summary>
        /// 服务器不支持所请求的功能
        /// </summary>
        [Description("服务器不支持所请求的功能")]
        [HttpCorresponding(HttpStatusCode.NotImplemented)]
        NOT_IMPLEMENTED,
        /// <summary>
        /// 中间代理服务器在等待来自另一个代理或原始服务器的响应时已超时
        /// </summary>
        [Description("中间代理服务器在等待来自另一个代理或原始服务器的响应时已超时")]
        [HttpCorresponding(HttpStatusCode.BadGateway)]
        GATEWAY_TIMEOUT
    }
}