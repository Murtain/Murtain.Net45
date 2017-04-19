using System.Web.Http;
using System.Web.Mvc;

namespace Murtain.Web.ApiDocument.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "document/{action}/{id}",
                new { controller = "Document", action = "Index", id = UrlParameter.Optional });
            

            HelpPageConfig.Register(GlobalConfiguration.Configuration);


        }
    }
}