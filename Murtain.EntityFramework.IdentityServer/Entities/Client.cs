/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IdentityServer3.Core.Models;

namespace IdentityServer3.EntityFramework.Entities
{
    [Table("CLIENTS")]
    public class Client
    {
        [Key]
        [Column("ID")]
        public virtual int Id { get; set; }

        [Column("ENABLED")]
        public virtual bool Enabled { get; set; }

        [Required]
        [StringLength(200)]
        [Index(IsUnique=true)]
        [Column("CLIENT_ID")]
        public virtual string ClientId { get; set; }

        public virtual ICollection<ClientSecret> ClientSecrets { get; set; }
        
        [Required]
        [StringLength(200)]
        [Column("CLIENT_NAME")]
        public virtual string ClientName { get; set; }
        [StringLength(2000)]
        [Column("CLIENT_URI")]
        public virtual string ClientUri { get; set; }
        [Column("LOGO_URI")]
        public virtual string LogoUri { get; set; }

        [Column("REQUIRE_CONSENT")]
        public virtual bool RequireConsent { get; set; }
        [Column("ALLOW_REMEMBER_CONSENT")]
        public virtual bool AllowRememberConsent { get; set; }
        [Column("ALLOW_ACCESS_TOKENS_VIA_BROWSER")]
        public virtual bool AllowAccessTokensViaBrowser { get; set; }

        [Column("FLOW")]
        public virtual Flows Flow { get; set; }
        [Column("ALLOW_CLIENT_CREDENTIALS_ONLY")]
        public virtual bool AllowClientCredentialsOnly { get; set; }

        public virtual ICollection<ClientRedirectUri> RedirectUris { get; set; }
        public virtual ICollection<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

        [Column("LOGOUT_URI")]
        public virtual string LogoutUri { get; set; }
        [Column("LOGOUT_SESSION_REQUIRED")]
        public virtual bool LogoutSessionRequired { get; set; }
        [Column("REQUIRE_SIGN_OUT_PROMPT")]
        public virtual bool RequireSignOutPrompt { get; set; }

        [Column("ALLOW_ACCESS_TO_ALL_SCOPES")]
        public virtual bool AllowAccessToAllScopes { get; set; }
        public virtual ICollection<ClientScope> AllowedScopes { get; set; }

        // in seconds
        [Range(0, Int32.MaxValue)]
        [Column("IDENTITY_TOKEN_LIFETIME")]
        public virtual int IdentityTokenLifetime { get; set; }
        [Range(0, Int32.MaxValue)]
        [Column("ACCESS_TOKEN_LIFETIME")]
        public virtual int AccessTokenLifetime { get; set; }
        [Range(0, Int32.MaxValue)]
        [Column("AUTHORIZATION_CODE_LIFETIME")]
        public virtual int AuthorizationCodeLifetime { get; set; }
        
        [Range(0, Int32.MaxValue)]
        [Column("ABSOLUTE_REFRESH_TOKEN_LIFETIME")]
        public virtual int AbsoluteRefreshTokenLifetime { get; set; }
        [Range(0, Int32.MaxValue)]
        [Column("SLIDING_REFRESH_TOKEN_LIFETIME")]
        public virtual int SlidingRefreshTokenLifetime { get; set; }

        [Column("REFRESH_TOKEN_USAGE")]
        public virtual TokenUsage RefreshTokenUsage { get; set; }
        [Column("UPDATE_ACCESS_TOKEN_ON_REFRESH")]
        public virtual bool UpdateAccessTokenOnRefresh { get; set; }
        [Column("REFRESH_TOKEN_EXPIRATION")]
        public virtual TokenExpiration RefreshTokenExpiration { get; set; }
        [Column("ACCESS_TOKEN_TYPE")]
        public virtual AccessTokenType AccessTokenType { get; set; }

        [Column("ENABLE_LOCAL_LOGIN")]
        public virtual bool EnableLocalLogin { get; set; }
        public virtual ICollection<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }

        [Column("INCLUDE_JWT_ID")]
        public virtual bool IncludeJwtId { get; set; }

        public virtual ICollection<ClientClaim> Claims { get; set; }
        [Column("ALWAYS_SEND_CLIENT_CLAIMS")]
        public virtual bool AlwaysSendClientClaims { get; set; }
        [Column("PREFIX_CLIENT_CLAIMS")]
        public virtual bool PrefixClientClaims { get; set; }

        [Column("ALLOW_ACCESS_TO_ALL_GRANT_TYPES")]
        public virtual bool AllowAccessToAllGrantTypes { get; set; }

        public virtual ICollection<ClientCustomGrantType> AllowedCustomGrantTypes { get; set; }
        public virtual ICollection<ClientCorsOrigin> AllowedCorsOrigins { get; set; }
    }
}
