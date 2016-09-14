using System.Collections.Generic;
using Murtain.GlobalSettings.Provider;
using Murtain.Collections;
using Murtain.Dependency;

namespace Murtain.GlobalSettings.Configuration
{
    /// <summary>
    /// Used to configure setting system.
    /// </summary>
    public interface IGlobalSettingsConfiguration
    {
        /// <summary>
        /// key of cache
        /// </summary>
        string CacheName { get; set; }

        /// <summary>
        /// List of settings providers.
        /// </summary>
        ITypeList<GlobalSettingsProvider> Providers { get; }
    }
}
