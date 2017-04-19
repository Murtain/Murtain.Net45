using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Web.Mvc
{
    public abstract class LocalizationWebViewPage : LocalizationWebViewPage<decimal>
    {
    }
    public abstract class LocalizationWebViewPage<TModel> : AbstractWebViewPage<TModel>
    {
        protected LocalizationWebViewPage()
        {
           LocalizationSourceName = "Views";
        }
    }
}
