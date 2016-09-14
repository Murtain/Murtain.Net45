using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.OAuth2.SDK.Requests.Captcha;
using Murtain.Domain.Services;

namespace Murtain.OAuth2.Core
{
    public interface ICaptchaService : IApplicationService
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MessageCaptchaSendResponse> MessageCaptchaSendAsync(MessageCaptchaSendRequest request);

        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmailCaptchaSendResponse> EmailCaptchaSendAsync(EmailCaptchaSendRequest request);
    }
}
