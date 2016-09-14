using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using AutoMapper;
using Murtain.AutoMapper;

namespace Murtain.AutoMapper
{
    internal static class IMapperConfigurationExpressionExtensions
    {
        public static void CreateAttributeMaps(this IMapperConfigurationExpression configuration, Type type)
        {
            configuration.CreateAttributeMap<AutoMapFromAttribute>(type);
            configuration.CreateAttributeMap<AutoMapToAttribute>(type);
            configuration.CreateAttributeMap<AutoMapAttribute>(type);
        }
        private static void CreateAttributeMap<TAttribute>(this IMapperConfigurationExpression configuration, Type type)
            where TAttribute : AutoMapAttribute
        {
            if (!type.IsDefined(typeof(TAttribute)))
            {
                return;
            }

            foreach (var autoMapToAttribute in type.GetCustomAttributes<TAttribute>())
            {
                if (autoMapToAttribute.TargetTypes == null || autoMapToAttribute.TargetTypes.Length == 0)
                {
                    continue;
                }

                foreach (var targetType in autoMapToAttribute.TargetTypes)
                {
                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.To))
                    {
                        configuration.CreateMap(type, targetType);
                    }

                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.From))
                    {
                        configuration.CreateMap(targetType, type);
                    }
                }
            }
        }
    }
}
