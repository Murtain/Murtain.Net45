using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Murtain.Configuration.Startup;
using Murtain.Dependency;
using Murtain.Domain.UnitOfWork.ConventionalRegistras;
using Murtain.EntityFramework.ConventionalRegistras;
using Murtain.EntityFramework.Configuration;

namespace Murtain.EntityFramework.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseDataAccessEntityFramework(this StartupConfiguration bootstrap, Action<IEntityFrameworkConfiguration> invoke = null)
        {
            IocManager.Instance.RegisterIfNot<IEntityFrameworkConfiguration, EntityFrameworkConfiguration>();
            IocManager.Instance.AddConventionalRegistrar(new EntityFrameworkConventionalRegistrar());

            if (invoke != null)
            {
                invoke(IocManager.Instance.Resolve<IEntityFrameworkConfiguration>());
            }

            return bootstrap;
        }

    }
}
