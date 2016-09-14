using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        object Get(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="invoker">若缓存不存在，则调用invoker获取结果</param>
        T Get<T>(string key, Func<T> invoker = null);


        /// <summary>
        /// 获取多个缓存对象
        /// </summary>
        IDictionary<string, object> MultiGet(IEnumerable<string> keys);

        /// <summary>
        /// 获取多个缓存对象
        /// </summary>
        IEnumerable<T> MultiGet<T>(IEnumerable<string> keys);

        /// <summary>
        /// 获取多个缓存对象，缓存中遗漏的使用invoker方法补全
        /// </summary>
        IEnumerable<T> MultiGet<T>(IEnumerable<string> keys, Func<IEnumerable<string>, IEnumerable<T>> invoker);

        /// <summary>
        /// 缓存数据，覆盖原有键值
        /// </summary>
        void Set(string key, object value);

        /// <summary> 
        /// 缓存数据，覆盖原有键值
        /// 失效条件：到达了失效时间
        /// </summary>
        void Set(string key, object value, DateTime invalidatedTime);

        /// <summary>
        /// 缓存数据，覆盖原有键值
        /// 失效条件：约定时间内没有被访问
        /// </summary>
        void Set(string key, object value, TimeSpan invalidatedSpan);

        /// <summary>
        /// 从缓存中读取value并作出修改后，将value重新存入缓存，存入value时需进行并发校验
        /// 失效条件：无
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="invoker"></param>
        T Modify<T>(string key, Func<T, T> invoker);

        /// <summary>
        /// 从缓存中读取value并作出修改后，将value重新存入缓存，存入value时需进行并发校验
        /// 失效条件：到达了失效时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="invoker"></param>
        T Modify<T>(string key, Func<T, T> invoker, DateTime expireAt);

        /// <summary>
        /// 从缓存中读取value并作出修改后，将value重新存入缓存，存入value时需进行并发校验
        /// 失效条件：约定时间内没有被访问
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="invoker"></param>
        T Modify<T>(string key, Func<T, T> invoker, TimeSpan validFor);

        /// <summary>
        /// 获取缓存，如果不存在，则缓存invoker的执行结果
        /// </summary>
        T Retrive<T>(string key, Func<T> invoker);

        /// <summary>
        /// 获取缓存，如果不存在，则缓存invoker的执行结果
        /// 失效条件：到达了失效时间
        /// </summary>
        T Retrive<T>(string key, Func<T> invoker, DateTime invalidatedTime);

        /// <summary>
        /// 获取缓存，如果不存在，则缓存invoker的执行结果
        /// 失效条件：约定时间内没有被访问
        /// </summary>
        T Retrive<T>(string key, Func<T> invoker, TimeSpan invalidatedSpan);

        /// <summary>
        /// 延长缓存寿命
        /// </summary>
        T Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, DateTime expireAt);

        /// <summary>
        /// 延长缓存寿命
        /// </summary>
        T Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, TimeSpan validFor);

        /// <summary>
        /// 移除指定缓存
        /// </summary>
        void Remove(string key);

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        void FlushAll();

        #region 为减少并发冲突，在服务端实现计数更新

        /// <summary>
        /// 计数增加，只有当key存在时，才进行计数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="delta">增加数</param>
        void Increment(string key, int delta);

        /// <summary>
        /// 计数增加，只有当key存在时，才进行计数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="delta">增加数</param>
        /// <param name="expiresAt">在指定时间过期</param>
        void Increment(string key, int delta, DateTime expiresAt);

        /// <summary>
        /// 计数增加，只有当key存在时，才进行计数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="delta">增加数</param>
        /// <param name="validFor">有效期</param>
        void Increment(string key, int delta, TimeSpan validFor);

        /// <summary>
        /// 计数增加，提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="delta">增加数</param>
        void Increment(string key, int defaultValue, int delta);

        /// <summary>
        /// 计数增加，提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="delta">增加数</param>
        /// <param name="expiresAt">在指定时间过期</param>
        void Increment(string key, int defaultValue, int delta, DateTime expiresAt);

        /// <summary>
        /// 计数增加，提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="delta">增加数</param>
        /// <param name="validFor">有效期</param>
        void Increment(string key, int defaultValue, int delta, TimeSpan validFor);

        /// <summary>
        /// 计数减少，只有当key存在时，才进行计数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="delta">减少数</param>
        void Decrement(string key, int delta);

        /// <summary>
        /// 计数减少，只有当key存在时，才进行计数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="delta">减少数</param>
        /// <param name="expiresAt">在指定时间过期</param>
        void Decrement(string key, int delta, DateTime expiresAt);

        /// <summary>
        /// 计数减少，只有当key存在时，才进行计数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="delta">减少数</param>
        /// <param name="validFor">有效期</param>
        void Decrement(string key, int delta, TimeSpan validFor);

        /// <summary>
        /// 计数减少，提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="delta">减少数</param>
        void Decrement(string key, int defaultValue, int delta);

        /// <summary>
        /// 计数减少，提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="delta">减少数</param>
        /// <param name="expiresAt">在指定时间过期</param>
        void Decrement(string key, int defaultValue, int delta, DateTime expiresAt);

        /// <summary>
        /// 计数减少，提供默认值，如果key值不存在，则插入默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="delta">减少数</param>
        /// <param name="validFor">有效期</param>
        void Decrement(string key, int defaultValue, int delta, TimeSpan validFor);

        #endregion
    }
}
