using Aimrank.Application.CSGO;
using Aimrank.Infrastructure.Application.CSGO;
using Autofac;

namespace Aimrank.Infrastructure.Configuration.CSGO
{
    internal class CSGOModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServerProcessManager>().As<IServerProcessManager>().SingleInstance();
            builder.RegisterType<ServerEventNotifier>().As<IServerEventNotifier>().SingleInstance();
        }
    }
}