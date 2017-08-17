using System;
using System.Collections.Generic;
using System.Reflection;

using Autofac;
using Autofac.Core;
using Murtain.Runtime.Session.Modules;

namespace Murtain.Dependency
{
    /// <summary>
    /// This class is used to directly perform dependency injection tasks.
    /// </summary>
    public class IocManager : IIocManager
    {
        private List<IConventionalDependencyRegistrar> _conventionalRegistrars;
        public static IocManager Instance { get; private set; }

        static IocManager()
        {
            Instance = new IocManager();
        }
        public IocManager()
        {
            var builder = new ContainerBuilder();
            Container = builder.Build();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();
        }
        public IContainer Container { get; private set; }
        public bool IsRegistered(Type type)
        {
            return Container.IsRegistered(type);
        }
        public bool IsRegistered<TType>()
        {
            return Container.IsRegistered<TType>();
        }
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }
        public void RegisterAssemblyByConvention(Assembly[] assembly, params IModule[] modules)
        {
            var context = new ConventionalRegistrationContext(assembly, IocManager.Instance);
            var builder = new ContainerBuilder();

            _conventionalRegistrars.ForEach(x => { x.RegisterAssembly(context); });

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            builder.Update(Container);
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();
        }
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    builder.RegisterType(type).AsSelf().SingleInstance();
                    break;
                case DependencyLifeStyle.Transient:
                    builder.RegisterType(type).AsSelf().InstancePerDependency();
                    break;
                default:
                    break;
            }
            builder.Update(Container);
        }
        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    builder.RegisterType<TType>().AsSelf().SingleInstance();
                    break;
                case DependencyLifeStyle.Transient:
                    builder.RegisterType<TType>().AsSelf().InstancePerDependency();
                    break;
                default:
                    break;
            }
            builder.Update(Container);
        }
        public void RegisterPropertiesAutowired<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
             where TType : class
             where TImpl : class, TType
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    builder.RegisterType<TImpl>().As<TType>().AsImplementedInterfaces().PropertiesAutowired().SingleInstance();
                    break;
                case DependencyLifeStyle.Transient:
                    builder.RegisterType<TImpl>().As<TType>().AsImplementedInterfaces().PropertiesAutowired().InstancePerDependency();
                    break;
                default:
                    break;
            }
            builder.Update(Container);
        }
        public void Register(Type iType, Type iImpl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    builder.RegisterType(iImpl).As(iType).AsImplementedInterfaces().SingleInstance();
                    break;
                case DependencyLifeStyle.Transient:
                    builder.RegisterType(iImpl).As(iType).AsImplementedInterfaces().InstancePerDependency();
                    break;
                default:
                    break;
            }
            builder.Update(Container);
        }
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    builder.RegisterType<TImpl>().As<TType>().AsImplementedInterfaces().SingleInstance();
                    break;
                case DependencyLifeStyle.Transient:
                    builder.RegisterType<TImpl>().As<TType>().AsImplementedInterfaces().InstancePerDependency();
                    break;
                default:
                    break;
            }
            builder.Update(Container);
        }
        public void RegisterWithParameter<TType>(string parameterName, object parameterValue, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    builder.RegisterType<TType>().WithParameter(parameterName, parameterValue).AsSelf().AsImplementedInterfaces().SingleInstance();
                    break;
                case DependencyLifeStyle.Transient:
                    builder.RegisterType<TType>().WithParameter(parameterName, parameterValue).AsSelf().AsImplementedInterfaces().InstancePerDependency();
                    break;
                default:
                    break;
            }
            builder.Update(Container);
        }
        public void RegisterWithParameter<TType, TImpl>(string parameterName, object parameterValue, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    builder.RegisterType<TImpl>().WithParameter(parameterName, parameterValue).As<TType>().AsImplementedInterfaces().SingleInstance();
                    break;
                case DependencyLifeStyle.Transient:
                    builder.RegisterType<TImpl>().WithParameter(parameterName, parameterValue).As<TType>().AsImplementedInterfaces().InstancePerDependency();
                    break;
                default:
                    break;
            }
            builder.Update(Container);
        }
        public void RegisterWithParameters<TType>(IEnumerable<Parameter> parameters, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    builder.RegisterType<TType>().WithParameters(parameters).AsSelf().AsImplementedInterfaces().SingleInstance();
                    break;
                case DependencyLifeStyle.Transient:
                    builder.RegisterType<TType>().WithParameters(parameters).AsSelf().AsImplementedInterfaces().InstancePerDependency();
                    break;
                default:
                    break;
            }
            builder.Update(Container);
        }
        public void RegisterWithParameters<TType, TImpl>(IEnumerable<Parameter> parameters, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            var builder = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    builder.RegisterType<TImpl>().WithParameters(parameters).As<TType>().AsImplementedInterfaces().SingleInstance();
                    break;
                case DependencyLifeStyle.Transient:
                    builder.RegisterType<TImpl>().WithParameters(parameters).As<TType>().AsImplementedInterfaces().InstancePerDependency();
                    break;
                default:
                    break;
            }
            builder.Update(Container);
        }
        public void RegisterInstance<TInstance>(TInstance instance)
             where TInstance : class
        {
            var _builder = new ContainerBuilder();
            _builder.RegisterInstance<TInstance>(instance);
            _builder.Update(Container);
        }
        public void RegisterModule(IModule module)
        {
            var _builder = new ContainerBuilder();
            _builder.RegisterModule(module);
            _builder.Update(Container);
        }
        public object Resolve(Type type, params Parameter[] parameters)
        {
            return Container.Resolve(type, parameters);
        }
        public T Resolve<T>(params Parameter[] parameters)
        {
            return Container.Resolve<T>(parameters);
        }
        public object ResolveOptional(Type serviceType, params Parameter[] parameters)
        {
            return Container.ResolveOptional(serviceType, parameters);
        }

        public TService ResolveOptional<TService>(params Parameter[] parameters) where TService : class
        {
            return Container.ResolveOptional<TService>(parameters);
        }
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
