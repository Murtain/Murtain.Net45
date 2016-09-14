using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;
using System.Threading;

using Autofac;
using Murtain.Configuration.Startup;
using Murtain.OAuth2.Core.SettingProviders;
using Murtain.Localization.Settings;
using Murtain.Localization.Dictionaries;
using Murtain.EntityFramework.Startup;
using Murtain.Auditing.Startup;
using Murtain.Domain.UnitOfWork.ConventionalRegistras;
using Murtain.Localization;
using Murtain.Extensions;
using Murtain.OAuth2.Core;
using Murtain.Localization.Dictionaries.Xml;
using System.Reflection;
using Autofac.Extras.DynamicProxy2;
using Murtain.OAuth2.Core.Stores;
using Murtain.GlobalSettings.Store;

namespace Murtain.OAuth2.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StartupConfig.RegisterDependency(config =>
            {
                //应用程序配置
                config.GlobalSettingsConfiguration.Providers.Add<AuthorizationSettingProvider>();
                config.GlobalSettingsConfiguration.Providers.Add<LocalizationSettingProvider>();
                config.GlobalSettingsConfiguration.Providers.Add<ResourcesSettingProvider>();
                config.GlobalSettingsConfiguration.Providers.Add<MessageSettingProvider>();

                //本地化
                config.LocalizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Messages, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Messages)));
                config.LocalizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Events, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Events)));
                config.LocalizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Scopes, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Scopes)));
                config.LocalizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource(Constants.Localization.SourceName.Views, new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), Constants.Localization.RootNamespace.Views)));

                //EF 连接字符串
                config.UseDataAccessEntityFramework(cfg =>
                {
                    cfg.DefaultNameOrConnectionString = "DefaultConnection";
                });

                config.UseAuditing();
                config.UseAutoMapper();

                config.RegisterWebMvcApplication(new ConventionalRegistrarConfig());
            });
        }

        public class ConventionalRegistrarConfig : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                //builder.RegisterType<GlobalSettingStore>()
                //    .As<IGlobalSettingStore>()
                //    .AsImplementedInterfaces()
                //    .EnableInterfaceInterceptors()
                //    .InterceptedBy(typeof(UnitOfWorkInterceptor))
                //    .InstancePerDependency();
                //base.Load(builder);
            }
        }
        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            var langCookie = Request.Cookies["Murtain.Localization.CultureName"];
            if (langCookie != null && LocalizationHelper.IsValidCultureCode(langCookie.Value))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(langCookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCookie.Value);
            }
            else if (!Request.UserLanguages.IsNullOrEmpty())
            {
                var firstValidLanguage = Request
                    .UserLanguages
                    .FirstOrDefault(LocalizationHelper.IsValidCultureCode);

                if (firstValidLanguage != null)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(firstValidLanguage);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(firstValidLanguage);
                }
            }
        }

    }
}
