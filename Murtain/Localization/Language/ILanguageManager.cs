using System.Collections.Generic;
using Murtain.Dependency;
namespace Murtain.Localization.Language
{
    public interface ILanguageManager
    {
        LanguageInfo CurrentLanguage { get; }

        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}