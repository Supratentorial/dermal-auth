using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dermal.auth
{
    public class Config
    {

        public static IEnumerable<Client> Clients = new List<Client> {
                new Client{
                    ClientId = "dermal-spa",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = {
                        "http://localhost:4200/callback.html",
                        "http://localhost:4200/popup.html",
                        "http://localhost:4200/silent.html"
                    },
                    PostLogoutRedirectUris = { "http://localhost:4200/home" },
                    AllowedScopes = {"openid", "profile", "email", "api1"},
                    AllowedCorsOrigins = {"http://localhost:4200"}
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

        public static IEnumerable<ApiResource> Apis = new List<ApiResource>
        {
            new ApiResource("dermal-api", "Dermal API")
        };

    }
}

