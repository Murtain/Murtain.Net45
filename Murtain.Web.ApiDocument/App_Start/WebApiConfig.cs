using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

using Murtain.Web.HttpControllerSelectors;
using Murtain.Web.Attributes;

namespace Murtain.OAuth2.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                  name: "namespace",
                  routeTemplate: "api/{namespace}/{controller}/{id}",
                  constraints: new { @namespace = @"v1|v2|v3" },
                  defaults: new { id = RouteParameter.Optional }
              );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));

            config.Filters.Add(new WebApiExceptionFilterAttribute());
            config.Filters.Add(new ValidateModelAttribute());
        }
    }
}
