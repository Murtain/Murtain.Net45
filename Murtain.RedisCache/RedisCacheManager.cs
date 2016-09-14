using Murtain.Caching;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.RedisCache
{
    public class RedisCacheManager : ICacheManager
    {
        public IDatabase Database { get; set; }
        public ConnectionMultiplexer Connection { get; set; }
        public RedisCacheManager(string configuration)
        {
            try
            {
                Connection = ConnectionMultiplexer.Connect(configuration);
                Database = Connection.GetDatabase();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public object Get(string key)
        {
            return Database.StringGet(key);
        }

        public T Get<T>(string key, Func<T> invoker = null)
        {
            if (Database.KeyExists(key))
            {
                return JsonConvert.DeserializeObject<T>(Database.StringGet(key));
            }
            return default(T);
        }

        public IDictionary<string, object> MultiGet(IEnumerable<string> keys)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (var key in keys)
            {
                var obj = Get(key);

                if (obj != null)
                {
                    dict.Add(key, obj);
                }
            }
            return dict;
        }

        public IEnumerable<T> MultiGet<T>(IEnumerable<string> keys)
        {
            return MultiGet(keys).Select(t => (T)t.Value);
        }

        public IEnumerable<T> MultiGet<T>(IEnumerable<string> keys, Func<IEnumerable<string>, IEnumerable<T>> invoker)
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

        public void Set(string key, object value)
        {
            Database.StringSet(key, JsonConvert.SerializeObject(value));
        }

        public void Set(string key, object value, DateTime invalidatedTime)
        {
            Database.StringSet(key, JsonConvert.SerializeObject(value), invalidatedTime - DateTime.Now);
        }

        public void Set(string key, object value, TimeSpan invalidatedSpan)
        {
            Database.StringSet(key, JsonConvert.SerializeObject(value), invalidatedSpan);
        }

        public T Modify<T>(string key, Func<T, T> invoker)
        {
            if (key.Length == 0)
            {
                return default(T);
            }

            lock (key)
            {
                var get = Get(key);

                if (get == null)
                {
                    return default(T);
                }

                T value = invoker((T)get);

                Set(key, value);
                return value;
            }
        }

        public T Modify<T>(string key, Func<T, T> invoker, DateTime expireAt)
        {
            if (key.Length == 0)
            {
                return default(T);
            }

            lock (key)
            {
                var get = Get(key);

                if (get == null)
                {
                    return default(T);
                }

                T value = invoker((T)get);

                Set(key, value, expireAt);
                return value;
            }
        }

        public T Modify<T>(string key, Func<T, T> invoker, TimeSpan validFor)
        {
            if (key.Length == 0)
            {
                return default(T);
            }

            lock (key)
            {
                var get = Get(key);

                if (get == null)
                {
                    return default(T);
                }

                T value = invoker((T)get);

                Set(key, value, validFor);
                return value;
            }
        }

        public T Retrive<T>(string key, Func<T> invoker)
        {
            if (Database.KeyExists(key))
            {
                return Get<T>(key);
            }
            else
            {
                T obj = invoker();
                Set(key, obj);
                return obj;
            }
        }

        public T Retrive<T>(string key, Func<T> invoker, DateTime invalidatedTime)
        {
            if (Database.KeyExists(key))
            {
                return Get<T>(key);
            }
            else
            {
                T obj = invoker();
                Set(key, obj, invalidatedTime);
                return obj;
            }
        }

        public T Retrive<T>(string key, Func<T> invoker, TimeSpan invalidatedSpan)
        {
            if (Database.KeyExists(key))
            {
                return Get<T>(key);
            }
            else
            {
                T obj = invoker();
                Set(key, obj, invalidatedSpan);
                return obj;
            }
        }

        public T Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, DateTime expireAt)
        {
            throw new NotImplementedException();
        }

        public T Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            Database.KeyDelete(key);
        }

        public void FlushAll()
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int delta)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int delta, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int delta, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int defaultValue, int delta)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int defaultValue, int delta, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int defaultValue, int delta, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int delta)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int delta, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int delta, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int defaultValue, int delta)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int defaultValue, int delta, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int defaultValue, int delta, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }
    }
}
