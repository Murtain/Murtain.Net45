using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Http.Owin;

using IdentityServer3.Core.ViewModels;
using IdentityServer3.Core.Models;

using Murtain.OAuth2.Core;
using Murtain.OAuth2.SDK.Requests.Captcha;
using Murtain.OAuth2.Web.Controllers.Shared;
using Murtain.Caching;
using Murtain.Web.Models;
using Murtain.Runtime.Cookie;
using Murtain.Runtime.Security;
using Murtain.Extensions;
using Murtain.OAuth2.Web.Models;
using Murtain.OAuth2.SDK.Requests.UserAccount;
using Murtain.AutoMapper;

namespace Murtain.OAuth2.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ICacheManager cacheManager;
        private readonly ICaptchaService captchaService;
        private readonly IUserAccountService userAccountService;

        public AccountController(ICaptchaService messageService, ICacheManager cacheManager, IUserAccountService userAccountService)
        {
            this.captchaService = messageService;
            this.cacheManager = cacheManager;
            this.userAccountService = userAccountService;
        }
        public virtual ActionResult Login(LoginViewModel model, SignInMessage message)
        {
            return this.View(model);
        }
        public virtual ActionResult Registration(string signin)
        {
            return View();
        }
        public virtual ActionResult Logout(LogoutViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult LoggedOut(LoggedOutViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return View();
        }
        public virtual ActionResult Consent(ConsentViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult Permissions(ClientPermissionsViewModel model)
        {
            return View(model);
        }
        public virtual ActionResult Error(IdentityServer3.Core.ViewModels.ErrorViewModel model)
        {
            return this.View(model);
        }


        [HttpGet]
        public ActionResult GetCaptcha()
        {
            return File(Captcha.GetBytes(Constants.CookieNames.CaptchaSignup), @"image/jpeg");
        }
        [HttpPost]
        public async Task<JsonResult> ValidateCaptchaAndSendMessageCode(string telphone, string captcha)
        {
            if (string.IsNullOrEmpty(telphone))
            {
                return Json(new MvcAjaxResponse(false, LocalizationManager
                    .GetSource(Constants.Localization.SourceName.Views)
                    .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_NOT_EXSIT_MOBILE)
                    ));
            }

            if (CryptoManager.DecryptDES(CookieManager.GetCookieValue(Constants.CookieNames.CaptchaSignup)) != captcha)
            {
                return Json(new MvcAjaxResponse(false,
                    LocalizationManager
                    .GetSource(Constants.Localization.SourceName.Views)
                    .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_ERROR_CAPTCHA)
                    ));
            }

            var response = await captchaService.MessageCaptchaSendAsync(new MessageCaptchaSendRequest
            {
                Captcha = SDK.Enums.MessageCaptcha.Register,
                Telphone = telphone,
                ExpiredTime = 30
            });

            return Json(new MvcAjaxResponse(response.Ok, response.Message));
        }
        [HttpPost]
        public JsonResult ValidateMessageCaptcha(string captcha, string telphone)
        {
            if (this.cacheManager.Get<string>(string.Format(Constants.CacheNames.MessageCaptcha, SDK.Enums.MessageCaptcha.Register, telphone, captcha)) != captcha)
            {
                return Json(new MvcAjaxResponse(false, LocalizationManager
                    .GetSource(Constants.Localization.SourceName.Views)
                    .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_EXPIRED_MESSAGE_CAPTCHA)
                    ));
            }

            return Json(new MvcAjaxResponse(LocalizationManager
                    .GetSource(Constants.Localization.SourceName.Views)
                    .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_MESSAGE_CAPTCHA_VALIDATE_OK)
                    ));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(string signin, LocalRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = userAccountService.RegisterWithTelphone(model.MapTo<RegisterWithTelphoneRequestModel>());
                if (response.Ok)
                {
                    return Redirect("/core/" + IdentityServer3.Core.Constants.RoutePaths.Login + "?signin=" + signin);
                }

                model.ErrorMessage = response.Message;
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult GetUserProfileData()
        {
            var model = userAccountService.GetUserProfileData(new GetProfileDataRequestModel
            {
                UserId = AppSession.UserId
            });
            return Json(new MvcAjaxResponse(model));
        }
    }
}