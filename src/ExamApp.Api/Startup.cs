using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExamApp.Api.Framework;
using ExamApp.Core.Repositories;
using ExamApp.Infrastructure.Mappers;
using ExamApp.Infrastructure.Mongo;
using ExamApp.Infrastructure.Repositories;
using ExamApp.Infrastructure.Services;
using ExamApp.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using MongoDB.Driver;
using ExamApp.Infrastructure.IoC;

namespace ExamApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Add framework services.
            services.AddMvc()
                .AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);
            services.AddMemoryCache(); 
            services.AddAuthorization(x => x.AddPolicy("HasAdminRole", p => p.RequireRole("admin")));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "http://localhost:5000",
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_123!"))
                };
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            // loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            //app.AddNLogWeb();
            env.ConfigureNLog("nlog.config");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            MongoConfigurator.Initialize();

            SeedData(app);
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }

        private void SeedData(IApplicationBuilder app)
        {
            var settings = app.ApplicationServices.GetService<IOptions<AppSettings>>();
            if(settings.Value.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }
        }
    }
}
