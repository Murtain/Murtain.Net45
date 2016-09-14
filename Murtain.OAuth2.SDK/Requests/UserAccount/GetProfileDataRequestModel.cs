using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Murtain.Domain.Services;
using Murtain.Runtime.Validation;

namespace Murtain.OAuth2.SDK.Requests.UserAccount
{
    /// <summary>
    /// 获取个人资料
    /// </summary>
    public class GetProfileDataRequestModel : RequestBase, IValidate
    {
        /// <summary>
        /// 用户账户Id
        /// </summary>
        [Required]
        public int? Id { get; set; }
        /// <summary>
        /// 获取请求服务
        /// </summary>
        /// <returns></returns>
        public override string GetMothod()
        {
            return "useraccount.getprofiledata";
        }
    }

    /// <summary>
    /// 获取个人资料
    /// </summary>
    public class GetProfileDataResponseModel : ResponseBase<SDK.ViewModels.UserAccount>
    {
    }
}
