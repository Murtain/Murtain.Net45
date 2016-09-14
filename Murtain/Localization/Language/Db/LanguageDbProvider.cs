using Murtain.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Language.Db
{
    /// <summary>
    /// Implements <see cref="ILanguageProvider"/> to get languages from <see cref="IApplicationLanguageManager"/>.
    /// </summary>
    public class LanguageDbProvider : ILanguageProvider
    {

        private readonly ILanguageDbManager _languageDbManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LanguageDbProvider(ILanguageDbManager languageDbManager)
        {
            _languageDbManager = languageDbManager;

        }
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            var languageInfos = AsyncHelper.RunSync(() => _languageDbManager.GetLanguagesAsync())
                    .OrderBy(l => l.DisplayName)
                    .Select(l => l.MapToLanguageInfo())
                    .ToList();

            SetDefaultLanguage(languageInfos);

            return languageInfos;
        }

        private void SetDefaultLanguage(List<LanguageInfo> languageInfos)
        {
            if (languageInfos.Count <= 0)
            {
                return;
            }

            var defaultLanguage = AsyncHelper.RunSync(() => _languageDbManager.GetDefaultLanguageOrNullAsync());
            if (defaultLanguage == null)
            {
                languageInfos[0].IsDefault = true;
                return;
            }

            var languageInfo = languageInfos.FirstOrDefault(l => l.Name == defaultLanguage.Name);
            if (languageInfo == null)
            {
                languageInfos[0].IsDefault = true;
                return;
            }

            languageInfo.IsDefault = true;
        }
    }
}
