using Aimrank.Common.Application;
using Aimrank.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using Aimrank.Modules.UserAccess.Infrastructure.Configuration.Mediator;
using Autofac;

namespace Aimrank.Modules.UserAccess.Infrastructure.Configuration
{
    public static class UserAccessStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor)
        {
            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();
            
            UserAccessCompositionRoot.SetContainer(_container);
        }
    }
}