using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Murtain.Web.ApiDocument.Areas.HelpPage.Models
{
    /// <summary>
    /// 文档
    /// </summary>
    public class DocumentModel
    {
        /// <summary>
        /// 文档描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// API版本
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// API
        /// </summary>
        public IEnumerable<ApiDescriptionModel> ApiDescriptions { get; internal set; }
    }
}