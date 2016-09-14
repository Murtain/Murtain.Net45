using Murtain.Collections;
using Murtain.GlobalSettings.Provider;
using System.Collections.Generic;

namespace Murtain.GlobalSettings.Configuration
{
    /// <summary>
    /// Used to configure setting system.
    /// </summary>
    public class GlobalSettingsConfiguration : IGlobalSettingsConfiguration
    {
        public string CacheName { get; set; }
        public ITypeList<GlobalSettingsProvider> Providers { get; private set; }

        public GlobalSettingsConfiguration()
        {
            CacheName = Constants.CacheNames.GlobalSettings;
            Providers = new TypeList<GlobalSettingsProvider>();
        }
    }
}