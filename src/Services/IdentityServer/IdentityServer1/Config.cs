// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
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
            new("HandbookAPI")
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
            }
        };
}