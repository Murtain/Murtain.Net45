using System;
using System.Linq;

using Castle.Core;

using Murtain.Dependency;
using Murtain.Auditing.Configuration;
using Murtain.Auditing.Attributes;

using Autofac;
using Autofac.Extras.DynamicProxy2;
using Murtain.Auditing.Store;
using Castle.Core.Logging;
using Murtain.Domain.Services;
using Murtain.Domain.UnitOfWork.ConventionalRegistras;
using System.ComponentModel;
using System.Collections.Generic;

namespace Murtain.Auditing.ConventionalRegistras
{
    public class AuditingRegistrar : IConventionalDependencyRegistrar
    {
        private IAuditingConfiguration _config;
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            _config = context.IocManager.Resolve<IAuditingConfiguration>();
            if (_config.IsEnabled)
            {
                var builder = new ContainerBuilder();

                builder.RegisterAssemblyTypes(context.Assembly)
                     .Where(t => _config.Selectors.Any(selector => selector.Predicate(t))
                                || (t.IsDefined(typeof(AuditedAttribute), true))
                                || (t.GetMethods().Any(m => m.IsDefined(typeof(AuditedAttribute), true))))
                    .AsImplementedInterfaces()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(AuditingInterceptor))
                    ;

                builder.RegisterAssemblyTypes(context.Assembly)
                    .Where(t => !t.IsAbstract && typeof(IApplicationService).IsAssignableFrom(t) && t != typeof(IApplicationService))
                    .AsImplementedInterfaces()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(AuditingInterceptor),typeof(UnitOfWorkInterceptor))
                   ;

                builder.RegisterType<AuditingInterceptor>();

                builder.Update(context.IocManager.IocContainer);
            }
        }
    }
}