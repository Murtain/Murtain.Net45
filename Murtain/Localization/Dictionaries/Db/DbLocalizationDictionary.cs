using Murtain.Caching;
using Murtain.Extensions;
using Murtain.Localization.Models;
using Murtain.Localization.Store;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Dictionaries.Db
{
    public class DbLocalizationDictionary : ILocalizationDictionary 
    {
        private readonly string _sourceName;
        private readonly ILocalizationDictionary _internalDictionary;
        private readonly ILanguageTextStore _languageTextStore;
        private readonly ICacheManager _cacheManager;


        public DbLocalizationDictionary(string sourceName,
            ILocalizationDictionary internalDictionary,
            ILanguageTextStore languageTextStore,
            ICacheManager cacheManager)
        {
            _sourceName = sourceName;
            _internalDictionary = internalDictionary;
            _languageTextStore = languageTextStore;
            _cacheManager = cacheManager;
        }

        public CultureInfo CultureInfo { get { return _internalDictionary.CultureInfo; } }

        public string this[string name]
        {
            get { return _internalDictionary[name]; }
            set { _internalDictionary[name] = value; }
        }

        public LocalizedString GetOrNull(string name)
        {
            var dictionary = GetAllLanguageText();
            var value = dictionary.GetOrDefault(name);
            if (value != null)
            {
                return new LocalizedString(name, value, CultureInfo);
            }
            //Not found in database, fall back to internal dictionary
            var internalLocalizedString = _internalDictionary.GetOrNull(name);
            if (internalLocalizedString != null)
            {
                return internalLocalizedString;
            }
            //Not found at all
            return null;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings()
        {
            var dictionary = new Dictionary<string, LocalizedString>();

            foreach (var localizedString in _internalDictionary.GetAllStrings())
            {
                dictionary[localizedString.Name] = localizedString;
            }

            var defaultDictionary = GetAllLanguageText();
            foreach (var keyValue in defaultDictionary)
            {
                dictionary[keyValue.Key] = new LocalizedString(keyValue.Key, keyValue.Value, CultureInfo);
            }

            return dictionary.Values.ToImmutableList();
        }

        private Dictionary<string, string> GetAllLanguageText()
        {
            return _cacheManager.Retrive<Dictionary<string, string>>(Constants.CacheNames.LocalizationText + _sourceName.ToUpper(), () =>
             {
                 return _languageTextStore
                             .GetAllLanguageTextsAsync(l => l.Source == _sourceName && l.LanguageName == CultureInfo.Name)
                             .Result
                             .ToDictionary(l => l.Key, l => l.Value);
             });
        }
    }
}
