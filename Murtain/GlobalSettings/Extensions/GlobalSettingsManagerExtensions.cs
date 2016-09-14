using System.Collections.Generic;
using System.Threading.Tasks;
using Murtain.Extensions;
using Murtain.Threading;

namespace Murtain.GlobalSettings.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IGlobalSettingManager"/>.
    /// </summary>
    public static class GlobalSettingsManagerExtensions
    {
        /// <summary>
        /// Gets current value of a setting.
        /// It gets the setting value, overwritten by application and the current user if exists.
        /// </summary>
        /// <param name="settingManager">Setting manager</param>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Current value of the setting</returns>
        public static string GetSettingValue(this IGlobalSettingManager settingManager, string name)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueAsync(name));
        }
    }
}