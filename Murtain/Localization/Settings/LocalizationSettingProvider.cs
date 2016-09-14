using System.Collections.Generic;
using Murtain.Localization.Configuration;
using Murtain.GlobalSettings.Provider;
using Murtain.GlobalSettings;
using Murtain.GlobalSettings.Models;

namespace Murtain.Localization.Settings
{
    public class LocalizationSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<GlobalSettings.Models.GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
            {
                new GlobalSettings.Models.GlobalSetting { Name = "Settings.Localization.DefaultLanguageName", Value= "zh-CN" , Group = "本地化设置" ,Description = "默认语言" }
            };
        }
    }
}