using Aimrank.Web.Common.Infrastructure;
using Aimrank.Web.Modules.Matches.Infrastructure;
using Aimrank.Web.Modules.UserAccess.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Database.Migrator
{
    public static class Program
    {
        private static readonly Type[] DbContextTypes = new[]
        {
            typeof(MatchesContext),
            typeof(UserAccessContext)
        };

        public static async Task Main()
        {
            var contexts = GetDbContexts();

            foreach (var context in contexts)
            {
                var migrations = await context.Database.GetPendingMigrationsAsync();
                if (migrations.Any())
                {
                    Console.WriteLine("Applying migrations...");

                    await context.Database.MigrateAsync();
                    
                    Console.WriteLine("Migrations applied.");
                }
                else
                {
                    Console.WriteLine("All migrations already applied.");
                }
                
                await context.DisposeAsync();
            }
        }

        private static IEnumerable<DbContext> GetDbContexts()
        {
            var configuration = GetConfiguration();

            var contexts = DbContextTypes.Select(t =>
            {
                var optionsBuilder = (DbContextOptionsBuilder) Activator
                    .CreateInstance(typeof(DbContextOptionsBuilder<>).MakeGenericType(t));
                optionsBuilder
                    .UseNpgsql(configuration.GetConnectionString("Database"))
                    .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
                    .UseSnakeCaseNamingConvention();
                return (DbContext) Activator.CreateInstance(t, optionsBuilder.Options);
            });

            return contexts;
        }
        
        private static IConfiguration GetConfiguration()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configurationBuilder = new ConfigurationBuilder();

            if (env == "Development")
            {
                configurationBuilder
                    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "migratorSettings.json"));
            }
            else
            {
                configurationBuilder
                    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"))
                    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, $"appsettings.{env}.json"), true)
                    .AddEnvironmentVariables();
            }

            return configurationBuilder.Build();
        }
    }
}