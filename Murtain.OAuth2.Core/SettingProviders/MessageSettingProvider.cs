using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Murtain.GlobalSettings.Provider;
using Murtain.GlobalSettings.Models;
using Murtain.OAuth2.Core;

namespace Murtain.OAuth2.Core.SettingProviders
{
    public class MessageSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
            {
                new GlobalSetting { Name = Constants.Settings.Message.MessageContentTemplate,DisplayName = "短信验证码内容模板", Value = "",Group = "短信平台" ,Description = "短信验证码内容模板"},
                new GlobalSetting { Name = Constants.Settings.Message.MessageSeverUrl,DisplayName = "短信平台接口地址", Value = "",Group = "短信平台" ,Description = "短信平台接口地址"},
                new GlobalSetting { Name = Constants.Settings.Message.MessageDeptType,DisplayName = "短信部门代码", Value = "",Group = "短信平台" ,Description = "短信部门代码"},
                new GlobalSetting { Name = Constants.Settings.Message.MessageBesType,DisplayName = "短信业务代码", Value = "",Group = "短信平台" ,Description = "短信业务代码"},
                new GlobalSetting { Name = Constants.Settings.Message.MessageExpiredTime,DisplayName = "短信失效时间(分钟)", Value = "",Group = "短信平台" ,Description = "短信失效时间(分钟)"},
            };
        }

    }
}