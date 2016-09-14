using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Murtain.GlobalSettings.Models;
using Murtain.GlobalSettings.Provider;
using Murtain.OAuth2.Core;

namespace Murtain.OAuth2.Core.SettingProviders
{
    public class ResourcesSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
            {
                new GlobalSetting { Name = Constants.Settings.Resources.Domain,DisplayName = "资源站点根目录", Value = "http://172.30.30.190:9000/",Group = "资源设置" ,Description = "资源站点根目录"}
            };
        }

    }
}