using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.RedisCache.Configuration
{
    public interface IRedisCacheConfiguartion
    { 
        string NameOrConnectionString { get; set; }
        string RedisLockNameOrConnectionString { get; set; }
    }
}
