using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac.Core;

namespace Murtain.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to resolve dependencies.
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// Gets an object from IOC container.
        /// </summary> 
        /// <param name="serviceType">Type of the object to get</typeparam>
        /// <returns>The object instance</returns>
        object Resolve(Type serviceType, params Parameter[] parameters);
        /// <summary>
        /// Gets an object from IOC container.
        /// </summary> 
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <returns>The object instance</returns>
        T Resolve<T>(params Parameter[] parameters);
        /// <summary>
        /// Retrieve a service from the context, or null if the service is not registered.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="parameters">Parameters for the service.</param>
        /// <returns></returns>
        object ResolveOptional(Type serviceType, params Parameter[] parameters);
        /// <summary>
        /// Retrieve a service from the context, or null if the service is not registered.
        /// </summary>
        /// <typeparam name="TService">The service to resolve.</typeparam>
        /// <param name="parameters">Parameters for the service.</param>
        /// <returns></returns>
        TService ResolveOptional<TService>(params Parameter[] parameters) where TService : class;
    }
}
