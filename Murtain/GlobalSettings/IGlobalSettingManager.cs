using Murtain.Dependency;
using Murtain.GlobalSettings.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murtain.GlobalSettings
{
    /// <summary>
    /// This is the main interface that must be implemented to be able to load/change values of settings.
    /// </summary>
    public interface IGlobalSettingManager
    {
        /// <summary>
        /// Gets the <see cref="GlobalSetting"/> object with given unique name.
        /// Throws exception if can not find the setting.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>The <see cref="GlobalSetting"/> object.</returns>
        Task<GlobalSetting> GetSettingAsync(string name);
        /// <summary>
        /// Gets current value of a setting.
        /// It gets the setting value, overwritten by application if exists.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Current value of the setting</returns>
        Task<string> GetSettingValueAsync(string name, GlobalSettingScope scope = GlobalSettingScope.Application);
        /// <summary>
        /// Gets a list of all setting.
        /// </summary>
        /// <returns>All settings.</returns>
        Task<IReadOnlyList<GlobalSetting>> GetAllSettingsAsync();
        /// <summary>
        /// Add Or Update setting for the application level.
        /// </summary>
        /// <param name="data">save data</param>
        /// <returns></returns>
        Task<GlobalSetting> AddOrUpdateSettingAsync(GlobalSetting data);
        /// <summary>
        /// Delete setting by name.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Value of the setting</returns>
        Task<GlobalSetting> DeleteSettingAsync(string name);
        /// <summary>
        /// Clear cache.
        /// </summary>
        Task ClearGlobalSettingCacheAsync();
    }
}
