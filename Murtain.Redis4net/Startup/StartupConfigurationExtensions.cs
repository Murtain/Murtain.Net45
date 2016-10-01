using Autofac;
using Murtain.Caching;
using Murtain.Configuration.Startup;
using Murtain.Dependency;
using Murtain.Redis4net.Configuration;
using Murtain.RedisCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Configuration.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseRedis4net(this StartupConfiguration bootstrap, Action<IRedis4netConfiguration> invoke = null)
        {
            if (invoke != null)
            {
                invoke(IocManager.Instance.Resolve<IRedis4netConfiguration>());
            }

            return bootstrap;
        }

    }
}
