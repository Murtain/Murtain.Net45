using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.GlobalSettings.Provider;
using Murtain.GlobalSettings.Models;
using Murtain.OAuth2.Core;

namespace Murtain.OAuth2.Core.SettingProviders
{
    public class AuthorizationSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
                   {
                       new GlobalSetting {Name = Constants.Settings.Authorization.Domain,DisplayName = "认证服务器地址",Value = "127.0.0.1",Group =  "授权管理", Scope = GlobalSettingScope.Application ,Description = "认证服务器地址"},
                       new GlobalSetting {Name = Constants.Settings.Authorization.CaptchaAddress ,DisplayName = "图片验证码地址",Value = "https://localhost:44373/Captcha/GetCaptcha",Group =  "授权管理", Scope = GlobalSettingScope.Application ,Description = "图片验证码地址"},
                   };
        }
    }
}
