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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer3.EntityFramework
{
    class EfConstants
    {
        public const string ConnectionName = "IdentityServer3";

        public class TableNames
        {
            public const string Client = "CLIENTS";
            public const string ClientClaim = "CLIENT_CLAIMS";
            public const string ClientCustomGrantType = "CLIENT_CUSTOM_GRANT_TYPES";
            public const string ClientIdPRestriction = "CLIENT_P_RESTRICTIONS";
            public const string ClientPostLogoutRedirectUri = "CLIENT_POST_LOGOUT_REDIRECT_URIS";
            public const string ClientRedirectUri = "CLIENT_REDIRECT_URIS";
            public const string ClientScopes = "CLIENT_SCOPES";
            public const string ClientSecret = "CLIENT_SECRETS";
            public const string ClientCorsOrigin = "CLIENT_CORS_ORIGINS";

            public const string Scope = "SCOPES";
            public const string ScopeClaim = "SCOPE_CLAIMS";
            public const string ScopeSecrets = "SCOPE_SECRETS";

            public const string Consent = "CONSENTS";
            public const string Token = "TOKENS";
        }
    }
}
