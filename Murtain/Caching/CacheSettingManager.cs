using Murtain.Caching;
using Murtain.Caching.Configuration;
using Murtain.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Caching
{
    public class CacheSettingManager : ICacheSettingManager
    {

        private readonly ICacheSettingsConfiguration settingsConfiguration;
        private readonly Dictionary<string, CacheSetting> settings = new Dictionary<string, CacheSetting>();

        public CacheSettingManager(ICacheSettingsConfiguration settingsConfiguration)
        {
            this.settingsConfiguration = settingsConfiguration;

            foreach (var providerType in settingsConfiguration.Providers)
            {
                var provider = CreateProvider(providerType);
                foreach (var cacheSetting in provider.GetCacheSettings())
                {
                    this.settings[cacheSetting.Name] = cacheSetting;
                }
            }
        }

        public Task<IEnumerable<CacheSetting>> GetAllSettingAsync()
        {
            return Task.FromResult(this.settings.Values.AsEnumerable());
        }

        public Task<CacheSetting> GetSettingAsync(string name)
        {
            CacheSetting cacheSetting;
            if (this.settings.TryGetValue(name, out cacheSetting))
            {
                return Task.FromResult(cacheSetting);
            };

            return null;
        }

        public Task<TimeSpan?> GetSettingTimeSpanAsync(string name)
        {
            CacheSetting cacheSetting;
            if (this.settings.TryGetValue(name, out cacheSetting))
            {
                return Task.FromResult(cacheSetting.ExpiredTime);
            };

            return null;
        }

        public Task<string> GetSettingValueAsync(string name)
        {
            CacheSetting cacheSetting;
            if (this.settings.TryGetValue(name, out cacheSetting))
            {
                return Task.FromResult(cacheSetting.Value);
            };

            return null;
        }

        private CacheSettingProvider CreateProvider(Type providerType)
        {
            IocManager.Container.RegisterIfNot(providerType);
            return (CacheSettingProvider)(IocManager.Container.Resolve(providerType));
        }
    }
}
