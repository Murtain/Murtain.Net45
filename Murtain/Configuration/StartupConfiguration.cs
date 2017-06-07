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

            IocManager.Container.AddConventionalRegistrar(new BasicConventionalRegistrar());
            IocManager.Container.AddConventionalRegistrar(new EventBusConventionalRegistras());
            IocManager.Container.AddConventionalRegistrar(new UnitOfWorkConventionalRegistrar());

            IocManager.Container.RegisterIfNot<IEventBus, EventBus>();
            IocManager.Container.RegisterIfNot<IEmailSender, EmailSender>();
            IocManager.Container.RegisterIfNot<IAppSession, AppClaimsSession>();
            IocManager.Container.RegisterIfNot<ICacheManager, NetCacheManager>();

            IocManager.Container.RegisterIfNot<ICurrentUnitOfWorkProvider, CallContextCurrentUnitOfWorkProvider>();
            IocManager.Container.RegisterIfNot<IUnitOfWorkManager, UnitOfWorkManager>();
            IocManager.Container.RegisterIfNot<IUnitOfWorkDefaultOptionsConfiguration, UnitOfWorkDefaultOptionsConfiguration>();

            IocManager.Container.RegisterIfNot<ILanguageProvider, LanguageProvider>();
            IocManager.Container.RegisterIfNot<ILanguageManager, LanguageManager>();

            IocManager.Container.RegisterIfNot<IGlobalSettingStore, SimpleGlobalSettingStore>(DependencyLifeStyle.Transient);
            IocManager.Container.RegisterIfNot<IGlobalSettingManager, GlobalSettingManager>();

            IocManager.Container.RegisterIfNot<ILocalizationManager, LocalizationManager>();
            IocManager.Container.RegisterIfNot<ILocalizationConfiguration, LocalizationConfiguration>();
            IocManager.Container.RegisterIfNot<IEmailSettingConfiguration, EmailSettingConfiguration>();
            IocManager.Container.RegisterIfNot<IGlobalSettingsConfiguration, GlobalSettingsConfiguration>();


            UnitOfWorkDefaultOptionsConfiguration = IocManager.Container.Resolve<IUnitOfWorkDefaultOptionsConfiguration>();
            GlobalSettingsConfiguration = IocManager.Container.Resolve<IGlobalSettingsConfiguration>();
            LocalizationConfiguration = IocManager.Container.Resolve<ILocalizationConfiguration>();

            IocManager.Container.RegisterModule(new LocalizationManagerModule());
            IocManager.Container.RegisterModule(new EventBusModule());
            IocManager.Container.RegisterModule(new GlobalSettingsManagerModule());
            IocManager.Container.RegisterModule(new AppSessionModule());


        }

    }

    public static class StartupConfigurationExtensions
    {

        public static StartupConfiguration UseLoggingLog4net(this StartupConfiguration bootstrap, string configFile = "log4net.config")
        {
            IocManager.Container.RegisterWithParameter<ILoggerFactory, Log4netFactory>("configFile", configFile);
            IocManager.Container.RegisterModule(new LoggingModule());
            return bootstrap;
        }

        public static StartupConfiguration RegisterConsoleApplication(this StartupConfiguration bootstrap, params IModule[] modules)
        {
            IocManager.Container.RegisterAssemblyByConvention(AssemblyLoader.GetAssemblies(), modules);

            return bootstrap;
        }

    

    }
}
