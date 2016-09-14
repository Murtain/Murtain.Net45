using Murtain.GlobalSettings.Models;
using Murtain.GlobalSettings.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Net.Mail
{
    public class EmailSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
                   {
                       new GlobalSetting {Name = Constants.Settings.Email.Host,DisplayName = "消息通知邮箱SMTP",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "消息通知邮箱SMTP"},
                       new GlobalSetting {Name = Constants.Settings.Email.UserName ,DisplayName = "发送邮箱账户",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "消息通知邮箱发送邮箱账户"},
                       new GlobalSetting {Name = Constants.Settings.Email.Password  ,DisplayName = "发送邮箱账户密码",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "发送邮箱账户密码"},
                       new GlobalSetting {Name = Constants.Settings.Email.Port,DisplayName = "邮件端口",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "邮件端口"},
                       new GlobalSetting {Name = Constants.Settings.Email.Domain,DisplayName = "域",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "域"},
                       new GlobalSetting {Name = Constants.Settings.Email.EnableSsl,DisplayName = "启用SSL",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "启用SSL"},
                       new GlobalSetting {Name = Constants.Settings.Email.UseDefaultCredentials,DisplayName = "使用默认凭证",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "使用默认凭证"},
                       new GlobalSetting {Name = Constants.Settings.Email.DefaultFromAddress,DisplayName = "发信人地址",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "发信人地址"},
                       new GlobalSetting {Name = Constants.Settings.Email.DefaultFromDisplayName,DisplayName = "发信人姓名",Group =  "邮箱设置", Scope = GlobalSettingScope.Application ,Description = "发信人姓名"},
                   };
        }
    }
}
