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
using Murtain.Collections;

namespace Murtain.Configuration.Startup
{
    public static class StartupConfigurationExtensions
    {
        private static bool createdMappingsBefore;
        private static readonly object syncObj = new object();
        private static AssemblyLoader assemblyLoader;

        public static StartupConfiguration UseAutoMapper(this StartupConfiguration bootstrap, Action<IAutoMapperConfiguration> invoke = null)
        {
            IocManager.Instance.RegisterIfNot<IAutoMapperConfiguration, AutoMapperConfiguration>();

            if (invoke != null)
            {
                invoke(IocManager.Instance.Resolve<IAutoMapperConfiguration>());
            }

            var autoMapperConfigration = IocManager.Instance.Resolve<IAutoMapperConfiguration>();
            assemblyLoader = new AssemblyLoader(autoMapperConfigration.AssemblyLoadingPattern);

            lock (syncObj)
            {
                //We should prevent duplicate mapping in an application, since AutoMapper is static.
                if (!createdMappingsBefore)
                {
                    Mapper.Initialize(configuration =>
                    {
                        var autoAttributesClassTypes = assemblyLoader.GetAllTypes(type => type.IsDefined(typeof(AutoMapAttribute)) || type.IsDefined(typeof(AutoMapFromAttribute)) || type.IsDefined(typeof(AutoMapToAttribute))).ToList();
                        var otherMappingsCalssTypes = assemblyLoader.GetAllTypes(type => typeof(IAutoMaping).IsAssignableFrom(type) && type != typeof(IAutoMaping) && !type.IsAbstract).ToList();

                        MapAutoAttributes(configuration, autoAttributesClassTypes);
                        MapOtherMappings(configuration, otherMappingsCalssTypes);

                        foreach (var configurator in autoMapperConfigration.Configurators)
                        {
                            configurator(configuration);
                        }
                    });
                    createdMappingsBefore = true;
                }
            }

            return bootstrap;
        }

        private static void MapAutoAttributes(IMapperConfigurationExpression configuration, List<Type> types)
        {
            foreach (var type in types)
            {
                configuration.CreateAttributeMaps(type);
            }
        }

        private static void MapOtherMappings(IMapperConfigurationExpression configuration, List<Type> types)
        {
            types.ForEach(x =>
            {
                x.GetMethod(nameof(IAutoMaping.CreateMappings)).Invoke(Activator.CreateInstance(x), new object[] { configuration });
            });
        }
    }
}
