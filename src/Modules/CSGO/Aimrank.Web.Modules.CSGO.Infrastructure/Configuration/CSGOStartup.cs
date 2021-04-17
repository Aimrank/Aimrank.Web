using Aimrank.Web.Common.Infrastructure.EventBus;
using Aimrank.Web.Modules.CSGO.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.CSGO.Infrastructure.Configuration.Mediator;
using Aimrank.Web.Modules.CSGO.Infrastructure.Configuration.Pods;
using Aimrank.Web.Modules.CSGO.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.CSGO.Infrastructure.Configuration.Quartz;
using Aimrank.Web.Modules.CSGO.Infrastructure.Configuration.Rabbit;
using Autofac;
using System.Net.Http;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Configuration
{
    public static class CSGOStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IEventBus eventBus,
            IHttpClientFactory httpClientFactory,
            CSGOModuleSettings csgoModuleSettings)
        {
            ConfigureCompositionRoot(
                connectionString,
                eventBus,
                httpClientFactory,
                csgoModuleSettings);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IEventBus eventBus,
            IHttpClientFactory httpClientFactory,
            CSGOModuleSettings csgoModuleSettings)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new PodsModule());
            containerBuilder.RegisterModule(new RabbitMQModule(csgoModuleSettings.RabbitMQSettings));
            containerBuilder.RegisterInstance(httpClientFactory);
            containerBuilder.RegisterInstance(eventBus);

            _container = containerBuilder.Build();
            
            CSGOCompositionRoot.SetContainer(_container);
        }
    }
}