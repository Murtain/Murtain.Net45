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
using System.IO;
using System.Web;

namespace Murtain.Configuration.Startup
{
    public static class StartupConfigurationExtensions
    {
        private const string AssemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease|^IdentityServer3";

        private static bool _createdMappingsBefore;
        private static readonly object _syncObj = new object();

        public static StartupConfiguration UseAutoMapper(this StartupConfiguration bootstrap, Action<IAutoMapperConfiguration> invoke = null)
        {
            IocManager.Instance.RegisterIfNot<IAutoMapperConfiguration, AutoMapperConfiguration>();

            if (invoke != null)
            {
                invoke(IocManager.Instance.Resolve<IAutoMapperConfiguration>());
            }

            var autoMapperConfigration = IocManager.Instance.Resolve<IAutoMapperConfiguration>();

            lock (_syncObj)
            {
                //We should prevent duplicate mapping in an application, since AutoMapper is static.
                if (!_createdMappingsBefore)
                {
                    Mapper.Initialize(configuration =>
                    {
                        MapAutoAttributes(configuration);
                        MapOtherMappings(configuration);
                        foreach (var configurator in autoMapperConfigration.Configurators)
                        {
                            configurator(configuration);
                        }
                    });
                    _createdMappingsBefore = true;
                }
            }

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


        private static Assembly[] GetAssemblies()
        {
            var path = GetPhysicalPath(AppDomain.CurrentDomain.BaseDirectory);
            return FilterSystemAssembly(GetAssemblies(path)).ToArray();
        }
        private static List<string> GetAllFiles(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList();
        }
        private static List<Assembly> GetAssemblies(string directoryPath)
        {
            var filePaths = GetAllFiles(directoryPath).Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"));
            return filePaths.Select(Assembly.LoadFrom).ToList();
        }
        private static string GetPhysicalPath(string relativePath)
        {
            if (HttpContext.Current == null)
            {
                if (relativePath.StartsWith("~"))
                {
                    relativePath = relativePath.Remove(0, 2);
                }
                return Path.GetFullPath(relativePath);
            }
            if (relativePath.StartsWith("~"))
            {
                return HttpContext.Current.Server.MapPath(relativePath);
            }

            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
            {
                return HttpContext.Current.Server.MapPath("~" + relativePath);
            }
            if (HttpContext.Current != null)
            {
                return relativePath + "bin";
            }
            return HttpContext.Current.Server.MapPath("~/" + relativePath);
        }
        private static Assembly[] FilterSystemAssembly(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .Where(assembly => !Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
                .ToArray();
        }

        private static Type[] GetAllTypes(Func<Type, bool> predicate)
        {
            var allTypes = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                allTypes.AddRange(assembly.GetTypes().Where(type => type != null));
            }

            return allTypes.Where(predicate).ToArray();
        }

    }
}
