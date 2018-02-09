namespace UniversitySystem.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Reflection;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SpaServices.Webpack;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    using AutoMapper;

    using Common;
    using Data;
    using Data.Models;
    using Data.Repositories;
    using Data.Repositories.Contracts;
    using Data.Initializer;
    using Infrastructure.Extensions;
    using Infrastructure.Mapping;


    public class Startup
    {
        private const string AssemblyUniversitySystem = "UniversitySystem";
        private const string AssemblyUniversitySystemDataModels = "UniversitySystem.Data.Models";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<UniversitySystemDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<Student, IdentityRole>()
                .AddEntityFrameworkStores<UniversitySystemDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration[GlobalJWTConstants.Issuer],
                        ValidAudience = Configuration[GlobalJWTConstants.Issuer],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[GlobalJWTConstants.Key]))
                    };
                });

            services.RegisterAllNonGenericDependenciesWhichImplement<IDependency>(AssemblyUniversitySystem);

            var repositoryPair = new KeyValuePair<Type, Type>(typeof(IRepository<>), typeof(EntityRepository<>));
            var modelTypes = Assembly.Load(AssemblyUniversitySystemDataModels).GetTypes();
            var genericTypes = new Dictionary<KeyValuePair<Type, Type>, Type[]>();
            genericTypes.Add(repositoryPair, modelTypes);
            services.RegisterGenericDependencies(genericTypes);
           
            Mapper.Initialize(config => config.AddProfile(new AutoMapperProfile(AssemblyUniversitySystem)));
            services.AddTransient<IMapper>(x => Mapper.Instance);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseInitialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
