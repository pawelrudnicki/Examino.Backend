using System.Reflection;
using Autofac;
using ExamApp.Infrastructure.Services;

namespace ExamApp.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .SingleInstance();
        }
    }
}