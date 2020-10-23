using IdentityServer4.Models;
using System.Collections.Generic;

namespace TVA.ENDPOINT.Auth.Model
{
    public static class ConfigAuthInfo
    {
        public static IEnumerable<IdentityResource> GetIdentityResources
        {
            get
            {
                return new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                };
            }
        }

        public static IEnumerable<Client> GetClients
        {
            get
            {
                return new List<Client>
                {
                    new Client
                    {
                        ClientId = "7F472E6A73D6AE6D",
                        ClientName = "Android App",
                        ClientSecrets = { new Secret("5A8A1B3F7B6111E7CA34EFE5235C2".Sha256()) },
                        AllowedGrantTypes = ("password,client_credentials,delegation").Split(","),
                        AllowedScopes = ("openid,profile,TravelAssistant.Api").Split(","),
                        //RedirectUris = { "http://localhost:5000/signin-oidc" },
                        //PostLogoutRedirectUris = { "http://localhost:5000/signout-callback-oidc" },
                        AllowOfflineAccess = true
                    },
                    new Client
                    {
                        ClientId = "9EB7BB633269FCDF",
                        ClientName = "Ios App",
                        ClientSecrets = { new Secret("9CAE8F4E4E35463F5E97634AB126E".Sha256()) },
                        AllowedGrantTypes = ("password,client_credentials,delegation").Split(","),
                        AllowedScopes = ("openid,profile,TravelAssistant.Api").Split(","),
                        //RedirectUris = { "http://localhost:5000/signin-oidc" },
                        //PostLogoutRedirectUris = { "http://localhost:5000/signout-callback-oidc" },
                        AllowOfflineAccess = true
                    }
                };
            }
        }

        public static IEnumerable<ApiResource> GetApiResources
        {
            get
            {
                return new List<ApiResource>
                {
                    new ApiResource("TravelAssistant.Api","Travel Assistant Apis"),
                };
            }
        }
    }
}
