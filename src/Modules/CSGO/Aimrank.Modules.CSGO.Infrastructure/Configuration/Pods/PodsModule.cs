using Aimrank.Modules.CSGO.Application.Services;
using Autofac;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration.Pods
{
    internal class PodsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PodClient>().As<IPodClient>().InstancePerLifetimeScope();
        }
    }
}