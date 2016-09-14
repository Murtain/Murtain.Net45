using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.RedisCache.Lock
{

    public interface IRedisLockManager
    {
        /// <summary>
        /// 如果锁空闲立即返回 获取失败一直等待
        /// </summary>
        /// <param name="key"></param>
        void Lock(string key,int expired = 10);
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        void UnLock(string key);
    }
}
