using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Infrastructure.Data;
using Aimrank.Web.Common.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.DataAccess
{
    internal static class Extensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string databaseConnectionString)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            services.AddDbContext<MatchesContext>(options => options
                .UseNpgsql(databaseConnectionString)
                .UseSnakeCaseNamingConvention()
                .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>());
            
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<MatchesContext>());

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(_ =>
                new SqlConnectionFactory(databaseConnectionString));

            services.Scan(scan => scan
                .FromAssemblyOf<MatchesContext>()
                .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}