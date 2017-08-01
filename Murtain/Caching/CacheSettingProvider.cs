using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Caching
{
    public abstract class CacheSettingProvider : ICacheSettingProvider
    {
        public abstract IEnumerable<CacheSetting> GetCacheSettings();
    }
}
