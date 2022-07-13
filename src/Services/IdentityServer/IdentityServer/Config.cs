// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> GetApiResources() =>
        new[]
        {
            new ApiResource("HandbookAPI")
            {
                Scopes = new List<string>
                {
                    "HandbookAPI.read",
                    "HandbookAPI.write",
                    "HandbookAPI"
                }
                // ApiSecrets = new List<Secret>
                // {
                //     new Secret("ClientSecret1".Sha256())
                // },
                // UserClaims = new List<string>
                // {
                //     "role"
                // }
            },
            new ApiResource("movieAPI", "Movie API")
            {
                Scopes = new List<string>
                {
                    "role"
                }
            }
        };

    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new()
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };

    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope>
        {
            new("HandbookAPI.read"),
            new("HandbookAPI.write"),
            new("HandbookAPI"),
            new("movieAPI")
        };


    public static IEnumerable<Client> GetClients(string apiUrl) =>
        new[]
        {
            new()
            {
                ClientId = "client_id_swagger",
                ClientSecrets = {new Secret("client_secret_swagger".Sha256())},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedCorsOrigins = {$"{apiUrl}"},
                AllowedScopes =
                {
                    "HandbookAPI",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },

                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false,

                RedirectUris = {$"{apiUrl}/swagger/oauth2-redirect.html"},

                PostLogoutRedirectUris = {$"{apiUrl}/swagger/"}
            },

            new Client
            {
                ClientName = "Postman",
                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    "HandbookAPI"
                },
                RedirectUris = new[]
                {
                    "https://www.getpostman.com/oauth2/callback"
                },
                Enabled = true,
                ClientId = "client_id_postman",
                ClientSecrets = {new Secret("client_secret_postman".Sha256())},
                PostLogoutRedirectUris = {"http://localhost:5000/signout-callback-oidc"},
                ClientUri = null,
                AllowedGrantTypes = new[]
                {
                    GrantType.ResourceOwnerPassword
                }
            },
            new Client
            {
                ClientId = "movieClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"movieAPI"}
            },
            new Client
            {
                ClientId = "movies_mvc_client",
                ClientName = "Movies MVC Web App",
                AllowedGrantTypes = new[]
                {
                    GrantType.Hybrid
                },
                RequirePkce = false,
                AllowRememberConsent = false,
                RedirectUris = new List<string>
                {
                    "https://localhost:9801/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    "https://localhost:9801/signout-callback-oidc"
                },
                ClientSecrets = new List<Secret>
                {
                    new("secret".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    // IdentityServerConstants.StandardScopes.Address,
                    // IdentityServerConstants.StandardScopes.Email,
                    "movieAPI"
                },
                AllowOfflineAccess = true
            }
        };
}