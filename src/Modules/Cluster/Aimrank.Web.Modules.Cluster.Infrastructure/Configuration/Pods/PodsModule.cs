using Aimrank.Web.Modules.Cluster.Application.Clients;
using Autofac;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Pods
{
    internal class PodsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PodClient>().As<IPodClient>().InstancePerLifetimeScope();
        }
    }
}