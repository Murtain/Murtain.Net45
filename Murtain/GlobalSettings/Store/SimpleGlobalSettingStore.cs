using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Murtain.Logging;
using Murtain.GlobalSettings.Models;

namespace Murtain.GlobalSettings.Store
{
    /// <summary>
    /// Implements default behavior for ISettingStore.
    /// Only <see cref="GetSettingAsync"/> method is implemented and it gets setting's value
    /// from application's configuration file if exists, or returns null if not.
    /// </summary>
    public class SimpleGlobalSettingStore : IGlobalSettingStore
    {
        public SimpleGlobalSettingStore()
        {
        }

        public Task<GlobalSetting> GetSettingAsync(string name)
        {
            return Task.FromResult(new Models.GlobalSetting { Name = name, Value = ConfigurationManager.AppSettings[name] });
        }
        public Task DeleteSettingAsync(string name)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, SimpleSettingStore does not support DeleteSettingAsync.");
            throw new NotImplementedException();
        }

        public Task AddOrUpdateSettingAsync(GlobalSetting setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, SimpleSettingStore does not support AddOrUpdateSettingAsync.");
            throw new NotImplementedException();
        }

        public Task<List<GlobalSetting>> GetAllSettingsAsync()
        {
            var settings = new List<GlobalSetting>();
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                settings.Add(new Models.GlobalSetting { Name = key, Value = ConfigurationManager.AppSettings[key] });
            }
            return Task.FromResult(settings);
        }
    }
}