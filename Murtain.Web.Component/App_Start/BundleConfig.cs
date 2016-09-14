using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Murtain.Web.Component
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var rootPath = "https://172.30.30.190:44300/";

            bundles.Add(new ScriptBundle("~/scripts/jquery"));
            bundles.Add(new ScriptBundle("~/scripts/react", rootPath + "scripts/react/react.min.js"));
        }
    }

    public static class HtmlHelperResourceExtensions
    {
        private static string resourceAbsolute = "https://172.30.30.190:44300/";

        public static IHtmlString RenderScript(this HtmlHelper html, string virtualPath)
        {
            return html.Raw("<script src=\"" + GetResourcesAbsolutePath(virtualPath) + "\" type=\"text/javascript\"></script>");
        }

        public static IHtmlString RenderStyle(this HtmlHelper html, string virtualPath)
        {
            return html.Raw("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + GetResourcesAbsolutePath(virtualPath) + "\" />");
        }

        public static IHtmlString RenderPluginStyles(this HtmlHelper html)
        {
            var styles = new string[]
            {
               "resources/plugins/bootstrap-3.3.0/css/bootstrap.min.css",
               "resources/plugins/bootstrap-3.3.0/css/bootstrap.extend.css",

               "resources/plugins/font-awesome-4.3.0/css/font-awesome.css",

               "resources/css/animate.css",

               "resources/plugins/angular-toaster/toaster.css",
               "resources/plugins/angular-icheck/css/custom.css",
               "resources/plugins/angular-datatables/css/datatables.css",
               "resources/plugins/angular-chosen/css/chosen.css",
               "resources/plugins/angular-laydate/skins/dahong/laydate.css",
               "resources/plugins/angular-switchery/css/switchery.css",
               "resources/plugins/angular-loading-bar/loading-bar.css",

               "resources/plugins/jquery-flavr/css/flavr.css",
               "resources/plugins/jquery-mcustomerscroll/jquery.mcustomscrollbar.min.css",


               "resources/css/inspinia.css",
               "resources/css/override.css"
            };
            StringBuilder sb = new StringBuilder();

            foreach (var s in styles)
            {
                sb.AppendLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + resourceAbsolute + s + "\" />");
            }
            return html.Raw(sb.ToString());
        }

        public static IHtmlString RenderPluginScripts(this HtmlHelper html)
        {
            var scripts = new string[]
            {
               "resources/plugins/jquery-2.1.1/jquery.2.1.1.min.js",
               "resources/plugins/jquery-2.1.1/jquery-borwser.js",

               "resources/plugins/jquery-form/jquery.form.js",
               "resources/plugins/angular-1.3.8/angular.js",

               "resources/plugins/angular-plugins/angular-ui-router.js",
               "resources/plugins/angular-1.3.8/angular-animate.min.js",

               "resources/plugins/bootstrap-3.3.0/js/bootstrap.min.js",

               "resources/plugins/jquery-metismenu/jquery.metisMenu.js",
               "resources/plugins/jquery-slimscroll/jquery.slimscroll.min.js",

               "resources/plugins/angular-icheck/jquery.icheck.min.js",
               "resources/plugins/angular-icheck/angular.icheck.js",
               "resources/plugins/angular-radio/angular.radio.js",

               "resources/plugins/jquery-mcustomerscroll/jquery.mousewheel.min.js",
               "resources/plugins/jquery-mcustomerscroll/jquery.mcustomscrollbar.min.js",

               "resources/plugins/angular-datatables/jquery.datatables-1.10.4.min.js",
               "resources/plugins/angular-datatables/jquery.datatables.fnreloadajax.1.10.4.js",
               "resources/plugins/angular-datatables/jquery.datatables.fixedcolumns.js",
               "resources/plugins/angular-datatables/jquery.datatables-bootstrap.1.10.4.js",
               "resources/plugins/angular-datatables/angular.datatables.js",

               "resources/plugins/angular-chosen/chosen.jquery.js",
               "resources/plugins/angular-chosen/chosen.js",

               "resources/plugins/angular-auto-validate/auto-validate.js",
               "resources/plugins/angular-inputmask/jquery.jasny-bootstrap-3.1.2.js",
               "resources/plugins/angular-inputmask/inputmask.js",

               "resources/plugins/angular-laydate/laydate.js",
               "resources/plugins/angular-laydate/angular-laydate.js",

               "resources/plugins/angular-toaster/toaster.js",

               "resources/plugins/angular-switchery/jquery.switchery.min.js",
               "resources/plugins/angular-switchery/angular.switchery.js",

               "resources/plugins/angular-loading-bar/loading-bar.js",

               "resources/plugins/jquery-flavr/js/flavr.min.js",
               "resources/plugins/jquery-dragsort/jquery-dragsort.js",

               "resources/plugins/jquery-cropbox/cropbox.js",

               "resources/plugins/angular-dialog/ui.bootstrap.js",
               "resources/plugins/angular-dialog/angular-dialog.js",

            };
            StringBuilder sb = new StringBuilder();

            foreach (var s in scripts)
            {
                sb.AppendLine("<script src=\"" + resourceAbsolute + s + "\" type=\"text/javascript\"></script>");
            }
            return html.Raw(sb.ToString());
        }

        private static string GetResourcesAbsolutePath(string virtualPath)
        {
            return resourceAbsolute + virtualPath;
        }
    }
}
