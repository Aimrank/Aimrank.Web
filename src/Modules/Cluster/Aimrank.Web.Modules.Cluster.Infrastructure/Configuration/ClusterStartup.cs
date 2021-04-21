using Aimrank.Web.Common.Infrastructure.EventBus;
using Aimrank.Web.Modules.Cluster.Infrastructure.Application.Events.MatchCanceled;
using Aimrank.Web.Modules.Cluster.Infrastructure.Application.Events.MatchFinished;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Mediator;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Pods;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Quartz;
using Autofac;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration
{
    public static class ClusterStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            ILogger logger,
            IHttpClientFactory httpClientFactory,
            IEventBus eventBus)
        {
            ConfigureCompositionRoot(
                connectionString,
                logger,
                httpClientFactory,
                eventBus);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            ILogger logger,
            IHttpClientFactory httpClientFactory,
            IEventBus eventBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new PodsModule());
            containerBuilder.RegisterInstance(httpClientFactory);
            containerBuilder.RegisterInstance(eventBus);
            containerBuilder.RegisterInstance(logger);
            
            eventBus.Subscribe(new MatchCanceledEventHandler());
            eventBus.Subscribe(new MatchFinishedEventHandler());

            _container = containerBuilder.Build();
            
            ClusterCompositionRoot.SetContainer(_container);
        }
    }
}