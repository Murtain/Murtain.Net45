using Autofac;
using Autofac.Core;
using Murtain.Caching;
using Murtain.Dependency;
using Murtain.Extensions;
using Murtain.Localization.Dictionaries.Xml;
using Murtain.Localization.Language;
using Murtain.Localization.Language.Db;
using Murtain.Localization.Store;
using Murtain.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Dictionaries.Db
{
    /// <summary>
    /// Extends <see cref="ILocalizationDictionaryProvider"/> to add tenant and database based localization.
    /// </summary>
    public class DbLocalizationDictionaryProvider : ILocalizationDictionaryProvider
    {
        public ILocalizationDictionary DefaultDictionary
        {
            get { return GetDefaultDictionary(); }
        }

        public IDictionary<string, ILocalizationDictionary> Dictionaries
        {
            get { return GetDictionaries(); }
        }

        private readonly ConcurrentDictionary<string, ILocalizationDictionary> _dictionaries;

        private string _sourceName;

        private readonly ILocalizationDictionaryProvider _internalProvider;

        private ILanguageDbManager _languageDbManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbLocalizationDictionaryProvider"/> class.
        /// </summary>
        public DbLocalizationDictionaryProvider(ILocalizationDictionaryProvider internalProvider = null)
        {
            _internalProvider = internalProvider ?? new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), Constants.Localization.DefaultLocalizationXmlSources
                        );
            _dictionaries = new ConcurrentDictionary<string, ILocalizationDictionary>();
        }

        public void Initialize(string sourceName)
        {
            _sourceName = sourceName;
            _languageDbManager = IocManager.Instance.Resolve<ILanguageDbManager>();
            _internalProvider.Initialize(_sourceName);
        }

        protected virtual IDictionary<string, ILocalizationDictionary> GetDictionaries()
        {
            var languages = AsyncHelper.RunSync(()=> _languageDbManager.GetLanguagesAsync());

            foreach (var language in languages)
            {
                _dictionaries.GetOrAdd(language.Name, s => CreateLocalizationDictionary(language.MapToLanguageInfo()));
            }

            return _dictionaries;
        }

        protected virtual ILocalizationDictionary GetDefaultDictionary()
        {
            var defaultLanguage = AsyncHelper.RunSync(() => _languageDbManager.GetLanguagesAsync()).FirstOrDefault(l => l.MapToLanguageInfo().IsDefault);
            if (defaultLanguage == null)
            {
                throw new ApplicationException("Default language is not defined!");
            }

            return _dictionaries.GetOrAdd(defaultLanguage.Name, s => CreateLocalizationDictionary(defaultLanguage.MapToLanguageInfo()));
        }

        protected virtual ILocalizationDictionary CreateLocalizationDictionary(LanguageInfo language)
        {
            var internalDictionary =
                _internalProvider.Dictionaries.GetOrDefault(language.Name) ??
                new EmptyInternalDictionary(CultureInfo.GetCultureInfo(language.Name));

            var dictionary = IocManager.Instance.Resolve<ILocalizationDictionary>(
                    new NamedParameter("sourceName", _sourceName),
                    new NamedParameter("internalDictionary", internalDictionary)
                );

            return dictionary;
        }

        public virtual void Extend(ILocalizationDictionary dictionary)
        {
            //Add
            ILocalizationDictionary existingDictionary;
            if (!_internalProvider.Dictionaries.TryGetValue(dictionary.CultureInfo.Name, out existingDictionary))
            {
                _internalProvider.Dictionaries[dictionary.CultureInfo.Name] = dictionary;
                return;
            }

            //Override
            var localizedStrings = dictionary.GetAllStrings();
            foreach (var localizedString in localizedStrings)
            {
                existingDictionary[localizedString.Name] = localizedString.Value;
            }
        }
    }

    internal class EmptyInternalDictionary : ILocalizationDictionary
    {
        public CultureInfo CultureInfo { get; private set; }

        public EmptyInternalDictionary(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
        }

        public LocalizedString GetOrNull(string name)
        {
            return null;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings()
        {
            return new LocalizedString[0];
        }

        public string this[string name]
        {
            get { return null; }
            set { }
        }
    }
}
