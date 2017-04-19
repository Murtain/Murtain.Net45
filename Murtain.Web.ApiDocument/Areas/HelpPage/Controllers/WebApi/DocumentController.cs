using Murtain.SDK.Attributes;
using Murtain.Web.ApiDocument.Areas.HelpPage.ModelDescriptions;
using Murtain.Web.ApiDocument.Areas.HelpPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;

namespace Murtain.Web.ApiDocument.Areas.HelpPage.Controllers.WebApi
{
    /// <summary>
    /// 文档管理|提供文档查询服务
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocumentController : ApiController
    {

        private readonly IDocumentationProvider documentationProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">System.Web.Http.HttpServer 实例的配置</param>
        public DocumentController(HttpConfiguration config)
        {
            base.Configuration = config;
            this.documentationProvider = Configuration.Services.GetDocumentationProvider();
        }

        /// <summary>
        /// 文档查询
        /// </summary>
        /// <returns></returns>
        [Route("api/documents")]
        public IEnumerable<DocumentModel> Get()
        {
            var documents = new List<DocumentModel>();
            var apiGroups = Configuration.Services.GetApiExplorer()
                                                            .ApiDescriptions
                                                            .ToLookup(api => api.ActionDescriptor.ControllerDescriptor)
                                                            ;
            foreach (var group in apiGroups)
            {
                documents.Add(GetDocument(group));
            }

            return documents;
        }
        /// <summary>
        /// 文档查询
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        [Route("api/documents/{controller_name}")]
        public DocumentModel Get(string controller_name)
        {
            var group = Configuration.Services.GetApiExplorer()
                                                         .ApiDescriptions
                                                         .ToLookup(api => api.ActionDescriptor.ControllerDescriptor)
                                                         .FirstOrDefault(x => x.Key.ControllerName.Remove(x.Key.ControllerName.Length - DefaultHttpControllerSelector.ControllerSuffix.Length).Equals(controller_name, StringComparison.OrdinalIgnoreCase));
            if (group == null)
            {
                throw new Exception($"controller {controller_name} not found.");
            }

            return GetDocument(group);

        }
        /// <summary>
        /// 文档查询
        /// </summary>
        /// <returns></returns>
        [Route("api/documents/{controller_name}/api-descriptions")]
        public IEnumerable<ApiDescriptionModel> GetApiDescriptions(string controller_name)
        {

            var groups = Configuration.Services.GetApiExplorer()
                                                         .ApiDescriptions
                                                         .ToLookup(api => api.ActionDescriptor.ControllerDescriptor)
                                                         .Where(x => x.Key.ControllerName.Remove(x.Key.ControllerName.Length - DefaultHttpControllerSelector.ControllerSuffix.Length).Equals(controller_name, StringComparison.OrdinalIgnoreCase));
            if (groups == null)
            {
                throw new Exception($"controller {controller_name} not found.");
            }

            return GetDocumentApiDescriptions(groups);

        }
        /// <summary>
        /// 文档查询
        /// </summary>
        /// <param name="controller_name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/documents/{controller_name}/api-description/{id}")]
        public HelpPageApiModel GetHelpApiModel(string controller_name, string id)
        {

            HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(id);

            if (apiModel == null)
            {
                throw new Exception($"ApiDescription id {id} not found.");
            }


            ModelDescription modelDescription = null;

            var returnCodeAttribute = ((System.Web.Http.Controllers.ReflectedHttpActionDescriptor)apiModel.ApiDescription.ActionDescriptor).MethodInfo.GetCustomAttributes(false).FirstOrDefault(x => x is ReturnCodeAttribute) as ReturnCodeAttribute;
            if (returnCodeAttribute != null)
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                modelDescription = modelDescriptionGenerator.GetOrCreateModelDescription(returnCodeAttribute.ReturnCode);
            }

            DocumentModel document = this.Get(controller_name);

            var returnModel = new HelpPageApiModel
            {
                Id = id,

                ControllerName = document.Description,
                ActionName = document.ApiDescriptions.FirstOrDefault(x => x.Id == id)?.Description,

                HttpMethod = apiModel.ApiDescription.HttpMethod.Method,
                Description = apiModel.ApiDescription.Documentation,
                RelativePath = apiModel.ApiDescription.RelativePath.ToLower(),

                RequestDocumentation = apiModel.RequestDocumentation,
                RequestModelDescription = apiModel.RequestModelDescription,
                ResourceDescription = apiModel.ResourceDescription,

                ReturnCodeModelDescription = modelDescription

            };

            foreach (var item in apiModel.UriParameters)
            {
                returnModel.UriParameters.Add(item);
            }
            foreach (var item in apiModel.SampleRequests)
            {
                returnModel.SampleRequests.Add(item);
            }
            foreach (var item in apiModel.SampleResponses)
            {
                returnModel.SampleResponses.Add(item);
            }

            return returnModel;

        }
        /// <summary>
        /// 文档对象
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        [Route("api/documents/model-description/{modelName}")]
        public ModelDescription GetModelDescription(string modelName)
        {

            ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
            ModelDescription modelDescription;
            if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
            {
                return modelDescription;
            }
            return null;
        }
        private DocumentModel GetDocument(IGrouping<System.Web.Http.Controllers.HttpControllerDescriptor, System.Web.Http.Description.ApiDescription> group)
        {

            var document = new DocumentModel();

            document.Description = documentationProvider?.GetDocumentation(group.Key);

            var controllerName = group.Key.ControllerName;
            document.ControllerName = controllerName.Remove(controllerName.Length - DefaultHttpControllerSelector.ControllerSuffix.Length).ToLower();
            document.ApiDescriptions = GetApiDescriptions(document.ControllerName);
            var segments = group.Key.ControllerType.Namespace.Split(Type.Delimiter);
            document.Namespace = segments[segments.Length - 1] == "Controllers" ? null : segments[segments.Length - 1];

            return document;
        }

        private IList<ApiDescriptionModel> GetDocumentApiDescriptions(IEnumerable<IGrouping<System.Web.Http.Controllers.HttpControllerDescriptor, System.Web.Http.Description.ApiDescription>> groups)
        {

            List<ApiDescriptionModel> models = new List<ApiDescriptionModel>();
            foreach (var group in groups)
            {
                foreach (var api in group.Where(x => !x.Route.RouteTemplate.Contains("api/{namespace}/")))
                {
                    models.Add(new ApiDescriptionModel
                    {
                        Id = api.GetFriendlyId(),
                        HttpMethod = api.HttpMethod.Method,
                        Description = api.Documentation,
                        RelativePath = api.RelativePath?.ToLower()
                    });
                }
            }


            return models;
        }
    }
}
