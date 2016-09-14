using System.Collections.Generic;
using Murtain.Dependency;
using Murtain.GlobalSettings.Models;

namespace Murtain.GlobalSettings.Provider
{
    /// <summary>
    /// Inherit this class to define settings for a module/application.
    /// </summary>
    public abstract class GlobalSettingsProvider
    {
        /// <summary>
        /// Gets all setting definitions provided by this provider.
        /// </summary>
        /// <returns>List of settings</returns>
        public abstract IEnumerable<GlobalSetting> GetSettings(GlobalSettingsProviderContext context);
    }
}