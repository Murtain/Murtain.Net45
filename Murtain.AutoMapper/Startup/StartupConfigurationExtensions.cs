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
            var types = AssemblyLoader.GetAllTypes(type => type.IsDefined(typeof(AutoMapAttribute)) || type.IsDefined(typeof(AutoMapFromAttribute)) || type.IsDefined(typeof(AutoMapToAttribute)));
            foreach (var type in types)
            {
                configuration.CreateAttributeMaps(type);
            }
        }

        private static void MapOtherMappings(IMapperConfigurationExpression configuration)
        {
            var types = AssemblyLoader.GetAllTypes(type => typeof(IAutoMaping).IsAssignableFrom(type) && type != typeof(IAutoMaping) && !type.IsAbstract).ToList();
            types.ForEach(x =>
            {
                x.GetMethod("CreateMappings").Invoke(Activator.CreateInstance(x), new object[] { configuration });
            });
        }

    }
}
