using System.Collections.Generic;
using System.Collections.Immutable;

using Murtain.Localization.Configuration;
using Murtain.Dependency;
using Murtain.Localization.Language;

namespace Murtain.Localization
{
    public class LanguageProvider : ILanguageProvider
    {
        private readonly ILocalizationConfiguration _configuration;

        public LanguageProvider(ILocalizationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }
    }
}