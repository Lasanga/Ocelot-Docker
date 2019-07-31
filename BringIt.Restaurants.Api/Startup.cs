using AutoMapper;
using BringIt.Restaurant.Infrastrucutre;
using BringIt.Restaurant.Infrastrucutre.Repositories;
using BringIt.Restaurant.Infrastrucutre.Repositories.MenuRepository;
using BringIt.Restaurant.Infrastrucutre.Repositories.RestaurantRepository;
using BringIt.Restaurant.Infrastrucutre.UnitOfWork;
using BringIt.Restaurant.Services.MenuManager;
using BringIt.Restaurant.Services.Restaurant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BringIt.Restaurants.Api
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

            services.AddDbContext<BringItRestuarantDbContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "AuthKey";

            }).AddJwtBearer("AuthKey", options =>
            {
                options.Authority = "http://bringit.auth.api:80";
                options.RequireHttpsMetadata = false;

                options.Audience = "api1";
            });
            services.AddAutoMapper();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();

            services.AddTransient<IMenuManager, MenuManager>();
            services.AddTransient<IRestaurantManager, RestaurantManager>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Restaurants API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
               });
        }
    }
}
