using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Murtain.RedisCache.Lock
{
    public class RedisLockManager : IRedisLockManager
    {

        private const int DEFAULT_SINGLE_EXPIRE_TIME_SECOND = 10;
        private const int DEFAULT_DB = 0;

        private readonly IDatabase client;
        private readonly ConnectionMultiplexer connection;

        private static readonly object locksync = new object();

        public RedisLockManager(string configuration)
        {
            try
            {
                this.connection = ConnectionMultiplexer.Connect(configuration);
                this.client = connection.GetDatabase(DEFAULT_DB);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 如果锁空闲立即返回 获取失败一直等待
        /// </summary>
        /// <param name="key"></param>
        public void Lock(string key, int expired)
        {
            try
            {
                do
                {
                    //如果锁空闲, 立即返回
                    if (client.LockTake(key, Guid.NewGuid().ToString("N"), TimeSpan.FromSeconds(expired == 0 ? DEFAULT_SINGLE_EXPIRE_TIME_SECOND : expired)))
                    {
                        break;
                    };
                    Thread.Sleep(300);

                } while (true);

            }
            catch (Exception exc)
            {
                throw new Exception("[Redis分布式锁]加锁失败:" + exc.Message, exc);
            }
        }
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        public void UnLock(string key)
        {
            try
            {
                client.KeyDelete(key);
            }
            catch (Exception exc)
            {
                throw new Exception("[Redis分布式锁]释放失败:" + exc.Message, exc);
            }
        }
    }
}
