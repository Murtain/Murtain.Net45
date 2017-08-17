using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;

namespace Murtain.Dependency
{
    /// <summary>
    /// This interface is used to directly perform dependency injection tasks.
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// Reference to the Autofac Container.
        /// </summary>
        IContainer Container { get;}
    }
}
