using Autofac;

namespace Aimrank.Infrastructure.Configuration
{
    internal static class AimrankCompositionRoot
    {
        private static IContainer _container;

        internal static void SetContainer(IContainer container) => _container = container;

        internal static ILifetimeScope BeginLifetimeScope() => _container.BeginLifetimeScope();
    }
}