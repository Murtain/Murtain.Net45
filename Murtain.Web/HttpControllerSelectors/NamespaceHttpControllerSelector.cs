using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace Murtain.Web.HttpControllerSelectors
{

    public class NamespaceHttpControllerSelector : DefaultHttpControllerSelector
    {
        private const string MS_SubRoutes = "MS_SubRoutes";
        private const string ROUTE_NAMESPACE_NAME = "namespace";
        private const string ROUTE_CONTROLLER_NAME = "controller";
        private readonly HttpConfiguration _configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;
        private readonly HashSet<string> _duplicates;

        public NamespaceHttpControllerSelector(HttpConfiguration config)
            : base(config)
        {
            _configuration = config;
            _duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }


        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            IHttpRouteData routeData = request.GetRouteData();
            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Get the namespace name and controller variables from the route data.

            string namespaceName = GetRouteVariable(routeData, ROUTE_NAMESPACE_NAME);
            if (string.IsNullOrEmpty(namespaceName))
            {
                namespaceName = GetVersionFromHTTPHeaderAndAcceptHeader(request);
            }
            string controllerName = GetRouteVariable(routeData, ROUTE_CONTROLLER_NAME);
            if (controllerName == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Find a matching controller.
            string key = String.Format(CultureInfo.InvariantCulture, "{0}", controllerName);
            if (!string.IsNullOrEmpty(namespaceName))
            {
                key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, controllerName);
            }

            HttpControllerDescriptor controllerDescriptor;
            if (_controllers.Value.TryGetValue(key, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            else if (_duplicates.Contains(key))
            {
                throw new HttpResponseException(
                    request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "Multiple controllers were found that match this request."));
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public override IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }
        public override string GetControllerName(HttpRequestMessage request)
        {
            return base.GetControllerName(request);
        }


        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

            // Create a lookup table where key is "namespace.controller". The value of "namespace" is the last
            // segment of the full namespace. For example:
            // MyApplication.Controllers.V1.ProductsController => "V1.Products"
            IAssembliesResolver assembliesResolver = _configuration.Services.GetAssembliesResolver();
            IHttpControllerTypeResolver controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();

            ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (Type t in controllerTypes)
            {
                var segments = t.Namespace.Split(Type.Delimiter);

                // For the dictionary key, strip "Controller" from the end of the type name.
                // This matches the behavior of DefaultHttpControllerSelector.
                var controllerName = t.Name.Remove(t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);
                string version = segments[segments.Length - 1];
                var key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version, controllerName);
                if (version == "Controllers")
                {
                    key = String.Format(CultureInfo.InvariantCulture, "{0}", controllerName);
                }
                // Check for duplicate keys.
                if (dictionary.Keys.Contains(key))
                {
                    _duplicates.Add(key);
                }
                else
                {
                    dictionary[key] = new HttpControllerDescriptor(_configuration, t.Name, t);
                }
            }

            // Remove any duplicates from the dictionary, because these create ambiguous matches. 
            // For example, "Foo.V1.ProductsController" and "Bar.V1.ProductsController" both map to "v1.products".
            foreach (string s in _duplicates)
            {
                dictionary.Remove(s);
            }
            return dictionary;
        }
        // Get a value from the route data, if present.
        private string GetRouteVariable(IHttpRouteData routeData, string name)
        {
            object result = null;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return result as string;
            }

            IHttpRouteData subRoteData = ((IEnumerable<IHttpRouteData>)routeData.Values[MS_SubRoutes])?.FirstOrDefault();
            if (subRoteData == null)
            {
                return string.Empty;
            }
            HttpActionDescriptor actionDescriptor = ((HttpActionDescriptor[])subRoteData.Route.DataTokens["actions"])[0];

            if (name == ROUTE_NAMESPACE_NAME)
            {
                Type controllerType = actionDescriptor.ControllerDescriptor.ControllerType;

                string[] segments = controllerType.Namespace.Split(Type.Delimiter);
                return segments[segments.Length - 1] == "Controllers" ? string.Empty : segments[segments.Length - 1];
            }
            if (name == ROUTE_CONTROLLER_NAME)
            {
                string controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
                return controllerName.Remove(controllerName.Length - DefaultHttpControllerSelector.ControllerSuffix.Length).ToLower();
            }

            return string.Empty;

        }
        private string GetRouteController(IHttpRouteData routeData)
        {

            object result = null;
            if (routeData.Values.TryGetValue(ROUTE_CONTROLLER_NAME, out result))
            {
                return result as string;
            }

            IHttpRouteData subRoteData = ((IEnumerable<IHttpRouteData>)routeData.Values[MS_SubRoutes])?.FirstOrDefault();
            if (subRoteData == null)
            {
                return string.Empty;
            }
            HttpActionDescriptor actionDescriptor = ((HttpActionDescriptor[])subRoteData.Route.DataTokens["actions"])[0];

            Type controllerType = actionDescriptor.ControllerDescriptor.ControllerType;

            return controllerType.Name.Remove(controllerType.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);
        }
        private string GetVersionFromHTTPHeaderAndAcceptHeader(HttpRequestMessage request)
        {
            if (request.Headers.Contains(ROUTE_NAMESPACE_NAME))
            {
                var versionHeader = request.Headers.GetValues(ROUTE_NAMESPACE_NAME).FirstOrDefault();
                if (versionHeader != null)
                {
                    return versionHeader;
                }
            }
            var acceptHeader = request.Headers.Accept;
            foreach (var mime in acceptHeader)
            {
                if (mime.MediaType == "application/json" || mime.MediaType == "text/html")
                {
                    var namespaceName = mime.Parameters
                                     .Where(v => v.Name.Equals(ROUTE_NAMESPACE_NAME, StringComparison.OrdinalIgnoreCase))
                                      .FirstOrDefault();

                    if (namespaceName != null)
                    {
                        return namespaceName.Value;
                    }
                    return string.Empty;
                }
            }
            return string.Empty;
        }
    }
}
