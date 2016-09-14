using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Enums
{
    /// <summary>
    /// 短信验证码类型
    /// </summary>
    public enum MessageCaptcha
    {
        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        Register = 1,
        /// <summary>
        /// 找回密码
        /// </summary>
        [Description("找回密码")]
        RetrievePassword = 2,
    }

   
}
