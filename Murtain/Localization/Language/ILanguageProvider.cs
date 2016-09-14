using Murtain.Localization.Language;
using System.Collections.Generic;

namespace Murtain.Localization
{
    public interface ILanguageProvider
    {
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}