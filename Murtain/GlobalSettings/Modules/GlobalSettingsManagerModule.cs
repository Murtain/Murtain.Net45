using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac.Core;
using Murtain.Dependency;

namespace Murtain.GlobalSettings.Modules
{
    public class GlobalSettingsManagerModule : Autofac.Module
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
              .Where(p => p.PropertyType == typeof(IGlobalSettingManager) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, IocManager.Instance.Resolve<IGlobalSettingManager>(), null);
            }
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            // Handle properties.
            registration.Activated += (sender, e) => InjectProperties(e.Instance);
        }
    }
}
