using Aimrank.Web.Common.Infrastructure;
using Aimrank.Web.Modules.Matches.Infrastructure;
using Aimrank.Web.Modules.UserAccess.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;
using System;

var configuration = LoadConfiguration();

MigrateDatabase(configuration);

static IConfiguration LoadConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(AppContext.BaseDirectory))
        .AddJsonFile("settings.json");

    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    if (environment is not null)
    {
        builder.AddJsonFile($"settings.{environment}.json", true);
    }

    builder.AddEnvironmentVariables();

    return builder.Build();
}

static void MigrateDatabase(IConfiguration configuration)
{
    Console.WriteLine("Preparing to migrate database...");
    
    var connectionString = configuration.GetConnectionString("Database");

    var optionsBuilderMatches = new DbContextOptionsBuilder<MatchesContext>()
        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
        .UseNpgsql(connectionString, ConfigureContext)
        .UseSnakeCaseNamingConvention();
        
    var optionsBuilderUserAccess = new DbContextOptionsBuilder<UserAccessContext>()
        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
        .UseNpgsql(connectionString, ConfigureContext)
        .UseSnakeCaseNamingConvention();

    using var contextMatches = new MatchesContext(optionsBuilderMatches.Options);
    using var contextUserAccess = new UserAccessContext(optionsBuilderUserAccess.Options);
    
    MigrateContexts(contextMatches, contextUserAccess);
}

static void ConfigureContext(NpgsqlDbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromMinutes(1), null);
    optionsBuilder.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
}

static void MigrateContexts(params DbContext[] contexts)
{
    foreach (var context in contexts)
    {
        var name = context.GetType().Name;

        var migrations = context.Database.GetPendingMigrations().ToList();
        if (migrations.Count > 0)
        {
            Console.WriteLine($"[{name}] Running database migration...");
            context.Database.Migrate();
            Console.WriteLine($"[{name}] {migrations.Count} migrations applied successfully.");
        }
        else
        {
            Console.WriteLine($"[{name}] All migrations already applied.");
        }
    }
}