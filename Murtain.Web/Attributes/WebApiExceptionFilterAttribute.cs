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

namespace Murtain.Web.Attributes
{

    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext context)
        {
            var request = context.Request.RequestUri.AbsolutePath;

            var response = new ResponseContentModel(SystemReturnCode.INTERNAL_SERVER_ERROR, request);

            if (context.Exception is NotImplementedException)
            {
                response = new ResponseContentModel(SystemReturnCode.NOT_IMPLEMENTED, request);
            }
            if (context.Exception is WebException)
            {
                response = new ResponseContentModel(SystemReturnCode.GATEWAY_TIMEOUT, request);
            }
            if (context.Exception is UserFriendlyExceprion)
            {
                var exception = context.Exception as UserFriendlyExceprion;
                response = new ResponseContentModel(exception.Code, exception.Message, request);
            }

            context.Response = new HttpResponseMessage(response.HttpStatusCode)
            {
                Content = new StringContent(response.ToString(),Encoding.UTF8,"application/json")
            };
        }
    }
}