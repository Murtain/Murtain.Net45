using Murtain.Dependency;
using Murtain.Extensions;
using Murtain.GlobalSettings;
using Murtain.GlobalSettings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Net.Mail.Configuration
{
    /// <summary>
    /// Implementation of <see cref="IEmailSettingConfiguration"/> that reads settings
    /// from <see cref="IGlobalSettingManager"/>.
    /// </summary>
    public class EmailSettingConfiguration : IEmailSettingConfiguration
    {
        protected readonly IGlobalSettingManager settingManager;

        public EmailSettingConfiguration(IGlobalSettingManager settingManager)
        {
            this.settingManager = settingManager;
        }

        /// <summary>
        /// SMTP Host name/IP.
        /// </summary>
        public string Host
        {
            get { return GetNotEmptySettingValue(Constants.Settings.Email.Host); }
        }

        /// <summary>
        /// SMTP Port.
        /// </summary>
        public int Port
        {
            get { return settingManager.GetSettingValue(Constants.Settings.Email.Port).TryInt(0); }
        }

        /// <summary>
        /// User name to login to SMTP server.
        /// </summary>
        public string UserName
        {
            get { return GetNotEmptySettingValue(Constants.Settings.Email.UserName); }
        }

        /// <summary>
        /// Password to login to SMTP server.
        /// </summary>
        public string Password
        {
            get { return GetNotEmptySettingValue(Constants.Settings.Email.Password); }
        }

        /// <summary>
        /// Domain name to login to SMTP server.
        /// </summary>
        public string Domain
        {
            get { return settingManager.GetSettingValue(Constants.Settings.Email.Domain); }
        }

        /// <summary>
        /// Is SSL enabled?
        /// </summary>
        public bool EnableSsl
        {
            get { return settingManager.GetSettingValue(Constants.Settings.Email.EnableSsl).TryBool(false); }
        }

        /// <summary>
        /// Use default credentials?
        /// </summary>
        public bool UseDefaultCredentials
        {
            get { return settingManager.GetSettingValue(Constants.Settings.Email.UseDefaultCredentials).TryBool(true); }
        }

        public string DefaultFromAddress
        {
            get { return GetNotEmptySettingValue(Constants.Settings.Email.DefaultFromAddress); }
        }

        public string DefaultFromDisplayName
        {
            get { return settingManager.GetSettingValue(Constants.Settings.Email.DefaultFromDisplayName); }
        }

        /// <summary>
        /// Gets a setting value by checking. Throws <see cref="AbpException"/> if it's null or empty.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        /// <returns>Value of the setting</returns>
        protected string GetNotEmptySettingValue(string name)
        {
            var value = settingManager.GetSettingValue(name);
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception(String.Format("Setting value for '{0}' is null or empty!", name));
            }

            return value;
        }
    }
}
