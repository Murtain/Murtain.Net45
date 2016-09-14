using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Castle.Core.Logging;

using Murtain.Localization.Configuration;
using Murtain.Localization.Dictionaries;
using Murtain.Localization.Sources;
using Murtain.Localization.Language;

using Murtain.Dependency;

namespace Murtain.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        public ILogger Logger { get; set; }

        public LanguageInfo CurrentLanguage { get { return _languageManager.CurrentLanguage; } }

        private readonly ILanguageManager _languageManager;
        private readonly ILocalizationConfiguration _configuration;
        private readonly IDictionary<string, ILocalizationSource> _sources;

        public LocalizationManager(
            ILanguageManager languageManager,
            ILocalizationConfiguration configuration)
        {
            Logger = NullLogger.Instance;
            _languageManager = languageManager;
            _configuration = configuration;
            _sources = new Dictionary<string, ILocalizationSource>();

            InitializeSources();
        }

        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _languageManager.GetLanguages();
        }

        public ILocalizationSource GetSource(string name)
        {
            if (!_configuration.IsEnabled)
            {
                return NullLocalizationSource.Instance;
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            ILocalizationSource source;
            if (!_sources.TryGetValue(name, out source))
            {
                throw new Exception("Can not find a source with name: " + name);
            }

            return source;
        }

        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _sources.Values.ToImmutableList();
        }

        private void InitializeSources()
        {
            if (!_configuration.IsEnabled)
            {
                return;
            }

            foreach (var source in _configuration.Sources)
            {
                if (_sources.ContainsKey(source.Name))
                {
                    throw new Exception("There are more than one source with name: " + source.Name + "! Source name must be unique!");
                }

                _sources[source.Name] = source;
                source.Initialize(_configuration);

                //Extending dictionaries
                if (source is IDictionaryBasedLocalizationSource)
                {
                    var dictionaryBasedSource = source as IDictionaryBasedLocalizationSource;
                    var extensions = _configuration.Sources.Extensions.Where(e => e.SourceName == source.Name).ToList();
                    foreach (var extension in extensions)
                    {
                        extension.DictionaryProvider.Initialize(source.Name);
                        foreach (var extensionDictionary in extension.DictionaryProvider.Dictionaries.Values)
                        {
                            dictionaryBasedSource.Extend(extensionDictionary);
                        }
                    }
                }
            }
        }
    }
}