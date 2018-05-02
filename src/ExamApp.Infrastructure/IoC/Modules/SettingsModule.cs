using Autofac;
using ExamApp.Infrastructure.Extensions;
using ExamApp.Infrastructure.Mongo;
using ExamApp.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace ExamApp.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<JwtSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<AppSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<MongoSettings>())
                .SingleInstance();
        }
    }
}