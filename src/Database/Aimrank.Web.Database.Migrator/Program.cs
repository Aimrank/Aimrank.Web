﻿using Aimrank.Web.Common.Infrastructure;
using Aimrank.Web.Modules.Cluster.Infrastructure;
using Aimrank.Web.Modules.Matches.Infrastructure;
using Aimrank.Web.Modules.UserAccess.Infrastructure;
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

    var optionsBuilderCluster = new DbContextOptionsBuilder<ClusterContext>()
        .UseSqlServer(connectionString, ConfigureContext);

    var optionsBuilderMatches = new DbContextOptionsBuilder<MatchesContext>()
        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
        .UseSqlServer(connectionString, ConfigureContext);
        
    var optionsBuilderUserAccess = new DbContextOptionsBuilder<UserAccessContext>()
        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
        .UseSqlServer(connectionString, ConfigureContext);

    using var contextCluster = new ClusterContext(optionsBuilderCluster.Options);
    using var contextMatches = new MatchesContext(optionsBuilderMatches.Options);
    using var contextUserAccess = new UserAccessContext(optionsBuilderUserAccess.Options);
    
    MigrateContexts(contextCluster, contextMatches, contextUserAccess);
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
            Console.WriteLine($"[{name}] {migrations.Count} migrations applied successfully.");
        }
        else
        {
            Console.WriteLine($"[{name}] All migrations already applied.");
        }
    }
}