using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration
{
    internal static class UserAccessCompositionRoot
    {
        private static IServiceProvider _provider;

        internal static void SetContainer(IServiceProvider provider) => _provider = provider;

        internal static IServiceScope CreateScope() => _provider.CreateScope();
    }
}