using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration
{
    internal static class MatchesCompositionRoot
    {
        private static IServiceProvider _provider;

        internal static void SetProvider(IServiceProvider provider) => _provider = provider;

        internal static IServiceScope CreateScope() => _provider.CreateScope();
    }
}