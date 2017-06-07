using Autofac;
using Murtain.Caching;
using Murtain.Configuration.Startup;
using Murtain.Dependency;
using Murtain.RedisCache;
using Murtain.RedisCache.Configuration;
using Murtain.RedisCache.ConventionalRegistas;
using Murtain.RedisCache.Lock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Configuration.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseRedisCache(this StartupConfiguration bootstrap, Action<IRedisCacheConfiguartion> invoke)
        {
            IocManager.Container.RegisterIfNot<IRedisCacheConfiguartion, RedisCacheConfiguartion>();
            IRedisCacheConfiguartion _redisCacheConfiguration = IocManager.Container.Resolve<IRedisCacheConfiguartion>();


            invoke(_redisCacheConfiguration);

            if (string.IsNullOrWhiteSpace(_redisCacheConfiguration.NameOrConnectionString))
            {
                throw new ArgumentNullException("Redis connectstring cannot be empty. ");
            }

            var builder = new ContainerBuilder();
            builder.RegisterType<RedisCacheManager>()
                   .As<ICacheManager>()
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .WithParameter("configuration", _redisCacheConfiguration.NameOrConnectionString)
                   .SingleInstance();

            builder.RegisterType<RedisLockManager>()
                   .As<IRedisLockManager>()
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .WithParameter("configuration", _redisCacheConfiguration.RedisLockNameOrConnectionString)
                   .SingleInstance();

            builder.Update(IocManager.Container.IocContainer);

            IocManager.Container.AddConventionalRegistrar(new RedisLockRegistrar());

            return bootstrap;
        }

    }
}
