using Autofac;
using Autofac.Extras.DynamicProxy2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Dependency;
using Murtain.Domain.Services;

namespace Murtain.Runtime.Validation.Interception
{
    public class ValidationInterceptorRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(context.Assembly)
                   .Where(t => typeof(IApplicationService).IsAssignableFrom(t) && t != typeof(IApplicationService) && !t.IsAbstract)
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(ValidationInterceptor))
                   .InstancePerDependency();

            builder.RegisterType<ValidationInterceptor>();

            builder.Update(context.IocManager.Container);
        }
    }
}
