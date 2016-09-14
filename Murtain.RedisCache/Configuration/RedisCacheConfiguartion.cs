using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.RedisCache.Configuration
{
    public class RedisCacheConfiguartion : IRedisCacheConfiguartion
    {
        public string NameOrConnectionString { get; set; }
        public string RedisLockNameOrConnectionString { get; set; }

    }
}
