using Murtain.Domain.Services;
using Murtain.OAuth2.SDK.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Requests.Captcha
{
    /// <summary>
    /// 发送短信
    /// </summary>
    public class MessageCaptchaSendRequest
    {
        /// <summary>
        /// 短信验证码类型
        /// </summary>
        public MessageCaptcha Captcha { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        public string Telphone { get; set; }
        /// <summary>
        /// 失效时间（分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
    }
    /// <summary>
    /// 发送短信
    /// </summary>
    public class MessageCaptchaSendResponse : ResponseBase
    {
    }
}
