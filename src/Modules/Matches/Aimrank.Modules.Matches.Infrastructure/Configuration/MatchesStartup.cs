using Aimrank.Common.Application;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Modules.Matches.Infrastructure.Configuration.CSGO;
using Aimrank.Modules.Matches.Infrastructure.Configuration.DataAccess;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Mediator;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Processing;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Quartz;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Redis;
using Autofac;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration
{
    public static class MatchesStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus,
            CSGOSettings csgoSettings,
            RedisSettings redisSettings)
        {
            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                eventBus,
                csgoSettings,
                redisSettings);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus,
            CSGOSettings csgoSettings,
            RedisSettings redisSettings)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new CSGOModule(csgoSettings));
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new RedisModule(redisSettings));
            containerBuilder.RegisterInstance(executionContextAccessor);
            containerBuilder.RegisterInstance(eventBus);

            _container = containerBuilder.Build();
            
            MatchesCompositionRoot.SetContainer(_container);
        }
    }
}