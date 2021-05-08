using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Application;
using Aimrank.Web.Common.Infrastructure;
using Aimrank.Web.Modules.Matches.Application.Clients;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Infrastructure.Application;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Quartz;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration
{
    public class MatchesModuleStartup : IModuleStartup
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMatchesModule, MatchesModule>();
            services.AddScoped<DbContext, MatchesContext>();
            services.AddDbContext<MatchesContext>(options => options
                .UseNpgsql(configuration.GetConnectionString("Database"),
                    x => x.MigrationsAssembly(GetType().Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>());
        }

        public void Initialize(IApplicationBuilder builder, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(MatchesModuleSettings)).Get<MatchesModuleSettings>();
            
            var eventBus = builder.ApplicationServices.GetRequiredService<IEventBus>();
            
            ILogger logger = builder.ApplicationServices.GetRequiredService<ILogger<MatchesModule>>();
            
            var services = new ServiceCollection();

            services.AddDataAccess(configuration.GetConnectionString("Database"));
            services.AddProcessing();
            services.AddQuartz();
            services.AddRedis(settings.RedisSettings);
            services.AddApplication();
            services.AddSingleton(builder.ApplicationServices.GetRequiredService<IExecutionContextAccessor>());
            services.AddSingleton(builder.ApplicationServices.GetRequiredService<IClusterClient>());
            services.AddSingleton(eventBus);
            services.AddSingleton(logger);
            
            MatchesCompositionRoot.SetProvider(services.BuildServiceProvider());
            
            EventBusStartup.Initialize(eventBus);
            QuartzStartup.Initialize();
        }
    }
}