using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Caching
{
    public interface ICacheSettingProvider
    {
        IEnumerable<CacheSetting> GetCacheSettings();
    }
}
