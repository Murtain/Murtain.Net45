using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Castle.Core.Logging;
using Murtain.Dependency;

namespace Murtain.Logging
{
    /// <summary>
    /// This class can be used to write logs from somewhere where it's a hard to get a reference to the <see cref="ILogger"/>.
    /// Normally, use <see cref="ILogger"/> with property injection wherever it's possible.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// A reference to the logger.
        /// </summary>
        public static ILogger Logger { get; private set; }

        static LogHelper()
        {
            Logger = IocManager.Container.IsRegistered(typeof(ILoggerFactory)) ? IocManager.Container.Resolve<ILoggerFactory>().Create(typeof(LogHelper)) : NullLogger.Instance;
        }
    }
}
