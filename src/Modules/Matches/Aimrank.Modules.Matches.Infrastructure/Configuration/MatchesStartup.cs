using Aimrank.Common.Application;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Modules.CSGO.Application.Contracts;
using Aimrank.Modules.Matches.Infrastructure.Configuration.DataAccess;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Mediator;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Processing;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Quartz;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Rabbit;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Redis;
using Autofac;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration
{
    public static class MatchesStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            ICSGOModule csgoModule,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus,
            MatchesModuleSettings matchesModuleSettings)
        {
            ConfigureCompositionRoot(
                connectionString,
                csgoModule,
                executionContextAccessor,
                eventBus,
                matchesModuleSettings);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            ICSGOModule csgoModule,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus,
            MatchesModuleSettings matchesModuleSettings)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new RedisModule(matchesModuleSettings.RedisSettings));
            containerBuilder.RegisterModule(new RabbitMQModule(matchesModuleSettings.RabbitMQSettings));
            containerBuilder.RegisterInstance(executionContextAccessor);
            containerBuilder.RegisterInstance(eventBus);
            containerBuilder.RegisterInstance(csgoModule);

            _container = containerBuilder.Build();
            
            MatchesCompositionRoot.SetContainer(_container);
        }
    }
}