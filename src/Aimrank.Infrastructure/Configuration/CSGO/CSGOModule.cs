using Aimrank.Application.CSGO;
using Aimrank.Infrastructure.Application.CSGO;
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
            builder.RegisterType<ServerProcessManager>().As<IServerProcessManager>().SingleInstance();
            builder.RegisterInstance(_csgoSettings).SingleInstance();
        }
    }
}