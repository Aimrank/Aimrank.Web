using Aimrank.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using Autofac;

namespace Aimrank.Modules.UserAccess.Infrastructure.Configuration
{
    public static class UserAccessStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString)
        {
            ConfigureCompositionRoot(connectionString);
        }

        private static void ConfigureCompositionRoot(string connectionString)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));

            _container = containerBuilder.Build();
            
            UserAccessCompositionRoot.SetContainer(_container);
        }
    }
}