using Autofac;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration
{
    internal static class MatchesCompositionRoot
    {
        private static IContainer _container;

        internal static void SetContainer(IContainer container) => _container = container;

        internal static ILifetimeScope BeginLifetimeScope() => _container.BeginLifetimeScope();
    }
}