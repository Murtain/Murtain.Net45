using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Murtain.Configuration.Startup;
using Murtain.Dependency;
using Murtain.Domain.UnitOfWork.ConventionalRegistras;
using Murtain.EntityFramework.ConventionalRegistras;
using Murtain.EntityFramework.Configuration;

namespace Murtain.Configuration.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseDataAccessEntityFramework(this StartupConfiguration bootstrap, Action<IEntityFrameworkConfiguration> invoke = null)
        {
            IocManager.Container.RegisterIfNot<IEntityFrameworkConfiguration, EntityFrameworkConfiguration>();
            IocManager.Container.AddConventionalRegistrar(new EntityFrameworkConventionalRegistrar());

            if (invoke != null)
            {
                invoke(IocManager.Container.Resolve<IEntityFrameworkConfiguration>());
            }

            return bootstrap;
        }

    }
}
