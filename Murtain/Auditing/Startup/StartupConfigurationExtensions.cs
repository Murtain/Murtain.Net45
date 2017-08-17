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
            IocManager.Instance.RegisterIfNot<IAuditingConfiguration, AuditingConfiguration>();
            IocManager.Instance.RegisterIfNot<IAuditingModelProvider, NullAuditingModelProvider>();
            IocManager.Instance.RegisterIfNot<IAuditingStore, LoggingAuditingStore>();

            IocManager.Instance.AddConventionalRegistrar(new AuditingRegistrar());

            invoke?.Invoke(IocManager.Instance.Resolve<IAuditingConfiguration>());

            return bootstrap;
        }
    }
}
