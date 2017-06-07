using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Dependency;
using Autofac;
using Murtain.Events.Handlers;
using System.Reflection;

namespace Murtain.Events.ConventionalRegistras
{
    public class EventBusConventionalRegistras : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            IEventBus eventBus = IocManager.Container.Resolve<IEventBus>();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(context.Assembly)
                    .Where(t => typeof(IEventHandler).IsAssignableFrom(t) && t != typeof(IEventHandler) && !t.IsAbstract)
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .InstancePerDependency();

            builder.Update(IocManager.Container.IocContainer);

            foreach (var assembly in context.Assembly)
            {
                var types = assembly.GetTypes().Where(t => typeof(IEventHandler).IsAssignableFrom(t) && t != typeof(IEventHandler));
                foreach (var type in types)
                {
                    if (!typeof(IEventHandler).IsAssignableFrom(type) || type.IsNotPublic)
                    {
                        continue;
                    }
                    var interfaces = type.GetInterfaces();
                    foreach (var inter in interfaces)
                    {
                        var genericArgs = inter.GetGenericArguments();
                        if (genericArgs.Length == 1)
                        {
                            eventBus.Register(genericArgs[0], (IEventHandler)IocManager.Container.Resolve(type));
                        }
                    }


                }
            }
        }
    }
}
