using Autofac;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration
{
    internal static class ClusterCompositionRoot
    {
        private static IContainer _container;

        internal static void SetContainer(IContainer container) => _container = container;

        internal static ILifetimeScope BeginLifetimeScope() => _container.BeginLifetimeScope();
    }
}