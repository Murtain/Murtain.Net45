using IdentityServer3.Core.Configuration;
using IdentityServer3.WsFederation.Configuration;
using IdentityServer3.WsFederation.Models;
using IdentityServer3.WsFederation.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.QQ;
using Microsoft.Owin.Security.WsFederation;
using Murtain.OAuth2.Web.Configuration;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Murtain.OAuth2.Web.Startup))]
namespace Murtain.OAuth2.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.Map("/core", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    IssuerUri = "https://localhost:44373/",                                             //令牌颁发者Uri
                    SiteName = "IdentityServer3 -  Identity",                                           //站点名称
                    SigningCertificate = Certificate.Get(),                                             //X.509证书（和相应的私钥签名的安全令牌）
                    RequireSsl = true,                                                                  //必须为SSL,默认为True
                    Endpoints = new EndpointOptions                                                     //允许启用或禁用特定的端点（默认的所有端点都是启用的）。
                    {
                        EnableCspReportEndpoint = false
                    },
                    Factory = IdentityServer3Factory.Configure("DefaultConnection"),                  //自定义配置
                    PluginConfiguration = PluginConfiguration,                                          //插件配置,允许添加协议插件像WS联邦支持。
                    ProtocolLogoutUrls = new List<string>                                               //配置回调URL，应该叫中登出（主要协议插件有用）。
                    {

                    },
                    LoggingOptions = new LoggingOptions                                                 //日志配置
                    {

                    },
                    CspOptions = new CspOptions
                    {
                        Enabled = false,
                    },
                    EnableWelcomePage = true,                                                            //启用或禁用默认的欢迎页。默认为True
                    AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions //授权配置
                    {
                        EnablePostSignOutAutoRedirect = true,
                        IdentityProviders = ConfigureIdentityProviders,
                        LoginPageLinks = new List<LoginPageLink> {
                            new LoginPageLink{ Text = "立即注册", Href = "/Account/Registration"},
                            new LoginPageLink{ Text = "忘记密码？", Href = "/Account/ForgotPassword"}
                       }
                    },

                    EventsOptions = new EventsOptions                                                   //事件配置
                    {
                        RaiseSuccessEvents = true,
                        RaiseErrorEvents = true,
                        RaiseFailureEvents = true,
                        RaiseInformationEvents = true
                    }
                });
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
            {
                MetadataAddress = "https://localhost:44373/core/wsfed/metadata",
                Wtrealm = "urn:owinrp",
                SignInAsAuthenticationType = "Cookies"
            });
        }

        private void PluginConfiguration(IAppBuilder pluginApp, IdentityServerOptions options)
        {
            var wsFedOptions = new WsFederationPluginOptions(options);

            // data sources for in-memory services
            wsFedOptions.Factory.Register(new Registration<IEnumerable<RelyingParty>>(RelyingParties.Get()));
            wsFedOptions.Factory.RelyingPartyService = new Registration<IRelyingPartyService>(typeof(InMemoryRelyingPartyService));

            pluginApp.UseWsFederationPlugin(wsFedOptions);
        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            app.UseQQConnectAuthentication(new QQConnectAuthenticationOptions
            {
                AuthenticationType = "QQ",
                Caption = "QQ登录",
                SignInAsAuthenticationType = signInAsType,

                AppId = "wx1ef4827bc8bc7a47",
                AppSecret = "4f2229bfefd96aa9d527b1ea1da657b4 "
            });

            //app.UseWeChatAuthentication(new WeChatAuthenticationOptions
            //{
            //    AuthenticationType = "WeChat",
            //    Caption = "微信登录",
            //    SignInAsAuthenticationType = signInAsType,

            //    AppId = "wx1ef4827bc8bc7a47",
            //    AppSecret = "4f2229bfefd96aa9d527b1ea1da657b4 "
            //});
        }

    }
}