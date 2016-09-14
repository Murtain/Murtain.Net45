using Castle.Core.Logging;
using Castle.DynamicProxy;
using Murtain.RedisCache.Lock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.RedisCache.ConventionalRegistas
{
    public class RedisLockInterceptor : IInterceptor
    {
        public ILogger Logger { get; set; }

        private IRedisLockManager redisLockManager;
        private string key;
        private int expired;
        public RedisLockInterceptor(IRedisLockManager redisLockManager)
        {
            this.Logger = NullLogger.Instance;
            this.redisLockManager = redisLockManager;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!invocation.MethodInvocationTarget.IsDefined(typeof(RedisLockAttribute), false))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                object[] atts = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(RedisLockAttribute), false);
                if (atts.Length == 1)
                {
                    RedisLockAttribute redisLockAttribute = atts[0] as RedisLockAttribute;

                    this.key = redisLockAttribute.Key;
                    this.expired = redisLockAttribute.Expired;

                    redisLockManager.Lock(this.key, this.expired);
                    invocation.Proceed();
                }
            }
            catch (Exception e)
            {
                Logger.Error("[Redis分布式锁]异常：" + e.Message);
                throw e;
            }
            finally
            {
                redisLockManager.UnLock(this.key);
            }
        }
    }
}
