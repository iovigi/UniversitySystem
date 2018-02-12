namespace UniversitySystem.Web
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc;

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

            services.AddIdentity<Student, IdentityRole>(config =>
            {
                var password = config.Password;
                password.RequireDigit = false;
                password.RequiredUniqueChars = 0;
                password.RequireLowercase = false;
                password.RequireNonAlphanumeric = false;
                password.RequireUppercase = false;
            }).AddEntityFrameworkStores<UniversitySystemDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.FromSeconds(360));
            services.AddAuthentication()
                .Services.ConfigureApplicationCookie(options =>
                {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                });

            services.RegisterAllNonGenericDependenciesWhichImplement<IDependency>(AssemblyUniversitySystem);

            var repositoryPair = new KeyValuePair<Type, Type>(typeof(IRepository<>), typeof(EntityRepository<>));
            var modelTypes = Assembly.Load(AssemblyUniversitySystemDataModels).GetTypes();
            var genericTypes = new Dictionary<KeyValuePair<Type, Type>, Type[]>();
            genericTypes.Add(repositoryPair, modelTypes);
            services.RegisterGenericDependencies(genericTypes);

            Mapper.Initialize(config => config.AddProfile(new AutoMapperProfile(AssemblyUniversitySystem)));
            services.AddTransient<IMapper>(x => Mapper.Instance);

            services.AddSession();
            services.AddMvc(opt=> {
                opt.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Common/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Course}/{action=GetCourseList}/{id?}");
            });
        }
    }
}
