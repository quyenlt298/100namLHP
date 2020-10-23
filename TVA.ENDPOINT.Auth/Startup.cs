using DVG.BDS.DAL.SQLServer.Library;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using TVA.DATA.DAL.IData.IDatabase;
using TVA.DATA.DAL.SqlServer.Configs;
using TVA.ENDPOINT.Auth.Configuration;
using TVA.ENDPOINT.Auth.Model;
using TVA.SERVICES.Domain.Account;
using TVA.SERVICES.External.Base;
using TVA.SERVICES.Infrastructure.Account;

namespace TVA.ENDPOINT.Auth
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Base config
            services.Configure<ConfigDb>(Configuration.GetSection("ConfigDb"));

            // Define base
            services.AddSingleton<IConnectDatabase, ConnectSQL>();
            services.AddSingleton<ISocialNetworkProvider, SocialNetworkProvider>();

            // Register instance
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IExtensionGrantValidator, DelegationGrantValidator>();

            services.AddMvc()
                .AddJsonOptions(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            // Configuration Identity Server 4
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(ConfigAuthInfo.GetIdentityResources)
                .AddInMemoryApiResources(ConfigAuthInfo.GetApiResources)
                .AddInMemoryClients(ConfigAuthInfo.GetClients)
                .AddCustomResourceOwnerPassword()
                .AddCustomSocialNetworkPassword();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
            app.UseAuthentication();
        }
    }
}
