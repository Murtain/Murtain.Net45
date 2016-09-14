using System.Collections.Generic;
using Murtain.Localization;
using Murtain.Localization.Language;

namespace Murtain.Localization.Configuration
{
    /// <summary>
    /// Used for localization configurations.
    /// </summary>
    internal class LocalizationConfiguration : ILocalizationConfiguration
    {
        /// <inheritdoc/>
        public IList<LanguageInfo> Languages { get; private set; }

        /// <inheritdoc/>
        public ILocalizationSourceList Sources { get; private set; }

        /// <inheritdoc/>
        public bool IsEnabled { get; set; }

        public LocalizationConfiguration()
        {
            Languages = new List<LanguageInfo>();
            Sources = new LocalizationSourceList();

            IsEnabled = true;
        }
    }
}