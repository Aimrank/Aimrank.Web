using Aimrank.Modules.CSGO.Infrastructure.Configuration.DataAccess;
using Aimrank.Modules.CSGO.Infrastructure.Configuration.Mediator;
using Aimrank.Modules.CSGO.Infrastructure.Configuration.Pods;
using Aimrank.Modules.CSGO.Infrastructure.Configuration.Processing;
using Aimrank.Modules.CSGO.Infrastructure.Configuration.Quartz;
using Aimrank.Modules.CSGO.Infrastructure.Configuration.Rabbit;
using Autofac;
using System.Net.Http;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration
{
    public static class CSGOStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IHttpClientFactory httpClientFactory,
            RabbitMQSettings rabbitMqSettings)
        {
            ConfigureCompositionRoot(
                connectionString,
                httpClientFactory,
                rabbitMqSettings);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IHttpClientFactory httpClientFactory,
            RabbitMQSettings rabbitMqSettings)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new PodsModule());
            containerBuilder.RegisterModule(new RabbitMQModule(rabbitMqSettings));
            containerBuilder.RegisterInstance(httpClientFactory);

            _container = containerBuilder.Build();
            
            CSGOCompositionRoot.SetContainer(_container);
        }
    }
}