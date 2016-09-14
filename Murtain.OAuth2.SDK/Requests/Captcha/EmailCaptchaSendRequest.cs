using Murtain.Domain.Services;
using Murtain.OAuth2.SDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Requests.Captcha
{
    /// <summary>
    /// 发送邮箱验证码
    /// </summary>
    public class EmailCaptchaSendRequest
    {
        /// <summary>
        /// 邮件验证码类型
        /// </summary>
        public EmailCaptcha Captcha { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 失效时间（分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
    }
    /// <summary>
    /// 发送邮箱验证码
    /// </summary>
    public class EmailCaptchaSendResponse : ResponseBase
    {
    }

}
