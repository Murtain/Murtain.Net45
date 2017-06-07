using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using Murtain.Extensions;
using Murtain.Domain.UnitOfWork;
using Murtain.Caching;
using Murtain.Runtime.Session;
using Murtain.Dependency;
using Murtain.GlobalSettings.Configuration;
using Murtain.GlobalSettings.Store;
using Murtain.GlobalSettings.Provider;
using Murtain.GlobalSettings.Models;
using Murtain.Threading;

namespace Murtain.GlobalSettings
{
    /// <summary>
    /// This class implements <see cref="IGlobalSettingManager"/> to manage setting values in the database.
    /// </summary>
    public class GlobalSettingManager : IGlobalSettingManager
    {
        private IGlobalSettingStore _settingStore;
        private ICacheManager _settingCacheProvider;
        private IGlobalSettingsConfiguration _settingsConfiguration;
        private Dictionary<string, GlobalSetting> _settings;
        private string settingCacheName;

        public GlobalSettingManager(ICacheManager cacheManager, IGlobalSettingsConfiguration settingsConfiguration, IGlobalSettingStore settingStore)
        {
            _settingsConfiguration = settingsConfiguration;
            _settingCacheProvider = cacheManager;
            _settingStore = settingStore;
            _settings = new Dictionary<string, GlobalSetting>();

            var context = new GlobalSettingsProviderContext();
            foreach (var providerType in _settingsConfiguration.Providers)
            {
                var provider = CreateProvider(providerType);
                foreach (var settings in provider.GetSettings(context))
                {
                    _settings[settings.Name] = settings;
                }
            }
            settingCacheName = _settingsConfiguration.CacheName;
        }


        public async Task<GlobalSetting> GetSettingAsync(string name)
        {
            var settings = await GetApplicationSettingsAsync();
            return await Task.FromResult<GlobalSetting>(settings.GetOrDefault(name));
        }

        public async Task<string> GetSettingValueAsync(string name, GlobalSettingScope scope = GlobalSettingScope.Application)
        {
            var setting = await GetSettingAsync(name);
            if (setting != null)
            {
                _settings[name] = setting;
            }
            return await Task.FromResult<string>(_settings[name].Value);
        }

        public async Task<IReadOnlyList<GlobalSetting>> GetAllSettingsAsync()
        {
            foreach (var setting in await GetApplicationSettingsAsync())
            {
                if (_settings.Keys.Contains(setting.Key))
                {
                    _settings[setting.Key].Id = setting.Value.Id;
                    _settings[setting.Key].Name = setting.Value.Name;
                    _settings[setting.Key].Value = setting.Value.Value;
                    _settings[setting.Key].Scope = setting.Value.Scope;


                    if (!string.IsNullOrEmpty(setting.Value.DisplayName))
                    {
                        _settings[setting.Key].DisplayName = setting.Value.DisplayName;
                    }
                    if (!string.IsNullOrEmpty(setting.Value.Group))
                    {
                        _settings[setting.Key].Group = setting.Value.Group;
                    }
                    if (!string.IsNullOrEmpty(setting.Value.Description))
                    {
                        _settings[setting.Key].Description = setting.Value.Description;
                    }
                }
            }
            return await Task.FromResult<IReadOnlyList<GlobalSetting>>(_settings.Values.ToImmutableList());
        }

        public async Task AddOrUpdateSettingAsync(GlobalSetting data)
        {
            await _settingStore.AddOrUpdateSettingAsync(data);
        }

        public async Task DeleteSettingAsync(string name)
        {
            await _settingStore.DeleteSettingAsync(name);
        }

        public async Task ClearGlobalSettingCacheAsync()
        {
            _settingCacheProvider.Remove(settingCacheName);
            await Task.FromResult(true);
        }

        private GlobalSettingsProvider CreateProvider(Type providerType)
        {
            IocManager.Container.RegisterIfNot(providerType);
            return (GlobalSettingsProvider)(IocManager.Container.Resolve(providerType));
        }

        private async Task<Dictionary<string, GlobalSetting>> GetApplicationSettingsAsync()
        {
            var settings = _settingCacheProvider.Retrive(settingCacheName, () =>
             {
                 var dictionary = new Dictionary<string, GlobalSetting>();
                 var allSetting = AsyncHelper.RunSync(() => _settingStore.GetAllSettingsAsync());
                 foreach (var setting in allSetting)
                 {
                     if (_settings.Keys.Contains(setting.Name))
                     {
                         dictionary[setting.Name] = setting;
                     }
                 }
                 return dictionary;
             });
            return await Task.FromResult(settings);
        }
    }
}