using Aimrank.Application;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Infrastructure.Configuration.CSGO;
using Aimrank.Infrastructure.Configuration.DataAccess;
using Aimrank.Infrastructure.Configuration.Jwt;
using Aimrank.Infrastructure.Configuration.Mediator;
using Autofac;

namespace Aimrank.Infrastructure.Configuration
{
    public static class AimrankStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus,
            JwtSettings jwtSettings)
        {
            ConfigureCompositionRoot(connectionString, executionContextAccessor, eventBus, jwtSettings);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus,
            JwtSettings jwtSettings)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new CSGOModule());
            containerBuilder.RegisterModule(new JwtModule(jwtSettings));
            containerBuilder.RegisterInstance(executionContextAccessor);
            containerBuilder.RegisterInstance(eventBus);

            _container = containerBuilder.Build();
            
            AimrankCompositionRoot.SetContainer(_container);
        }
    }
}