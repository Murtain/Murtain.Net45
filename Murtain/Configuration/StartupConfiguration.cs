using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Compilation;

using Autofac;
using Autofac.Core;

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
using Murtain.Collections;
using Murtain.Caching.Configuration;
using Murtain.GlobalSettings.Provider;

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
        /// <summary>
        /// 缓存管理配置
        /// </summary>
        public ICacheSettingsConfiguration CacheSettingsConfiguration { get; set; }


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

            IocManager.Instance.RegisterIfNot<ICacheSettingManager, CacheSettingManager>();

            IocManager.Instance.RegisterIfNot<ILocalizationManager, LocalizationManager>();

            IocManager.Instance.RegisterIfNot<ILocalizationConfiguration, LocalizationConfiguration>();
            IocManager.Instance.RegisterIfNot<IEmailSettingConfiguration, EmailSettingConfiguration>();

            IocManager.Instance.RegisterIfNot<ICacheSettingsConfiguration, CacheSettingsConfiguration>();
            IocManager.Instance.RegisterIfNot<IGlobalSettingsConfiguration, GlobalSettingsConfiguration>();


            UnitOfWorkDefaultOptionsConfiguration = IocManager.Instance.Resolve<IUnitOfWorkDefaultOptionsConfiguration>();
            LocalizationConfiguration = IocManager.Instance.Resolve<ILocalizationConfiguration>();
            CacheSettingsConfiguration = IocManager.Instance.Resolve<ICacheSettingsConfiguration>();
            GlobalSettingsConfiguration = IocManager.Instance.Resolve<IGlobalSettingsConfiguration>();


            CacheSettingsConfiguration.Providers.Add<GlobalSettingCacheProvider>();

            GlobalSettingsConfiguration.Providers.Add<EmailSettingProvider>();


            IocManager.Instance.RegisterModule(new LocalizationManagerModule());
            IocManager.Instance.RegisterModule(new EventBusModule());
            IocManager.Instance.RegisterModule(new GlobalSettingsManagerModule());
            IocManager.Instance.RegisterModule(new AppSessionModule());


        }

    }

    public static class StartupConfigurationExtensions
    {

        public static StartupConfiguration UseLoggingLog4net(this StartupConfiguration bootstrap, string configFile = "log4net.config")
        {
            IocManager.Instance.RegisterWithParameter<ILoggerFactory, Log4netFactory>("configFile", configFile);
            IocManager.Instance.RegisterModule(new LoggingModule());
            return bootstrap;
        }

        public static StartupConfiguration RegisterConsoleApplication(this StartupConfiguration bootstrap, string assemblyLoaderPartner, params IModule[] modules)
        {
            IocManager.Instance.RegisterAssemblyByConvention(new AssemblyLoader(assemblyLoaderPartner).GetAssemblies(), modules);
            return bootstrap;
        }
    }
}
