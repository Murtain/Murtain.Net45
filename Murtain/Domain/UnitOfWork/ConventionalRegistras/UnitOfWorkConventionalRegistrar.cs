using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Autofac.Extras.DynamicProxy2;
using Murtain.Dependency;
using Murtain.Domain.UnitOfWork;
using Murtain.Domain.Services;
using Murtain.Domain.Repositories;

namespace Murtain.Domain.UnitOfWork.ConventionalRegistras
{
    /// <summary>
    /// Registers classes derived from AppDbContext with configurations.
    /// </summary>
    public class UnitOfWorkConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.Register<ICurrentUnitOfWorkProvider, CallContextCurrentUnitOfWorkProvider>();
            context.IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(context.Assembly)
                   .Where(t => !t.IsAbstract && t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(UnitOfWorkHelper.HasUnitOfWorkAttribute)
                               || UnitOfWorkHelper.IsConventionalUowClass(t)
                   )
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(UnitOfWorkInterceptor))
                   .InstancePerDependency()
                   ;

            builder.RegisterType<UnitOfWorkInterceptor>();

            builder.Update(context.IocManager.Container);
        }

    }
}
