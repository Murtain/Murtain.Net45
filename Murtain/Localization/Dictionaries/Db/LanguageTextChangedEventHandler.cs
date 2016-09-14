using Murtain.Caching;
using Murtain.Dependency;
using Murtain.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Dictionaries.Db
{
    public class LanguageTextChangedEventHandler : IEventHandler<LanguageTextChangedEvent>
    {
        private readonly ICacheManager _cacheManager;

        public LanguageTextChangedEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void HandleEvent(LanguageTextChangedEvent eventData)
        {
            _cacheManager.Remove(Constants.CacheNames.LocalizationText + eventData.LanguageTextSourceName);
        }

    }
}
