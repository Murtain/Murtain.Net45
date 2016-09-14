using Murtain.Dependency;
using Murtain.GlobalSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Murtain.Web.Mvc
{
    public abstract class GlobalSettingWebViewPage<TModel> : WebViewPage<TModel>
    {
        /// <summary>
        /// Reference to the setting manager.
        /// </summary>
        public IGlobalSettingManager GlobalSettingManager { get; set; }
        public GlobalSettingWebViewPage()
        {
            GlobalSettingManager = IocManager.Instance.Resolve<IGlobalSettingManager>();
        }
    }

    public abstract class GlobalSettingWebViewPage : GlobalSettingWebViewPage<decimal>
    {
    }
}
