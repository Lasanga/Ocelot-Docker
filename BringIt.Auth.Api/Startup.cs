using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BringIt.Auth.Api.Core;
using BringIt.Auth.Api.Infastructure;
using BringIt.Auth.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace BringIt.Auth.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<BringItAuthDbContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("BringIt.Auth.Api")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<BringItAuthDbContext>()
               .AddDefaultTokenProviders();
            var ss = IdentityServerBuilderExtensionsCrypto.CreateRsaSecurityKey();
            var builder1 = services.AddIdentityServer(options =>
            {
                options.IssuerUri = null;
            }).AddSigningCredential(ss)
              .AddInMemoryIdentityResources(Config.GetIdentityResources())
              .AddInMemoryClients(Config.GetClients())
              .AddInMemoryApiResources(Config.GetApis())
              .AddAspNetIdentity<ApplicationUser>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "AuthKey";

            }).AddJwtBearer("AuthKey", options =>
            {
                options.Authority = "http://bringit.auth.api:80";
                options.RequireHttpsMetadata = false;
                options.Audience = "api1";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Account API", Version = "v1" });
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Seed(app);
            app.UseIdentityServer();
            app.UseMvc();
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
               });
        }

        private void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<BringItAuthDbContext>();
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                context.Database.Migrate();
                SeedDbContext.Seed(context, userManager, roleManager).Wait();
            }
        }
    }
}
