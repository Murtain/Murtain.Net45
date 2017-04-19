using Murtain.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.SDK
{
    public enum SystemReturnCode
    {
        [Description("服务器内部错误")]
        [HttpCorresponding(HttpStatusCode.InternalServerError)]
        INTERNAL_SERVER_ERROR = 10000,

        [Description("服务器不支持请求的函数")]
        [HttpCorresponding(HttpStatusCode.NotImplemented)]
        NOT_IMPLEMENTED = 10001,
        
        [Description("中间代理服务器在等待来自另一个代理或原始服务器的响应时已超时")]
        [HttpCorresponding(HttpStatusCode.GatewayTimeout)]
        GATEWAY_TIMEOUT = 10002,

        [Description("参数无效")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        INVALID_PARAMETERS = 10003,

    }
}
