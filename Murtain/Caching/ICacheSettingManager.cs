using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Caching
{
    public interface ICacheSettingManager
    {
        Task<CacheSetting> GetSettingAsync(string name);
        Task<string> GetSettingValueAsync(string name);
        Task<TimeSpan?> GetSettingTimeSpanAsync(string name);
        Task<IEnumerable<CacheSetting>> GetAllSettingAsync();
    }
}
