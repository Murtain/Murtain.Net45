using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.RedisCache.Lock
{
    /// <summary>
    /// 分布式锁属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class RedisLockAttribute : Attribute
    {
        public string Key { set; get; }
        public int Expired { get; set; }

        public RedisLockAttribute(string key)
        {
            this.Key = key;
        }
        public RedisLockAttribute(string key, int expired)
        {
            this.Key = key;
            this.Expired = expired;
        }
    }
}
