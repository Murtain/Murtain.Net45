using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

using IdentityServer3.Core.Services.InMemory;
using IdentityServer3.Core;

namespace Murtain.OAuth2.Web.Configuration
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            var users = new List<InMemoryUser>
            {
                new InMemoryUser{Subject = "10001", Username = "Murtain", Password = "123312", 
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Murtain"),
                        new Claim(Constants.ClaimTypes.GivenName, "松超"),
                        new Claim(Constants.ClaimTypes.FamilyName, "许"),
                        new Claim(Constants.ClaimTypes.Email, "mobet_net@163.com"),
                        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(Constants.ClaimTypes.Role, "Admin"),
                        new Claim(Constants.ClaimTypes.Role, "Geek"),
                        new Claim(Constants.ClaimTypes.WebSite, "https://www.murtain.com/"),
                        new Claim(Constants.ClaimTypes.Address, @"{ ""详细地址"": ""黄浦区蒙自路207号"", ""Locality"": ""上海"", ""PostalCode"": 200000, ""Country"": ""中国"" }", Constants.ClaimValueTypes.Json)
                    }
                }
            };

            return users;
        }
    }
}