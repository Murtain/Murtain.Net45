using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Http.Description;
using System.Linq;
using System.Web.Http.Controllers;

using System.Text;
using System.IO;
using System.Configuration;

using Murtain.Web.ApiDocument.Areas.HelpPage.ModelDescriptions;
using Murtain.Web.ApiDocument.Areas.HelpPage.Models;
using Newtonsoft.Json;

using Newtonsoft.Json.Converters;

namespace Murtain.Web.ApiDocument.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Category(string controllerName)
        {

            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            ViewBag.ApiGroups = Configuration.Services.GetApiExplorer().ApiDescriptions.ToLookup(api => api.ActionDescriptor.ControllerDescriptor);

            IGrouping<HttpControllerDescriptor, ApiDescription> grouping = null;
            foreach (var group in Configuration.Services.GetApiExplorer().ApiDescriptions.ToLookup(api => api.ActionDescriptor.ControllerDescriptor))
            {
                if (group.Key.ControllerName.ToLower() == controllerName)
                {
                    grouping = group;
                    break;
                }
            }
            return View(grouping);
        }

        public ActionResult Api(string apiId)
        {

            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();

            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);

                foreach (var group in Configuration.Services.GetApiExplorer().ApiDescriptions.ToLookup(api => api.ActionDescriptor.ControllerDescriptor))
                {
                    if (group.Key.ControllerName == apiModel.ApiDescription.ActionDescriptor.ControllerDescriptor.ControllerName)
                    {
                        ViewBag.ApiGroup = group;
                        break;
                    }
                }

                if (apiModel != null)
                {
                    ViewBag.ApiModel = apiModel;
                    return View(apiModel);
                }
            }
            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult Test(string controllerName, string apiId)
        {
            HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
            return View(apiModel);
        }


        public JsonResult TestRequestExcuteAsync(string url, string appkey, string appSecret, string apiId, string requestBody)
        {
            //try
            //{
            //    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            //    IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //    timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //    jsonSerializerSettings.Converters.Add(timeConverter);

            //    RequestBase request;
            //    HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
            //    Type requestModelType = apiModel.SampleRequestModelType;

            //    Type responseType = apiModel.SampleRequestModelType == null
            //                        ? null
            //                        : apiModel.SampleRequestModelType.Assembly.GetType(apiModel.SampleRequestModelType.FullName.Substring(0, apiModel.SampleRequestModelType.FullName.LastIndexOf(HelpPageConstants.ApiModelRequestSuffix)) + HelpPageConstants.ApiModelResponseSuffix);

            //    request = JsonConvert.DeserializeObject(requestBody, requestModelType) as RequestBase;
            //    request.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    request.Sign = SignHelper.SignRequest(request.GetParametersDictionary(), appSecret);

            //    DefaultClient client = new DefaultClient(request.AppKey, appSecret, url);

            //    var responseData = client.Request(request);

            //    var wrapper = JsonConvert.DeserializeObject<ApiResponseWrapper<object>>(responseData);

            //    if (!string.IsNullOrEmpty(wrapper.ErrorCode))
            //    {
            //        return Json(new { response = "[" + wrapper.ErrorCode + "]" + wrapper.ErrorMessage, post_data = request });
            //    }

            //    return Json(new { response = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(wrapper.Body), responseType, jsonSerializerSettings), post_data = request });
            //}
            //catch (Exception e)
            //{
            //    return Json(new { response = "发送请求时出现异常，请核对必须的参数的值与数据类型。" + System.Environment.NewLine + e.Message + System.Environment.NewLine + e.StackTrace });
            //}
            return Json(new { });
        }
    }
}