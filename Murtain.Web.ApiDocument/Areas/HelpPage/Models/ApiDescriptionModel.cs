using Murtain.Web.ApiDocument.Areas.HelpPage.ModelDescriptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Description;

namespace Murtain.Web.ApiDocument.Areas.HelpPage.Models
{
    /// <summary>
    /// API文档
    /// </summary>
    public class ApiDescriptionModel 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiDescriptionModel()
        {
        }
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the HttpMethod.
        /// </summary>
        public string HttpMethod { get; set; }
        /// <summary>
        /// Gets or sets the RelativePath.
        /// </summary>
        public string RelativePath { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        public string Namespace { get; set; }

    }
}