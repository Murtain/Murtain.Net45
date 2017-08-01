using Murtain.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.GlobalSettings.Provider
{
    public class GlobalSettingCacheProvider : CacheSettingProvider
    {
        public override IEnumerable<CacheSetting> GetCacheSettings()
        {
            return new[] {
                new CacheSetting {
                    Name = Constants.CacheNames.GlobalSettings,
                    Value = "APPLICATION:GLOBAL_SETTINGS",
                    Description = "全局配置缓存",
                    Group = "全局配置"
                },
                new CacheSetting {
                    Name = Constants.CacheNames.Language,
                    Value = "APPLICATION:LOCALIZATION:LANGUAGE",
                    Description = "本地化语言缓存",
                    Group = "本地化"
                },
                new CacheSetting {
                    Name = Constants.CacheNames.LocalizationText,
                    Value = "APPLICATION:LOCALIZATION:LOCALIZATION_TEXT:",
                    Description = "本地化资源缓存",
                    Group = "本地化"
                }
            };
        }
    }
}
