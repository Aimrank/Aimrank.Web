using Aimrank.Web.Common.Application;
using Aimrank.Web.Common.Infrastructure.EventBus;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchCanceled;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchFinished;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchStarted;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.PlayerDisconnected;
using Aimrank.Web.Modules.Matches.Infrastructure.Application;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Mediator;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Quartz;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Redis;
using Autofac;
using Microsoft.Extensions.Logging;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration
{
    public static class MatchesStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            MatchesModuleSettings matchesModuleSettings,
            ILogger logger,
            IClusterModule clusterModule,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus)
        {
            ConfigureCompositionRoot(
                connectionString,
                matchesModuleSettings,
                logger,
                clusterModule,
                executionContextAccessor,
                eventBus);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            MatchesModuleSettings matchesModuleSettings,
            ILogger logger,
            IClusterModule clusterModule,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new RedisModule(matchesModuleSettings.RedisSettings));
            containerBuilder.RegisterModule(new ApplicationModule());
            containerBuilder.RegisterInstance(executionContextAccessor);
            containerBuilder.RegisterInstance(eventBus);
            containerBuilder.RegisterInstance(logger);
            containerBuilder.RegisterInstance(clusterModule);
            
            eventBus.Subscribe(new MatchStartedEventHandler());
            eventBus.Subscribe(new MatchCanceledEventHandler());
            eventBus.Subscribe(new MatchFinishedEventHandler());
            eventBus.Subscribe(new PlayerDisconnectedEventHandler());

            _container = containerBuilder.Build();
            
            MatchesCompositionRoot.SetContainer(_container);
        }
    }
}