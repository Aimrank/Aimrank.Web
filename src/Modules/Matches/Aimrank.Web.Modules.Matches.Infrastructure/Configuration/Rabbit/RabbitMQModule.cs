using Aimrank.Web.Common.Infrastructure.EventBus.RabbitMQ;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchCanceled;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchFinished;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchStarted;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.PlayerDisconnected;
using Autofac;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Rabbit
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
                .Subscribe<MatchStartedEvent>(builder)
                .Subscribe<MatchCanceledEvent>(builder)
                .Subscribe<MatchFinishedEvent>(builder)
                .Subscribe<PlayerDisconnectedEvent>(builder)
                .Listen();
            
            builder.RegisterInstance(eventBus).SingleInstance();
        }
    }
}