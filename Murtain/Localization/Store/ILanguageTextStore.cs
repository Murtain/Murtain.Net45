using Murtain.Localization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Store
{
    public interface ILanguageTextStore
    {
        /// <summary>
        /// Gets a LanguageText or null.
        /// </summary>
        /// <param name="name">Name of the LanguageText</param>
        /// <returns>LanguageText object</returns>
        Task<LanguageText> GetLanguageTextAsync(string name);

        /// <summary>
        /// Adds a LanguageText.
        /// </summary>
        /// <param name="setting">LanguageText to add</param>
        Task<LanguageText> AddOrUpdateLanguageTextAsync(LanguageText languageText);

        /// <summary>
        /// Deletes a LanguageText.
        /// </summary>
        /// <param name="name">Name of the LanguageText</param>
        Task<LanguageText> DeleteLanguageTextAsync(string name);

        /// <summary>
        /// Gets a list of LanguageText.
        /// </summary>
        /// <returns>List of LanguageText</returns>
        Task<List<LanguageText>> GetAllLanguageTextsAsync(Expression<Func<LanguageText,bool>> lambda);
    }
}
