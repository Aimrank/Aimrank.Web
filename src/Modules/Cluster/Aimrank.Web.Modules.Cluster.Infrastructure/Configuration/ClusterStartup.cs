using Aimrank.Web.Common.Infrastructure.EventBus;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Mediator;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Pods;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Quartz;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Rabbit;
using Autofac;
using System.Net.Http;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration
{
    public static class ClusterStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IEventBus eventBus,
            IHttpClientFactory httpClientFactory,
            ClusterModuleSettings clusterModuleSettings)
        {
            ConfigureCompositionRoot(
                connectionString,
                eventBus,
                httpClientFactory,
                clusterModuleSettings);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IEventBus eventBus,
            IHttpClientFactory httpClientFactory,
            ClusterModuleSettings clusterModuleSettings)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new PodsModule());
            containerBuilder.RegisterModule(new RabbitMQModule(clusterModuleSettings.RabbitMQSettings));
            containerBuilder.RegisterInstance(httpClientFactory);
            containerBuilder.RegisterInstance(eventBus);

            _container = containerBuilder.Build();
            
            ClusterCompositionRoot.SetContainer(_container);
        }
    }
}