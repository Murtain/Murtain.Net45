# Murtain
Murtain is a rapid development framework for.net web applications.

## Murtain.Web.ApiDocument
Murtain.Web.ApiDocument is a most friendly WebApi2 help documentation.

|Package Name           | Status                |
|-----------------------|-----------------------|
|Murtain.Web.ApiDocument|[![NuGet version](https://d25lcipzij17d.cloudfront.net/badge.svg?id=nu&type=6&v=1.0.0.21&x2=0)](https://www.nuget.org/packages/Murtain.Web.ApiDocument)|

### How to use

1.Install packages.

    Install-Package Murtain.SDK
    Install-Package Murtain.Web
    Install-Package Murtain.Web.ApiDocument

2.Change your WebApiConfig.cs 

    config.Filters.Add(new WebApiExceptionFilterAttribute());
    config.Filters.Add(new ValidateModelAttribute());
            
    config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
    config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new SnakeCaseContractResolver();
    config.MessageHandlers.Add(new DefaultHandler());

This will allow WebApi to use only the Json mode and change the output and input styles for little snakecase.

3.Documentation

You just need to add Controller document annotations.

4.Return code and sample

    /// <summary>
    /// 验证短信验证码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [ReturnCode(typeof(VALIDATE_MESSAGE_CAPTCHA_RETURN_CODE))]
    [Route("api/account/sms-validate")]
    [JsonSample(typeof(ValidateMessageCaptchaSample))]
    public async Task ValidateMessageCaptchaAsync([FromBody] ValidateMessageCaptchaRequestModel input)
    {
        await userAccountService.ValidateMessageCaptchaAsync(input);
    }

Like this just need add an ReturnCodeAttribute. Here is the enumeration of the return code .

`HttpCorresponding` defines the return type http status.

    /// <summary>
    /// 验证短信验证码返回码
    /// </summary>
    public enum VALIDATE_MESSAGE_CAPTCHA_RETURN_CODE
    {
        /// <summary>
        /// 验证码无效
        /// </summary>
        [Description("验证码无效")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        INVALID_CAPTCHA = 21000,
        /// <summary>
        /// 验证码已过期，请重新获取验证码
        /// </summary>
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        [Description("验证码已过期，请重新获取验证码")]
        EXPIRED_CAPTCHA,
    }

If you want to display a sample in your document, just need add an JsonSampleAttribute. Here is the sample.

    /// <summary>
    /// Validate message captcha  samples
    /// </summary>
    public class ValidateMessageCaptchaSample : IJsonSampleModel
    {
        /// <summary>
        /// get error sample model
        /// </summary>
        /// <returns></returns>
        public object GetErrorSampleModel()
        {
            return new ResponseContentModel(VALIDATE_MESSAGE_CAPTCHA_RETURN_CODE.EXPIRED_CAPTCHA, "api/account/sms-validate");
        }
        /// <summary>
        /// get request sample model
        /// </summary>
        /// <returns></returns>
        public object GetRequestSampleModel()
        {
            return new ValidateMessageCaptchaRequestModel()
            {
                CaptchaType = MESSAGE_CAPTCHA_TYPE.REGISTER,
                Captcha = "600103",
                Mobile = "15618275257"
            };
        }
        /// <summary>
        /// get response sample model
        /// </summary>
        /// <returns></returns>
        public object GetResponseSampleModel()
        {
            return null;
        }
    }

Note: The sample class must inherit the IJsonSampleModel interface

## License

[MIT](LICENSE).
