using Aimrank.Common.Infrastructure;
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

    var optionsBuilderMatches = new DbContextOptionsBuilder<MatchesContext>()
        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
        .UseSqlServer(connectionString, ConfigureContext);
        
    var optionsBuilderUserAccess = new DbContextOptionsBuilder<UserAccessContext>()
        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
        .UseSqlServer(connectionString, ConfigureContext);

    using var contextMatches = new MatchesContext(optionsBuilderMatches.Options);
    using var contextUserAccess = new UserAccessContext(optionsBuilderUserAccess.Options);
    
    if (contextMatches.Database.GetPendingMigrations().Any())
    {
        Console.WriteLine("[MatchesContext] Running database migration...");
        contextMatches.Database.Migrate();
        Console.WriteLine("[MatchesContext] Migration finished successfully.");
    }
    else
    {
        Console.WriteLine("[MatchesContext] All migrations already applied.");
    }
    
    if (contextUserAccess.Database.GetPendingMigrations().Any())
    {
        Console.WriteLine("[UserAccessContext] Running database migration...");
        contextUserAccess.Database.Migrate();
        Console.WriteLine("[UserAccessContext] Migration finished successfully.");
    }
    else
    {
        Console.WriteLine("[UserAccessContext] All migrations already applied.");
    }
}

static void ConfigureContext(SqlServerDbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromMinutes(1), null);
    optionsBuilder.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
}