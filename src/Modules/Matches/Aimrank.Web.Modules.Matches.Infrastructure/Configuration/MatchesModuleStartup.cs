using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Application;
using Aimrank.Web.Common.Infrastructure;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Infrastructure.Application;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Quartz;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Redis;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration
{
    public class MatchesModuleStartup : IModuleStartup
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IMatchesModule, MatchesModule>();
        }

        public void Initialize(IApplicationBuilder builder, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(MatchesModuleSettings)).Get<MatchesModuleSettings>();
            
            var eventBus = builder.ApplicationServices.GetRequiredService<IEventBus>();
            
            ILogger logger = builder.ApplicationServices.GetRequiredService<ILogger<MatchesModule>>();
            
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule(new DataAccessModule(configuration.GetConnectionString("Database")));
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new RedisModule(settings.RedisSettings));
            containerBuilder.RegisterModule(new ApplicationModule());
            containerBuilder.RegisterInstance(builder.ApplicationServices.GetRequiredService<IExecutionContextAccessor>());
            containerBuilder.RegisterInstance(builder.ApplicationServices.GetRequiredService<IClusterModule>());
            containerBuilder.RegisterInstance(eventBus);
            containerBuilder.RegisterInstance(logger);
            
            MatchesCompositionRoot.SetContainer(containerBuilder.Build());
            
            EventBusStartup.Initialize(eventBus);
            QuartzStartup.Initialize();
        }
    }
}