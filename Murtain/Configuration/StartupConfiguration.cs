using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Compilation;

using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;

using Castle.Core.Logging;
using Castle.Services.Logging.Log4netIntegration;

using Murtain.Dependency;
using Murtain.Dependency.ConventionalRegistrars;
using Murtain.Modules.Logging;
using Murtain.Runtime.Session.Modules;
using Murtain.Caching;
using Murtain.Domain.UnitOfWork.Configuration;
using Murtain.GlobalSettings.Configuration;
using Murtain.GlobalSettings.Modules;
using Murtain.GlobalSettings;
using Murtain.GlobalSettings.Store;
using Murtain.Events.ConventionalRegistras;
using Murtain.Events.Modules;
using Murtain.Localization.Configuration;
using Murtain.Localization.Modules;
using Murtain.Runtime.Session;
using Murtain.Net.Mail.Configuration;
using Murtain.Net.Mail;
using Murtain.Domain.UnitOfWork;
using Murtain.Localization;
using Murtain.Localization.Language;
using Murtain.Events;
using Murtain.Domain.UnitOfWork.ConventionalRegistras;
namespace Murtain.Configuration.Startup
{

    public class StartupConfig
    {
        public static void RegisterDependency(Action<StartupConfiguration> invoke)
        {
            invoke(new StartupConfiguration());
        }
    }
    public class StartupConfiguration
    {
        /// <summary>
        /// 工作单元默认配置
        /// </summary>
        public IUnitOfWorkDefaultOptionsConfiguration UnitOfWorkDefaultOptionsConfiguration { get; set; }
        /// <summary>
        /// 全局设置配置
        /// </summary>
        public IGlobalSettingsConfiguration GlobalSettingsConfiguration { get; set; }
        /// <summary>
        /// 本地化配置
        /// </summary>
        public ILocalizationConfiguration LocalizationConfiguration { get; set; }

        public StartupConfiguration()
        {

            IocManager.Instance.AddConventionalRegistrar(new BasicConventionalRegistrar());
            IocManager.Instance.AddConventionalRegistrar(new EventBusConventionalRegistras());
            IocManager.Instance.AddConventionalRegistrar(new UnitOfWorkConventionalRegistrar());

            IocManager.Instance.RegisterIfNot<IEventBus, EventBus>();
            IocManager.Instance.RegisterIfNot<IEmailSender, EmailSender>();
            IocManager.Instance.RegisterIfNot<IAppSession, AppClaimsSession>();
            IocManager.Instance.RegisterIfNot<ICacheManager, NetCacheManager>();

            IocManager.Instance.RegisterIfNot<ICurrentUnitOfWorkProvider, CallContextCurrentUnitOfWorkProvider>();
            IocManager.Instance.RegisterIfNot<IUnitOfWorkManager, UnitOfWorkManager>();
            IocManager.Instance.RegisterIfNot<IUnitOfWorkDefaultOptionsConfiguration, UnitOfWorkDefaultOptionsConfiguration>();

            IocManager.Instance.RegisterIfNot<ILanguageProvider, LanguageProvider>();
            IocManager.Instance.RegisterIfNot<ILanguageManager, LanguageManager>();

            IocManager.Instance.RegisterIfNot<IGlobalSettingStore, SimpleGlobalSettingStore>(DependencyLifeStyle.Transient);
            IocManager.Instance.RegisterIfNot<IGlobalSettingManager, GlobalSettingManager>();

            IocManager.Instance.RegisterIfNot<ILocalizationManager, LocalizationManager>();
            IocManager.Instance.RegisterIfNot<ILocalizationConfiguration, LocalizationConfiguration>();
            IocManager.Instance.RegisterIfNot<IEmailSettingConfiguration, EmailSettingConfiguration>();
            IocManager.Instance.RegisterIfNot<IGlobalSettingsConfiguration, GlobalSettingsConfiguration>();


            UnitOfWorkDefaultOptionsConfiguration = IocManager.Instance.Resolve<IUnitOfWorkDefaultOptionsConfiguration>();
            GlobalSettingsConfiguration = IocManager.Instance.Resolve<IGlobalSettingsConfiguration>();
            LocalizationConfiguration = IocManager.Instance.Resolve<ILocalizationConfiguration>();

            IocManager.Instance.RegisterModule(new LocalizationManagerModule());
            IocManager.Instance.RegisterModule(new EventBusModule());
            IocManager.Instance.RegisterModule(new GlobalSettingsManagerModule());
            IocManager.Instance.RegisterModule(new AppSessionModule());


        }

    }

    public static class StartupConfigurationExtensions
    {
        private const string AssemblySkipLoadingPattern = "^System|^vshost32|^Nito.AsyncEx|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";

        public static StartupConfiguration UseLoggingLog4net(this StartupConfiguration bootstrap, string configFile = "log4net.config")
        {
            IocManager.Instance.RegisterWithParameter<ILoggerFactory, Log4netFactory>("configFile", configFile);
            IocManager.Instance.RegisterModule(new LoggingModule());
            return bootstrap;
        }
        public static StartupConfiguration RegisterWebMvcApplication(this StartupConfiguration bootstrap, params IModule[] modules)
        {
            var assemblies = FilterSystemAssembly(BuildManager.GetReferencedAssemblies().Cast<Assembly>());
            IocManager.Instance.RegisterAssemblyByConvention(assemblies);

            IocManager.Instance.AddConventionalRegistrar(new ControllerConventionalRegistrar());
            IocManager.Instance.RegisterAssemblyByConvention(assemblies, modules);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IocManager.Instance.IocContainer));

            return bootstrap;
        }
        public static StartupConfiguration RegisterWebApiApplication(this StartupConfiguration bootstrap, params Autofac.Module[] modules)
        {
            var assemblies = FilterSystemAssembly(BuildManager.GetReferencedAssemblies().Cast<Assembly>());
            IocManager.Instance.RegisterAssemblyByConvention(assemblies);

            HttpConfiguration configuration = GlobalConfiguration.Configuration;

            IocManager.Instance.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());

            IocManager.Instance.RegisterAssemblyByConvention(assemblies, modules);
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(IocManager.Instance.IocContainer);

            return bootstrap;
        }
        public static StartupConfiguration RegisterConsoleApplication(this StartupConfiguration bootstrap, params IModule[] modules)
        {
            IocManager.Instance.RegisterAssemblyByConvention(AppDomain.CurrentDomain.GetAssemblies(), modules);

            return bootstrap;
        }

        private static Assembly[] GetAssemblies()
        {
            var path = GetPhysicalPath(AppDomain.CurrentDomain.BaseDirectory);
            return FilterSystemAssembly(GetAssemblies(path)).ToArray();
        }
        private static List<string> GetAllFiles(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList();
        }
        private static List<Assembly> GetAssemblies(string directoryPath)
        {
            var filePaths = GetAllFiles(directoryPath).Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"));
            return filePaths.Select(Assembly.LoadFile).ToList();
        }
        private static string GetPhysicalPath(string relativePath)
        {
            if (HttpContext.Current == null)
            {
                if (relativePath.StartsWith("~"))
                {
                    relativePath = relativePath.Remove(0, 2);
                }
                return Path.GetFullPath(relativePath);
            }
            if (relativePath.StartsWith("~"))
            {
                return HttpContext.Current.Server.MapPath(relativePath);
            }

            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
            {
                return HttpContext.Current.Server.MapPath("~" + relativePath);
            }
            if (HttpContext.Current != null)
            {
                return relativePath + "bin";
            }
            return HttpContext.Current.Server.MapPath("~/" + relativePath);
        }
        private static Assembly[] FilterSystemAssembly(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .Where(assembly => !Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
                .ToArray();
        }

    }
}
