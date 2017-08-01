using Murtain.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Caching.Extensions
{
    public static class CacheManagerExtensions
    {
        /// <summary>
        /// 获取缓存，如果不存在，则缓存invoker的执行结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager">cacheManager</param>
        /// <param name="name">缓存配置名称</param>
        /// <param name="invoker">invoker</param>
        /// <returns></returns>
        public static T TryRetrive<T>(this ICacheManager cacheManager, string name, Func<T> invoker, params string[] format)
        {
            var cacheSettingManager = IocManager.Container.Resolve<ICacheSettingManager>();

            var key = cacheSettingManager.GetSettingValue(name);

            if (format.Length > 0)
            {
                key = string.Format(key, format);
            }

            var invalideTimeSpan = cacheSettingManager.GetSettingTimeSpan(name);

            if (invalideTimeSpan == null)
            {
                return cacheManager.Retrive<T>(key, invoker);
            }
            else
            {
                return cacheManager.Retrive<T>(key, invoker, invalideTimeSpan.Value);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager">cacheManager</param>
        /// <param name="name">缓存配置名称</param>
        /// <returns></returns>
        public static T TryGet<T>(this ICacheManager cacheManager, string name, params string[] format)
        {
            var cacheSettingManager = IocManager.Container.Resolve<ICacheSettingManager>();

            var key = cacheSettingManager.GetSettingValue(name);

            if (format.Length > 0)
            {
                key = string.Format(key, format);
            }

            return cacheManager.Get<T>(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager">cacheManager</param>
        /// <param name="name">缓存配置名称</param>
        /// <returns></returns>
        public static void TrySet(this ICacheManager cacheManager, string name, string value, params string[] format)
        {
            var cacheSettingManager = IocManager.Container.Resolve<ICacheSettingManager>();

            var key = cacheSettingManager.GetSettingValue(name);

            if (format.Length > 0)
            {
                key = string.Format(key, format);
            }
            var invalideTimeSpan = cacheSettingManager.GetSettingTimeSpan(name);

            cacheManager.Set(key, value, invalideTimeSpan.Value);
        }
    }
}
