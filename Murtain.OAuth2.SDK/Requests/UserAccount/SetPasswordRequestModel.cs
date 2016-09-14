using Murtain.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Requests.UserAccount
{
    public class SetPasswordRequestModel : RequestBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 原密码
        /// </summary>
        [Required]
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 获取请求服务
        /// </summary>
        /// <returns></returns>
        public override string GetMothod()
        {
            return "useraccount.setpassword";
        }
    }


    public class SetPasswordResponseModel : ResponseBase
    {

    }
}
