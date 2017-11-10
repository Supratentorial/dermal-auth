using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace dermal.auth
{
    public class Config
    {

        public static IEnumerable<Client> Clients => new List<Client> {
                new Client{
                    ClientId = "dermal-spa",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    RequireConsent = false,
                    RedirectUris = {
                        "http://localhost:5000"
                    },
                    PostLogoutRedirectUris = { "http://localhost:5000/home" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "dermal-api"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:5000"
                    }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("dermal-api", "Dermal API")
        };

    }
}

