using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Murtain.Runtime.Cookie
{
    public static class CookieManager
    {
        /// <summary>
        /// 创建cookie
        /// </summary>
        /// <param name="name">cookie名</param>
        /// <param name="value">cookie值</param>
        /// <param name="expires">过期时间</param>
        public static void CreateCookie(string name, string value, DateTime expires)
        {
            HttpCookie hc = value.Trim() == "" ? new HttpCookie(name) : new HttpCookie(name, value.Trim());
            hc.Path = "/";

            // 当过期时间为MinValue时，不设置cookie的过期时间，也就是说，关闭浏览器后cookie即过期
            if (expires != DateTime.MinValue)
            {
                hc.Expires = expires;
            }

            HttpContext.Current.Response.Cookies.Set(hc);
        }

        /// <summary>
        /// 清除cookie
        /// </summary>
        /// <param name="name">cookie名</param>
        /// <param name="domain">域名</param>
        public static void ClearCookie(string name)
        {
            CreateCookie(name, "", DateTime.Now.AddDays(-1));
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCookieValue(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];

            if (cookie == null)
            {
                return null;
            }
            else
            {
                return cookie.Value;
            }
        }
    }
}
