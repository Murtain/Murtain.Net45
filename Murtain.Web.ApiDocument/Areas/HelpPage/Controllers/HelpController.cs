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


        public async System.Threading.Tasks.Task<JsonResult> TestRequestExcuteAsync(string rootpath,  string apiId, string postData)
        {
            //try
            //{
            //    var settings = new JsonSerializerSettings();
            //    IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //    timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //    settings.Converters.Add(timeConverter);

            //    RequestModelBase request;
            //    HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
            //    Type requestModelType = apiModel.SampleRequestModelType;

            //    Type responseType = apiModel.SampleRequestModelType == null
            //                        ? null
            //                        : apiModel.SampleRequestModelType.Assembly.GetType(apiModel.SampleRequestModelType.FullName.Substring(0, apiModel.SampleRequestModelType.FullName.LastIndexOf(HelpPageConstants.ApiModelRequestSuffix)) + HelpPageConstants.ApiModelResponseSuffix);

            //    request = JsonConvert.DeserializeObject(postData, requestModelType) as RequestModelBase;
            //    var client = new DefaultClient(rootpath);

            //    var responseData = await client.RequestReadAsStringAsync(request);

            //    var result = JsonConvert.DeserializeObject<ResponseWrapper>(responseData);

            //    if (result.code != SystemReturnCode.Completed)
            //    {
            //        return Json(new { response = "[" + result.code + "]" + result.message, post_data = request });
            //    }

            //    return Json(new
            //    {
            //        response = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(result.body), responseType, settings),
            //        post_data = request
            //    });
            //}
            //catch (Exception e)
            //{
            //    return Json(new { response = e.Message + System.Environment.NewLine + e.StackTrace });
            //}
            return Json(new { });
        }
    }
}