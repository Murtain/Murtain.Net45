using Murtain.Caching;
using Murtain.Dependency;
using Murtain.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Language.Db
{
    public class LanguageChangedEventHandler : IEventHandler<LanguageChangedEvent>
    {
        private readonly ICacheManager _cacheManager;

        public LanguageChangedEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void HandleEvent(LanguageChangedEvent eventData)
        {
            _cacheManager.Remove(Constants.CacheNames.Language + eventData.LanguageSourceName);
        }

    }
}
