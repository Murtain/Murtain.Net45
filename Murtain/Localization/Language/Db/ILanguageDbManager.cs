using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Language.Db
{
    /// <summary>
    /// Manages host and tenant languages.
    /// </summary>
    public interface ILanguageDbManager
    {
        /// <summary>
        /// Gets list of all languages available 
        /// </summary>
        Task<IReadOnlyList<Models.Language>> GetLanguagesAsync();

        /// <summary>
        /// Gets the default language or null for the host.
        /// </summary>
        Task<Models.Language> GetDefaultLanguageOrNullAsync();

        /// <summary>
        /// Adds a new language.
        /// </summary>
        /// <param name="language">The language.</param>
        Task AddAsync(Models.Language language);

        /// <summary>
        /// Deletes a language.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        Task RemoveAsync(string languageName);

        /// <summary>
        /// Updates a language.
        /// </summary>
        /// <param name="language">The language to be updated</param>
        Task UpdateAsync(Models.Language language);

        /// <summary>
        /// Sets the default language for the host.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        Task SetDefaultLanguageAsync(string languageName);
    }
}
