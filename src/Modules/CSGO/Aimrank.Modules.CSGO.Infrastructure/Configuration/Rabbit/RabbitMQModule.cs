using Aimrank.Common.Infrastructure.EventBus.RabbitMQ;
using Aimrank.Modules.CSGO.Infrastructure.Application.Events.MatchCanceled;
using Aimrank.Modules.CSGO.Infrastructure.Application.Events.MatchFinished;
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
                .Subscribe<MatchCanceledEvent>(builder)
                .Subscribe<MatchFinishedEvent>(builder)
                .Listen();
            
            builder.RegisterInstance(eventBus).SingleInstance();
        }
    }
}