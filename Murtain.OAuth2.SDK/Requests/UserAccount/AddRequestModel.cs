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
    public class AddRequestModel : RequestBase, IValidate
    {
        /// <summary>
        /// Open ID
        /// </summary>
        [MaxLength(50)]
        public virtual string OpenId { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        public virtual string NickName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime? Birthday { get; set; }
        /// <summary>
        /// 移动号码
        /// </summary>
        [MaxLength(50)]
        public virtual string Telphone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        public virtual string Email { get; set; }
        /// <summary>
        /// 街道地址
        /// </summary>
        [MaxLength(250)]
        public virtual string Street { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        [MaxLength(50)]
        public virtual string City { get; set; }
        /// <summary>
        /// 所在省
        /// </summary>
        [MaxLength(50)]
        public virtual string Province { get; set; }
        /// <summary>
        /// 所在国家
        /// </summary>
        [MaxLength(50)]
        public virtual string Country { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual byte? Sex { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(2000)]
        public virtual string Headimageurl { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(50)]
        public virtual string IdentityNo { get; set; }
        /// <summary>
        /// 获取请求服务
        /// </summary>
        /// <returns></returns>
        public override string GetMothod()
        {
            return "useraccount.add";
        }
    }


    public class AddResponseModel : ResponseBase
    {
        public AddResponseModel()
        {

        }
        public AddResponseModel(bool ok, string message)
            : base(ok, message)
        {
        }
    }
}
