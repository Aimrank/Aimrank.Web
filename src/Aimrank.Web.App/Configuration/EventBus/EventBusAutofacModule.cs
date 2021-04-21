using Aimrank.Web.App.Configuration.EventBus.RabbitMQ;
using Aimrank.Web.Common.Infrastructure.EventBus;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Aimrank.Web.App.Configuration.EventBus
{
    public class EventBusAutofacModule : Module
    {
        private readonly IConfiguration _configuration;

        public EventBusAutofacModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var rabbitMqSettings = _configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
            builder.RegisterInstance(rabbitMqSettings);
            builder.RegisterType<RabbitMQEventSerializer>().SingleInstance();
            builder.RegisterType<RabbitMQRoutingKeyFactory>().SingleInstance();
            builder.RegisterType<InMemoryEventBusClient>().As<IEventBus>().SingleInstance();
            builder.RegisterDecorator<RabbitMQEventBus, IEventBus>();
        }
    }
}