using Aimrank.Application;
using Aimrank.Infrastructure.Configuration.CSGO;
using Aimrank.Infrastructure.Configuration.DataAccess;
using Aimrank.Infrastructure.Configuration.Mediator;
using Aimrank.Infrastructure.EventBus;
using Autofac;

namespace Aimrank.Infrastructure.Configuration
{
    public static class AimrankStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus)
        {
            ConfigureCompositionRoot(connectionString, executionContextAccessor, eventBus);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new CSGOModule());
            containerBuilder.RegisterInstance(executionContextAccessor);
            containerBuilder.RegisterInstance(eventBus);

            _container = containerBuilder.Build();
            
            AimrankCompositionRoot.SetContainer(_container);
        }
    }
}