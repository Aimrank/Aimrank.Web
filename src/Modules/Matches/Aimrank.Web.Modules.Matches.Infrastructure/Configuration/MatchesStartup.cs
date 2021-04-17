using Aimrank.Web.Common.Application;
using Aimrank.Web.Common.Infrastructure.EventBus;
using Aimrank.Web.Modules.CSGO.Application.Contracts;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Mediator;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Quartz;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Rabbit;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Redis;
using Autofac;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration
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