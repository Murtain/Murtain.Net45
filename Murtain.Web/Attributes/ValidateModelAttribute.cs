using Murtain.SDK;
using Murtain.Web.ContractResolver;
using Murtain.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Murtain.Web.Attributes
{
    public class ValidateModelAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new SnakeCaseContractResolver() };
        public override async void OnActionExecuting(HttpActionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                HttpResponseMessage response = context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.ModelState);

                var content = new ValidateModelResponseContentModel(SystemReturnCode.INVALID_PARAMETERS, context.Request.RequestUri.AbsolutePath);

                foreach (var key in context.ModelState.Keys)
                {
                    var state = context.ModelState[key];
                    if (state.Errors.Any())
                    {
                        content.ModelState.Add(key.Split('.').Length > 1 ? PropertyNameConvert.SnakeCasePropertyName(key.Split('.')[1]) : PropertyNameConvert.SnakeCasePropertyName(key), state.Errors.First().ErrorMessage);
                    }
                }
                var result = await response.Content.ReadAsStringAsync();

                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(content.ToString(), Encoding.UTF8, "application/json")
                };
            }
        }
    }
}
