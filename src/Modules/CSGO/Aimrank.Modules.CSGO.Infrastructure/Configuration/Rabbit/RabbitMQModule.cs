using Aimrank.Modules.CSGO.IntegrationEvents.External;
using Autofac;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration.Rabbit
{
    internal class RabbitMQModule : Autofac.Module
    {
        private readonly RabbitMQSettings _rabbitSettings;

        public RabbitMQModule(RabbitMQSettings rabbitSettings)
        {
            _rabbitSettings = rabbitSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var eventBus = new RabbitMQEventBus(_rabbitSettings);
            
            eventBus
                .Subscribe<MatchCanceledEvent>("Aimrank.Pod")
                .Subscribe<MatchFinishedEvent>("Aimrank.Pod")
                .Listen();
            
            builder.RegisterInstance(eventBus).SingleInstance();
        }
    }
}