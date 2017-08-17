using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac.Core;
using Murtain.Dependency;

namespace Murtain.Runtime.Session.Modules
{
    public class AppSessionModule : Autofac.Module
    {
        private static void InjectProperties(object instance)
        {
            if (Castle.DynamicProxy.ProxyUtil.IsProxy(instance))
            {
                instance = Castle.DynamicProxy.ProxyUtil.GetUnproxiedInstance(instance);
            }

            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(p => p.PropertyType == typeof(IAppSession) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, IocManager.Instance.Resolve<IAppSession>(), null);
            }
        }
        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Union(
                                      new[]
                              {
                                new ResolvedParameter(
                                    (p, i) => p.ParameterType == typeof(IAppSession),
                                    (p, i) =>  new AppClaimsSession()
                                ),
                              });
        }
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            // Handle constructor parameters.
            registration.Preparing += OnComponentPreparing;
            // Handle properties.
            registration.Activated += (sender, e) => InjectProperties(e.Instance);
        }
    }
}
