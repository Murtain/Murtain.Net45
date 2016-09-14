using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Enums
{
    /// <summary>
    /// 邮件验证码类型
    /// </summary>
    public enum EmailCaptcha
    {
        /// <summary>
        /// 绑定
        /// </summary>
        [Description("绑定")]
        Bind = 1,
        /// <summary>
        /// 变更
        /// </summary
        [Description("变更")]
        Change = 2,
    }
}
