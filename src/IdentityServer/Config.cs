// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        // support for standard open id (subject id) and profile (first name, last name, etc.) scopes
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1", "My Api")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // the ClientId and ClientSecret can be thought of as the login and password for the application itself
                // it identifies the application to the identity server so that it knows which application is trying to connect to it
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },

                // interactive ASP.NET Core MVC Client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    
                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after Login
                    RedirectUris =
                    {
                        "https://localhost5002/signin-oidc"
                    },

                    // where to redirect to after Logout
                    PostLogoutRedirectUris =
                    {
                        "https://localhost5002/signout-callback-oidc"
                    },

                    // enable support for refresh tokens
                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        // add api to the allowed scopes list
                        "api1"
                    }
                }
            };
    }
}