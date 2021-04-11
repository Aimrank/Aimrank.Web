using Aimrank.Common.Infrastructure;
using Aimrank.Modules.CSGO.Infrastructure;
using Aimrank.Modules.Matches.Infrastructure;
using Aimrank.Modules.UserAccess.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        .AddJsonFile("settings.json", true);

    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    if (environment is not null)
    {
        builder.AddJsonFile($"settings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true);
    }

    builder.AddEnvironmentVariables();

    return builder.Build();
}

static void MigrateDatabase(IConfiguration configuration)
{
    Console.WriteLine("Preparing to migrate database...");
    
    var connectionString = configuration.GetConnectionString("Database");

    var optionsBuilderCSGO = new DbContextOptionsBuilder<CSGOContext>()
        .UseSqlServer(connectionString, ConfigureContext);

    var optionsBuilderMatches = new DbContextOptionsBuilder<MatchesContext>()
        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
        .UseSqlServer(connectionString, ConfigureContext);
        
    var optionsBuilderUserAccess = new DbContextOptionsBuilder<UserAccessContext>()
        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
        .UseSqlServer(connectionString, ConfigureContext);

    using var contextCSGO = new CSGOContext(optionsBuilderCSGO.Options);
    using var contextMatches = new MatchesContext(optionsBuilderMatches.Options);
    using var contextUserAccess = new UserAccessContext(optionsBuilderUserAccess.Options);
    
    MigrateContexts(contextCSGO, contextMatches, contextUserAccess);
}

static void ConfigureContext(SqlServerDbContextOptionsBuilder optionsBuilder)
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
            Console.WriteLine($"[{name}] {migrations} migrations applied successfully.");
        }
        else
        {
            Console.WriteLine($"[{name}] All migrations already applied.");
        }
    }
}