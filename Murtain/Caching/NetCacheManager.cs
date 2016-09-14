using Murtain.Dependency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;


namespace Murtain.Caching
{
    public class NetCacheManager : ICacheManager
    {

        System.Web.Caching.Cache provider = null;
        CacheItemRemovedCallback DelegateExpireCallBack = null;

        public NetCacheManager()
        {
            provider = System.Web.HttpRuntime.Cache;
            DelegateExpireCallBack = new CacheItemRemovedCallback(ExpireCallBack);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        object ICacheManager.Get(string key)
        {
            var obj = provider.Get(key);
            return obj;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        T ICacheManager.Get<T>(string key, Func<T> invoker)
        {
            var obj = provider.Get(key);

            if (obj == null)
            {
                if (invoker != null)
                {
                    // 仍未获取到，则执行invoke
                    T invokerResult = invoker();

                    if (invokerResult != null)
                    {
                        return invokerResult;
                    }
                }

                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }

        /// <summary>
        /// 获取多个缓存对象，忽略遗漏的缓存key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        IDictionary<string, object> ICacheManager.MultiGet(IEnumerable<string> keys)
        {
            return MultiGet(keys);
        }

        /// <summary>
        /// 获取多个缓存对象，忽略遗漏的缓存key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        IEnumerable<T> ICacheManager.MultiGet<T>(IEnumerable<string> keys)
        {
            return MultiGet(keys).Select(t => (T)t.Value);
        }

        /// <summary>
        /// 获取多个缓存对象，缓存中遗漏的使用invoker方法补全
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="preKey">缓存key修饰的前缀</param>
        /// <param name="keys">待获取的key列表</param>
        /// <param name="invoker">补全方法</param>
        /// <returns></returns>
        IEnumerable<T> ICacheManager.MultiGet<T>(IEnumerable<string> keys, Func<IEnumerable<string>, IEnumerable<T>> invoker)
        {
            IDictionary<string, object> dict = MultiGet(keys.Distinct());
            IEnumerable<T> hitedT = dict.Select(t => (T)t.Value);

            int keyCount = keys.Count();
            int hitedTCount = hitedT.Count();

            if (keyCount == hitedTCount)
            {
                return hitedT;
            }
            else
            {
                IEnumerable<string> hitedKeys = dict.Select(t => t.Key);
                IEnumerable<string> missedKeys = keys.Where(t => !hitedKeys.Contains(t));

                if (missedKeys.Count() == 0)
                {
                    return hitedT;
                }
                else
                {
                    return hitedT.Concat(invoker(missedKeys));
                }
            }
        }

        /// <summary>
        /// 获取多个缓存对象
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        IDictionary<string, object> MultiGet(IEnumerable<string> keys)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (var key in keys)
            {
                var obj = provider.Get(key);

                if (obj != null)
                {
                    dict.Add(key, obj);
                }
            }

            return dict;
        }

        /// <summary>
        /// 缓存数据，覆盖原有键值
        /// </summary>
        void ICacheManager.Set(string key, object value)
        {
            Set(
                key,
                value,
                System.Web.Caching.Cache.NoAbsoluteExpiration);
        }

        /// <summary> 
        /// 缓存数据，覆盖原有键值
        /// 失效条件：到达了失效时间
        /// </summary>
        void ICacheManager.Set(string key, object value, DateTime invalidatedTime)
        {
            Set(
                key,
                value,
                invalidatedTime);
        }

        /// <summary> 
        /// 缓存数据，覆盖原有键值
        /// 失效条件：到达了失效时间
        /// </summary>
        void ICacheManager.Set(string key, object value, TimeSpan invalidatedSpan)
        {
            Set(
                key,
                value,
                DateTime.Now.Add(invalidatedSpan));
        }

        /// <summary>
        /// 缓存数据，覆盖原有键值
        /// 失效条件：到达了失效时间
        /// </summary>
        void Set(string key, object value, DateTime invalidatedTime)
        {
            if (key.Length == 0 || value == null)
            {
                return;
            }

            provider.Insert(
                key,
                value,
                null,
                invalidatedTime,
                System.Web.Caching.Cache.NoSlidingExpiration,
                CacheItemPriority.High,
                DelegateExpireCallBack);
        }

        /// <summary>
        /// 从缓存中读取value并作出修改后，将value重新存入缓存
        /// 失效条件：无
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="invoker"></param>
        T ICacheManager.Modify<T>(string key, Func<T, T> invoker)
        {
            return Modify<T>(key, invoker, System.Web.Caching.Cache.NoAbsoluteExpiration);
        }

        /// <summary>
        /// 从缓存中读取value并作出修改后，将value重新存入缓存
        /// 失效条件：到达了失效时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="invoker"></param>
        T ICacheManager.Modify<T>(string key, Func<T, T> invoker, DateTime expireAt)
        {
            return Modify<T>(key, invoker, expireAt);
        }

        /// <summary>
        /// 从缓存中读取value并作出修改后，将value重新存入缓存
        /// 失效条件：约定时间内没有被访问
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="invoker"></param>
        T ICacheManager.Modify<T>(string key, Func<T, T> invoker, TimeSpan validFor)
        {
            return Modify<T>(key, invoker, DateTime.Now.Add(validFor));
        }

        /// <summary>
        /// 从缓存中读取value并作出修改后，将value重新存入缓存
        /// 失效条件：到达了失效时间 或 约定时间内没有被访问
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="invoker"></param>
        T Modify<T>(string key, Func<T, T> invoker, DateTime expireAt)
        {
            if (key.Length == 0)
            {
                return default(T);
            }

            lock (key)
            {
                var get = provider.Get(key);

                if (get == null)
                {
                    return default(T);
                }

                T value = invoker((T)get);

                provider.Insert(
                    key,
                    value,
                    null,
                    expireAt,
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    CacheItemPriority.High,
                    DelegateExpireCallBack);
                return value;
            }
        }

        /// <summary>
        /// 获取缓存，如果不存在，则缓存invoker的执行结果
        /// </summary>
        T ICacheManager.Retrive<T>(string key, Func<T> invoker)
        {
            return Retrive<T>(
                key,
                invoker,
                System.Web.Caching.Cache.NoAbsoluteExpiration);
        }

        /// <summary>
        /// 获取缓存，如果不存在，则缓存invoker的执行结果
        /// 失效条件：到达了失效时间
        /// </summary>
        T ICacheManager.Retrive<T>(string key, Func<T> invoker, DateTime invaliddatedTime)
        {
            return Retrive<T>(
                key,
                invoker,
                invaliddatedTime);
        }

        /// <summary>
        /// 获取缓存，如果不存在，则缓存invoker的执行结果
        /// 失效条件：到达了失效时间
        /// </summary>
        T ICacheManager.Retrive<T>(string key, Func<T> invoker, TimeSpan invalidatedSpan)
        {
            return Retrive<T>(
                key,
                invoker,
                DateTime.Now.Add(invalidatedSpan));
        }

        /// <summary>
        /// 获取缓存，如果不存在，则缓存invoker的执行结果
        /// 失效条件：到达了失效时间
        /// </summary>
        T Retrive<T>(string key, Func<T> invoker, DateTime invalidatedTime)
        {
            if (key.Length == 0)
            {
                return invoker();
            }

            var cached = provider.Get(key);

            if (cached == null)
            {
                lock (key)
                {
                    // 再次尝试获取
                    cached = provider.Get(key);

                    if (cached != null)
                    {
                        return (T)cached;
                    }

                    // 仍未获取到，则执行invoke
                    T obj = invoker();

                    if (obj == null)
                    {
                        return default(T);
                    }

                    provider.Insert(
                        key,
                        obj,
                        null,
                        invalidatedTime,
                        System.Web.Caching.Cache.NoSlidingExpiration,
                        CacheItemPriority.High,
                        DelegateExpireCallBack);

                    return obj;
                }
            }
            else
            {
                return (T)cached;
            }
        }

        /// <summary>
        /// 延长缓存寿命
        /// </summary>
        T ICacheManager.Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, DateTime expireAt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 延长缓存寿命
        /// </summary>
        T ICacheManager.Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 移除指定缓存
        /// </summary>
        void ICacheManager.Remove(string key)
        {
            if (key.Length == 0)
            {
                return;
            }

            provider.Remove(key);
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        void ICacheManager.FlushAll()
        {
            IDictionaryEnumerator CacheEnum = provider.GetEnumerator();

            while (CacheEnum.MoveNext())
            {
                provider.Remove(CacheEnum.Key.ToString());
            }
        }

        /// <summary>
        /// callback
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="reason"></param>
        void ExpireCallBack(string key, object obj, System.Web.Caching.CacheItemRemovedReason reason)
        {
            /*
            Utils.WriteSomething(
                "key:" + key + ",  " + 
                "reason:" + (int)reason + ",  " + 
                "create_time:" + CacheFactory.Keys[key].CreateTime.ToString() + ",  " + 
                "expire_time:" + DateTime.Now.ToString());
            */

            // 记录过期次数
        }

        /// <summary>
        /// 只有当key存在时，才进行计数
        /// </summary>
        void ICacheManager.Increment(string key, int delta)
        {
            Increment(
                key,
                null,
                delta,
                System.Web.Caching.Cache.NoAbsoluteExpiration);
        }

        /// <summary>
        /// 只有当key存在时，才进行计数
        /// </summary>
        void ICacheManager.Increment(string key, int delta, DateTime expiresAt)
        {
            Increment(
                key,
                null,
                delta,
                expiresAt);
        }

        /// <summary>
        /// 只有当key存在时，才进行计数
        /// </summary>
        void ICacheManager.Increment(string key, int delta, TimeSpan validFor)
        {
            Increment(
                key,
                null,
                delta,
                DateTime.Now.Add(validFor));
        }

        /// <summary>
        /// 提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        void ICacheManager.Increment(string key, int defaultValue, int delta)
        {
            Increment(
                key,
                defaultValue,
                delta,
                System.Web.Caching.Cache.NoAbsoluteExpiration);
        }

        /// <summary>
        /// 提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        void ICacheManager.Increment(string key, int defaultValue, int delta, DateTime expiresAt)
        {
            Increment(
                key,
                defaultValue,
                delta,
                expiresAt);
        }

        /// <summary>
        /// 提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        void ICacheManager.Increment(string key, int defaultValue, int delta, TimeSpan validFor)
        {
            Increment(
                key,
                defaultValue,
                delta,
                DateTime.Now.Add(validFor));
        }

        /// <summary>
        /// 如果defaultValue为null，则当key对应的缓存值不存在时，不进行插入操作
        /// </summary>
        void Increment(string key, int? defaultValue, int delta, DateTime expiresAt)
        {
            var current = ((ICacheManager)this);
            var cached = current.Get(key);

            if (cached == null)
            {
                if (defaultValue == null)
                {
                    return;
                }
                else
                {
                    cached = (int)defaultValue;
                }
            }
            else
            {
                cached = (int)cached + delta;
            }

            Set(key,
                cached,
                expiresAt);
        }

        /// <summary>
        /// 只有当key存在时，才进行计数
        /// </summary>
        void ICacheManager.Decrement(string key, int delta)
        {
            Decrement(
                key,
                null,
                delta,
                System.Web.Caching.Cache.NoAbsoluteExpiration);
        }

        /// <summary>
        /// 只有当key存在时，才进行计数
        /// </summary>
        void ICacheManager.Decrement(string key, int delta, DateTime expiresAt)
        {
            Decrement(
                key,
                null,
                delta,
                expiresAt);
        }

        /// <summary>
        /// 只有当key存在时，才进行计数
        /// </summary>
        void ICacheManager.Decrement(string key, int delta, TimeSpan validFor)
        {
            Decrement(
                key,
                null,
                delta,
                DateTime.Now.Add(validFor));
        }

        /// <summary>
        /// 提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        void ICacheManager.Decrement(string key, int defaultValue, int delta)
        {
            Decrement(
                key,
                defaultValue,
                delta,
                System.Web.Caching.Cache.NoAbsoluteExpiration);
        }

        /// <summary>
        /// 提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        void ICacheManager.Decrement(string key, int defaultValue, int delta, DateTime expiresAt)
        {
            Decrement(
                key,
                defaultValue,
                delta,
                expiresAt);
        }

        /// <summary>
        /// 提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        void ICacheManager.Decrement(string key, int defaultValue, int delta, TimeSpan validFor)
        {
            Decrement(
                key,
                defaultValue,
                delta,
                DateTime.Now.Add(validFor));
        }

        /// <summary>
        /// 如果defaultValue为null，则当key对应的缓存值不存在时，不进行插入操作
        /// </summary>
        void Decrement(string key, int? defaultValue, int delta, DateTime expiresAt)
        {
            var current = ((ICacheManager)this);
            var cached = current.Get(key);

            if (cached == null)
            {
                if (defaultValue == null)
                {
                    return;
                }
                else
                {
                    cached = (int)defaultValue;
                }
            }
            else
            {
                cached = (int)cached - delta;
                cached = (int)cached > 0 ? cached : 0;
            }

            Set(key,
                cached,
                expiresAt);
        }

    }
}
