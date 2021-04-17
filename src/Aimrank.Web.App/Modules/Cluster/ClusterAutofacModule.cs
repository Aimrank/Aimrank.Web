using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Infrastructure;
using Autofac;

namespace Aimrank.Web.App.Modules.Cluster
{
    internal class ClusterAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClusterModule>()
                .As<IClusterModule>()
                .InstancePerLifetimeScope();
        }
    }
}