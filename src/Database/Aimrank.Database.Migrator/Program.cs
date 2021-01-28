using Aimrank.Common.Infrastructure;
using Aimrank.Infrastructure;
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

    var optionsBuilder = new DbContextOptionsBuilder<AimrankContext>()
        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
        .UseSqlServer(connectionString, ConfigureContext);

    using var context = new AimrankContext(optionsBuilder.Options);
    
    if (context.Database.GetPendingMigrations().Any())
    {
        Console.WriteLine("Running database migration...");
        
        context.Database.Migrate();
        
        Console.WriteLine("Migration finished successfully.");
    }
    else
    {
        Console.WriteLine("All migrations already applied.");
    }
}

static void ConfigureContext(SqlServerDbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromMinutes(1), null);
    optionsBuilder.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
}