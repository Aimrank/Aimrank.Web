using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Quartz
{
    internal static class Extensions
    {
        public static IServiceCollection AddQuartz(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(Assemblies.Infrastructure)
                .AddClasses(c => c.AssignableTo<IJob>())
                .AsSelf()
                .WithTransientLifetime());
                
            return services;
        }
    }
}