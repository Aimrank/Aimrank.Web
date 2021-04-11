using Autofac;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration
{
    internal static class CSGOCompositionRoot
    {
        private static IContainer _container;

        internal static void SetContainer(IContainer container) => _container = container;

        internal static ILifetimeScope BeginLifetimeScope() => _container.BeginLifetimeScope();
    }
}