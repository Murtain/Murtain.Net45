using Murtain.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Runtime.Session
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// </summary>
    public interface IAppSession
    {
        /// <summary>
        /// Gets current UserId or empty.
        /// </summary>
        string UserId { get; }

        string Name { get; }
    }
}
