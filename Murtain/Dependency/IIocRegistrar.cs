using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Murtain.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to register dependencies.
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// Checks whether given type is registered before.
        /// </summary>
        /// <param name="type">Type to check</param>
        bool IsRegistered(Type type);
        /// <summary>
        /// Checks whether given type is registered before.
        /// </summary>
        /// <typeparam name="TType">Type to check</typeparam>
        bool IsRegistered<TType>();
        /// <summary>
        /// Adds a dependency registrar for conventional registration.
        /// </summary>
        /// <param name="registrar">dependency registrar</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="IocManager.AddConventionalRegistrar"/> method.
        /// </summary>
        /// <param name="assembly">Assembly to register</param>
        /// <param name="modules">Additional modules</param>
        void RegisterAssemblyByConvention(Assembly[] assembly, params IModule[] modules);
        /// <summary>
        /// Registers a type as self registration.
        /// </summary>
        /// <param name="type">Type of the class</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        /// <summary>
        /// Registers a type as self registration.
        /// </summary>
        /// <typeparam name="TType">Type of the class</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class;
        /// <summary>
        /// Registers a type with it's implementation.
        /// </summary>
        /// <param name="iType">Registering type</typeparam>
        /// <param name="iImpl">The type that implements <see cref="TType"/></typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register(Type iType, Type iImpl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        /// <summary>
        /// Registers a type with it's implementation.
        /// </summary>
        /// <typeparam name="TType">Registering type</typeparam>
        /// <typeparam name="TImpl">The type that implements <see cref="TType"/></typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type</param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;
        /// <summary>
        /// Regisiters a Instance.
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="instance"></param>
        void RegisterInstance<TInstance>(TInstance instance)
             where TInstance : class;
        /// <summary>
        /// Regisiters a Module.
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="module"></param>
        void RegisterModule(IModule module);

        /// <summary>
        /// Register a typ with parameters
        /// </summary>
        void RegisterWithParameter<TType>(string parameterName, object parameterValue, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
            where TType : class;
        /// <summary>
        /// Register a typ with parameters
        /// </summary>
        void RegisterWithParameter<TType, TImpl>(string parameterName, object parameterValue, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
            where TType : class
            where TImpl : class, TType;


        /// <summary>
        /// Register a typ with parameters
        /// </summary>
        void RegisterWithParameters<TType>(IEnumerable<Parameter> parameters, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
            where TType : class;
        /// <summary>
        /// Register a typ with parameters
        /// </summary>
        void RegisterWithParameters<TType, TImpl>(IEnumerable<Parameter> parameters, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// Register a typ with PropertiesAutowired
        /// </summary>
        void RegisterPropertiesAutowired<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;
        
    }
}
