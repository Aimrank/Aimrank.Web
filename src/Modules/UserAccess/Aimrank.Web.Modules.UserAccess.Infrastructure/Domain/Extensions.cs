using Aimrank.Web.Modules.UserAccess.Domain.Users;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Domain
{
    internal static class Extensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            return services;
        }
    }
}