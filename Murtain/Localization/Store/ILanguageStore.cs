using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Store
{
    public interface ILanguageStore
    {
        /// <summary>
        /// Gets a Language or null.
        /// </summary>
        /// <param name="name">Name of the Language</param>
        /// <returns>LanguageText object</returns>
        Task<Models.Language> GetLanguageAsync(string name);

        /// <summary>
        /// Adds a Language.
        /// </summary>
        /// <param name="setting">Language to add</param>
        Task<Models.Language> AddOrUpdateLanguageAsync(Models.Language language);

        /// <summary>
        /// Deletes a Language.
        /// </summary>
        /// <param name="name">Name of the Language</param>
        Task<Models.Language> DeleteLanguageAsync(string name);

        /// <summary>
        /// Gets a list of Language.
        /// </summary>
        /// <returns>List of Language</returns>
        Task<List<Models.Language>> GetAllLanguagesAsync();
    }
}
