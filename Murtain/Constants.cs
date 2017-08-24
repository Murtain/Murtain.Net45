using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain
{
    internal class Constants
    {

        public static class Localization
        {
            public const string DefaultLanguageName = "Localization.DefaultLanguageName";
            public const string DefaultLanguageDisplayName = "Localization.DefaultLanguageDisplayName";
            public const string DefaultLocalizationXmlSources = "Localization.Sources.XmlSource";

        }

        public static class Settings
        {
            public static class Email
            {
                public const string Host = "Settings.Email.SMTP";
                public const string UserName = "Settings.Email.UserName";
                public const string Password = "Settings.Email.Password";
                public const string Port = "Settings.Email.Port";
                public const string Domain = "Settings.Email.Domain";
                public const string EnableSsl = "Settings.Email.EnableSsl";
                public const string UseDefaultCredentials = "Settings.Email.UseDefaultCredentials";
                public const string DefaultFromAddress = "Settings.Email.DefaultFromAddress";
                public const string DefaultFromDisplayName = "Settings.Email.DefaultFromDisplayName";
            }
        }

        public static class CacheNames
        {
            public const string LocalizationText = "Murtain:LOCALIZATION:LOCALIZATION_TEXT:";
            public const string Language = "Murtain:LOCALIZATION:LANGUAGE:";
            public const string GlobalSettings = "Murtain:GLOBAL_SETTINGS:APPLICATION";
        }
        public static class Crypto
        {
            public const string CryptoString = "npxywcut";
        }

    }
}
