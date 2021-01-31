using Aimrank.Application.Services;
using Aimrank.Infrastructure.Application.Services;
using Autofac;

namespace Aimrank.Infrastructure.Configuration.Authentication
{
    internal class AuthenticationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
        }
    }
}