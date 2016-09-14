using System.Collections.Generic;
using System.Threading.Tasks;

using Murtain.Dependency;
using Murtain.GlobalSettings.Models;

namespace Murtain.GlobalSettings.Store
{
    /// <summary>
    /// This interface is used to get/set settings from/to a data source (database).
    /// </summary>
    public interface IGlobalSettingStore
    {
        /// <summary>
        /// Gets a setting or null.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        /// <returns>Setting object</returns>
        Task<GlobalSetting> GetSettingAsync(string name);

        /// <summary>
        /// Adds a setting.
        /// </summary>
        /// <param name="setting">Setting to add</param>
        Task<GlobalSetting> AddOrUpdateSettingAsync(GlobalSetting setting);

        /// <summary>
        /// Deletes a setting.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        Task<GlobalSetting> DeleteSettingAsync(string name);

        /// <summary>
        /// Gets a list of setting.
        /// </summary>
        /// <returns>List of settings</returns>
        Task<List<GlobalSetting>> GetAllSettingsAsync();
    }
}