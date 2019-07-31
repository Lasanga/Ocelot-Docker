using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

namespace BringIt.Gateway.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationProviderKey = "AuthKey";
            Action<IdentityServerAuthenticationOptions> options = o =>
            {
                o.Authority = "http://bringit.auth.api:80";
                o.ApiName = "api1";
                o.SupportedTokens = SupportedTokens.Both;
                o.ApiSecret = "secret";
                o.RequireHttpsMetadata = false;
            };

            services.AddAuthentication()
                .AddIdentityServerAuthentication(authenticationProviderKey, options);

            services.AddMvc();
            services.AddOcelot()
                    .AddPolly();
                   // .AddDelegatingHandler<ClientHandler>(true);

            services.AddCors(x =>
            {
                x.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddSwaggerForOcelot(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
            app.UseStaticFiles();
            app.UseSwaggerForOcelotUI(Configuration)
                .UseOcelot()
                .Wait();
        }
    }
}
