using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.Collections;

namespace Murtain.Caching.Configuration
{
    public class CacheSettingsConfiguration : ICacheSettingsConfiguration
    {
        public ITypeList<CacheSettingProvider> Providers  { get; set; }

        public CacheSettingsConfiguration()
        {
            Providers = new TypeList<CacheSettingProvider>();
        }
    }
}
