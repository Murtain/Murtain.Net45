using Murtain.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Caching.Extensions
{
    public static class CacheSettingManagerExtensions
    {
        public static string GetSettingValue(this ICacheSettingManager cacheSettingManager, string name)
        {
            return AsyncHelper.RunSync(() => cacheSettingManager.GetSettingValueAsync(name));
        }
        public static TimeSpan? GetSettingTimeSpan(this ICacheSettingManager cacheSettingManager, string name)
        {
            return AsyncHelper.RunSync(() => cacheSettingManager.GetSettingTimeSpanAsync(name));
        }
    }
}
