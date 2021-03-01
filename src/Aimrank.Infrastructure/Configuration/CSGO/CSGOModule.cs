using Aimrank.Application.CSGO;
using Aimrank.Application.Services;
using Aimrank.Infrastructure.Application.CSGO;
using Aimrank.Infrastructure.Application.Services.Matches;
using Autofac;

namespace Aimrank.Infrastructure.Configuration.CSGO
{
    internal class CSGOModule : Autofac.Module
    {
        private readonly CSGOSettings _csgoSettings;

        public CSGOModule(CSGOSettings csgoSettings)
        {
            _csgoSettings = csgoSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_csgoSettings.UseFakeServerProcessManager)
            {
                builder.RegisterType<FakeServerProcessManager>().As<IServerProcessManager>().SingleInstance();
            }
            else
            {
                builder.RegisterType<ServerProcessManager>().As<IServerProcessManager>().SingleInstance();
            }
            
            builder.RegisterType<ServerEventMapper>().As<IServerEventMapper>().SingleInstance();
            builder.RegisterInstance(_csgoSettings).SingleInstance();
            builder.RegisterType<MatchService>().As<IMatchService>().InstancePerLifetimeScope();
        }
    }
}