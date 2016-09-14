using Autofac;
using Autofac.Extras.DynamicProxy2;
using Murtain.Dependency;
using Murtain.RedisCache.Lock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.RedisCache.ConventionalRegistas
{
    public class RedisLockRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var builder = new ContainerBuilder();

            //builder.RegisterAssemblyTypes(context.Assembly)
            //     .Where(t => !t.IsAbstract && 
            //                 (t.GetMethods().Any(m => m.IsDefined(typeof(RedisLockAttribute), true)))
            //     )
            //     .EnableClassInterceptors()
            //    ;

            builder.RegisterType<RedisLockInterceptor>();

            builder.Update(context.IocManager.IocContainer);
        }
    }
}
