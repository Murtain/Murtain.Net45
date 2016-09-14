using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Murtain.Domain.Services;

namespace Murtain.OAuth2.SDK.Requests.UserAccount
{
    /// <summary>
    /// 使用手机号注册
    /// </summary>
    public class RegisterWithTelphoneRequestModel : RequestBase
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        public string Telphone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [Required]
        public string LoginSign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetMothod()
        {
            return "useraccount.registerwithtelpone";
        }
    }

    public class RegisterWithTelphoneResponseModel : ResponseBase
    {
    }
}
