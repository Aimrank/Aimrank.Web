using Aimrank.Web.Common.Infrastructure.EventBus.RabbitMQ;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchCanceled;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchFinished;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchStarted;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.PlayerDisconnected;
using Autofac;
using Microsoft.Extensions.Logging;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Rabbit
{
    internal class RabbitMQModule : Autofac.Module
    {
        private readonly RabbitMQSettings _rabbitSettings;
        private readonly ILogger _logger;

        public RabbitMQModule(RabbitMQSettings rabbitSettings, ILogger logger)
        {
            _rabbitSettings = rabbitSettings;
            _logger = logger;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var eventBus = new RabbitMQEventBus(_rabbitSettings, _logger);

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