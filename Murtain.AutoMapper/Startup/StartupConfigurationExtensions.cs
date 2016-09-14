using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;

using Murtain.Configuration.Startup;
using Murtain.AutoMapper.Configuration;
using Murtain.Dependency;
using Murtain.AutoMapper;

using AutoMapper;

namespace Murtain.Configuration.Startup
{
    public static class StartupConfigurationExtensions
    {
        private const string AssemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";

        public static StartupConfiguration UseAutoMapper(this StartupConfiguration bootstrap, Action<IAutoMapperConfiguration> invoke = null)
        {
            IocManager.Instance.RegisterIfNot<IAutoMapperConfiguration, AutoMapperConfiguration>();

            if (invoke != null)
            {
                invoke(IocManager.Instance.Resolve<IAutoMapperConfiguration>());
            }

            var autoMapperConfigration = IocManager.Instance.Resolve<IAutoMapperConfiguration>();


            Mapper.Initialize(configuration =>
            {
                MapAutoAttributes(configuration);
                MapOtherMappings(configuration);
                foreach (var configurator in autoMapperConfigration.Configurators)
                {
                    configurator(configuration);
                }
            });

            return bootstrap;
        }

        private static void MapAutoAttributes(IMapperConfigurationExpression configuration)
        {
            var types = GetAllTypes(type => type.IsDefined(typeof(AutoMapAttribute)) || type.IsDefined(typeof(AutoMapFromAttribute)) || type.IsDefined(typeof(AutoMapToAttribute)));
            foreach (var type in types)
            {
                configuration.CreateAttributeMaps(type);
            }
        }

        private static void MapOtherMappings(IMapperConfigurationExpression configuration)
        {
            var types = GetAllTypes(type => typeof(IAutoMaping).IsAssignableFrom(type) && type != typeof(IAutoMaping) && !type.IsAbstract).ToList();
            types.ForEach(x =>
            {
                x.GetMethod("CreateMappings").Invoke(Activator.CreateInstance(x), new object[] { configuration });
            });
        }

        private static Type[] GetAllTypes(Func<Type, bool> predicate)
        {
            var allTypes = new List<Type>();

            foreach (var assembly in FilterSystemAssembly(AppDomain.CurrentDomain.GetAssemblies()))
            {
                allTypes.AddRange(assembly.GetTypes().Where(type => type != null));
            }

            return allTypes.Where(predicate).ToArray();
        }

        private static Assembly[] FilterSystemAssembly(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .Where(assembly => !Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
                .ToArray();
        }
    }
}
