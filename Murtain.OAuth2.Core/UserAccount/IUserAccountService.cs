using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Services;
using Murtain.OAuth2.SDK.Requests.UserAccount;

namespace Murtain.OAuth2.Core
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserAccountService : IdentityServer3.Core.Services.IUserService, IApplicationService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetPagingResponseModel GetPaging(GetPagingRequestModel request);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AddResponseModel Add(AddRequestModel request);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SaveResponseModel Save(SaveRequestModel request);
        /// <summary>
        /// 使用手机号码注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RegisterWithTelphoneResponseModel RegisterWithTelphone(RegisterWithTelphoneRequestModel request);
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetProfileDataResponseModel GetUserProfileData(GetProfileDataRequestModel request);
        /// <summary>
        /// 设置/修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SetPasswordResponseModel SetPassword(SetPasswordRequestModel request);
        /// <summary>
        /// 设置/修改邮箱
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SetEmailResponseModel SetEmail(SetEmailRequestModel request);
    }
}
