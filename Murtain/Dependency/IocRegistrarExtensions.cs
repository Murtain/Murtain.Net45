using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Dependency
{
    /// <summary>
    /// Extension methods for <see cref="IIocRegistrar"/> interface.
    /// </summary>
    public static class IocRegistrarExtensions
    {
        /// <summary>
        /// Registers a type as self registration if it's not registered before.
        /// </summary>
        /// <param name="iocRegistrar">Registrar</param>
        /// <param name="Type">type</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        /// <returns>True, if registered for given implementation.</returns>
        public static void RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return;
            }

            iocRegistrar.Register(type, lifeStyle);
        }
        /// <summary>
        /// Registers a type as self registration if it's not registered before.
        /// </summary>
        /// <param name="iocRegistrar">Registrar</param>
        /// <param name="iType">Type of the class</param>
        /// <param name="implType">Impl type of the class</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        /// <returns>True, if registered for given implementation.</returns>
        public static void RegisterIfNot(this IIocRegistrar iocRegistrar, Type iType, Type implType, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(iType))
            {
                return;
            }
            iocRegistrar.Register(iType, implType, lifeStyle);
        }
        /// <summary>
        /// Registers a type as self registration if it's not registered before.
        /// </summary>
        /// <typeparam name="T">Type of the class</typeparam>
        /// <param name="iocRegistrar">Registrar</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        /// <returns>True, if registered for given implementation.</returns>
        public static void RegisterIfNot<T>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class
        {
            if (iocRegistrar.IsRegistered<T>())
            {
                return;
            }

            iocRegistrar.Register<T>(lifeStyle);
        }
        /// <summary>
        /// Registers a type as self registration if it's not registered before.
        /// </summary>
        /// <param name="iocRegistrar">Registrar</param>
        /// <param name="type">Type of the class</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        /// <returns>True, if registered for given implementation.</returns>
        public static void RegisterIfNot<TType, TImpl>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            if (iocRegistrar.IsRegistered<TType>())
            {
                return;
            }
            iocRegistrar.Register<TType, TImpl>(lifeStyle);
        }

        /// <summary>
        /// Registers a type as self registration if it's not registered before.
        /// </summary>
        /// <param name="iocRegistrar">Registrar</param>
        /// <param name="type">Type of the class</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        /// <returns>True, if registered for given implementation.</returns>
        public static void RegisterIfNotWithPropertiesAutowired<TType, TImpl>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            if (iocRegistrar.IsRegistered<TType>())
            {
                return;
            }
            iocRegistrar.RegisterPropertiesAutowired<TType, TImpl>(lifeStyle);
        }
    }
}
