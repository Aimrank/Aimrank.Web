using Aimrank.Web.Modules.Matches.Application.Matches;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Matches;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Application
{
    internal static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMatchService, MatchService>();
            
            return services;
        }
    }
}