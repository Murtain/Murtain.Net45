using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.Configuration.Startup;
using Murtain.Dependency;
using Murtain.Auditing.ConventionalRegistras;
using Murtain.Auditing.Configuration;
using Murtain.Auditing.Store;
using Murtain.Auditing.Provider;

namespace Murtain.Auditing.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseAuditing(this StartupConfiguration bootstrap, Action<IAuditingConfiguration> invoke = null)
        {
            IocManager.Container.RegisterIfNot<IAuditingConfiguration, AuditingConfiguration>();
            IocManager.Container.RegisterIfNot<IAuditingModelProvider, NullAuditingModelProvider>();
            IocManager.Container.RegisterIfNot<IAuditingStore, LoggingAuditingStore>();

            IocManager.Container.AddConventionalRegistrar(new AuditingRegistrar());

            invoke?.Invoke(IocManager.Container.Resolve<IAuditingConfiguration>());

            return bootstrap;
        }
    }
}
