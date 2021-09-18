using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Summer.Infrastructure.SeedWork;
using ApiScope = IdentityServer4.Models.ApiScope;
using Client = IdentityServer4.Models.Client;
using IdentityResource = IdentityServer4.Models.IdentityResource;
using Secret = IdentityServer4.Models.Secret;


namespace Summer.Infrastructure.IdentityServer
{
    public class ConfigurationDbContextSeed : IDbContextSeed
    {
        private readonly ConfigurationDbContext _context;

        public ConfigurationDbContextSeed(ConfigurationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            if (!_context.Clients.Any())
            {
                foreach (var client in GetClients())
                {
                    _context.Clients.Add(client.ToEntity());
                }

                await _context.SaveChangesAsync();
            }

            if (!_context.IdentityResources.Any())
            {
                foreach (var resource in GetIdentityResources())
                {
                    _context.IdentityResources.Add(resource.ToEntity());
                }

                await _context.SaveChangesAsync();
            }

            if (!_context.ApiScopes.Any())
            {
                foreach (var resource in GetApiScopes())
                {
                    _context.ApiScopes.Add(resource.ToEntity());
                }

                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        private IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api1", "My API")
            };
        }

        private IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = {new Secret("secret".Sha256())},

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = {"api1"}
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "web client",
                    ClientName = "Web Client",

                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("web client secret".Sha256()) },

                    RedirectUris = { "http://localhost:5001/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5001/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

                    AllowedScopes = new [] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowAccessTokensViaBrowser = true,

                    RequireConsent = true,//是否显示同意界面
                    AllowRememberConsent = false,//是否记住同意选项
                }
            };
        }
    }
}