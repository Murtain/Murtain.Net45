using Murtain.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Requests.UserAccount
{
    /// <summary>
    /// 设置邮箱
    /// </summary>

    public class SetEmailRequestModel : RequestBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 新邮箱
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// 获取请求服务
        /// </summary>
        /// <returns></returns>
        public override string GetMothod()
        {
            return "useraccount.setemail";
        }
    }

    /// <summary>
    /// 设置邮箱
    /// </summary>
    public class SetEmailResponseModel : ResponseBase
    {

    }
}
