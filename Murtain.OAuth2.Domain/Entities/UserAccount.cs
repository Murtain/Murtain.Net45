using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Domain.Entities
{
    [Table("USER_ACCOUNT", Schema = "MA")]
    public class UserAccount : SoftDeleteEntityBase
    {
        /// <summary>
        /// 第三方登录提供程序Open Connect ID
        /// </summary>
        [MaxLength(50)]
        [Column("OPEN_ID")]
        public virtual string OpenId { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50)]
        [Column("NAME")]
        public virtual string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        [Column("NICK_NAME")]
        public virtual string NickName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [Column("BIRTHDAY")]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(50)]
        [Column("TELPHONE")]
        public virtual string Telphone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        [Column("EMAIL")]
        public virtual string Email { get; set; }
        /// <summary>
        /// 街道地址
        /// </summary>
        [MaxLength(250)]
        [Column("STREET")]
        public virtual string Street { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        [MaxLength(50)]
        [Column("CITY")]
        public virtual string City { get; set; }
        /// <summary>
        /// 所在省
        /// </summary>
        [MaxLength(50)]
        [Column("PROVINCE")]
        public virtual string Province { get; set; }
        /// <summary>
        /// 所在国家
        /// </summary>
        [MaxLength(50)]
        [Column("COUNTRY")]
        public virtual string Country { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Column("SEX")]
        public virtual byte? Sex { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(2000)]
        [Column("HEADIMAGE_URL")]
        public virtual string Headimageurl { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(50)]
        [Column("IDENTITY_NO")]
        public virtual string IdentityNo { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(250)]
        [Column("PASSWORD")]
        public virtual string Password { get; set; }
        /// <summary>
        /// 加密盐
        /// </summary>
        [MaxLength(50)]
        [Column("SALT")]
        public virtual string Salt { get; set; }
        /// <summary>
        /// Subject
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Column("SUBJECT")]
        public virtual string Subject { get; set; }
    }
}
