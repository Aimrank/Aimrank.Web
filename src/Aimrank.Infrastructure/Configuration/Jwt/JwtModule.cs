using Aimrank.Domain.RefreshTokens;
using Aimrank.Infrastructure.Domain.RefreshTokens;
using Autofac;

namespace Aimrank.Infrastructure.Configuration.Jwt
{
    internal class JwtModule : Autofac.Module
    {
        private readonly JwtSettings _jwtSettings;

        public JwtModule(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtService>().As<IJwtService>().InstancePerLifetimeScope();
            builder.RegisterInstance(_jwtSettings).SingleInstance();
        }
    }
}