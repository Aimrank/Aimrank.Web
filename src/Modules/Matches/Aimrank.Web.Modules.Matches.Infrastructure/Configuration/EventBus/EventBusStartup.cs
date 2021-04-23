using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.MatchCanceled;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.MatchFinished;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.MatchStarted;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.PlayerDisconnected;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus
{
    internal static class EventBusStartup
    {
        public static void Initialize(IEventBus eventBus)
        {
            eventBus
                .Subscribe(new IntegrationEventGenericHandler<MatchStartedEvent>())
                .Subscribe(new IntegrationEventGenericHandler<MatchCanceledEvent>())
                .Subscribe(new IntegrationEventGenericHandler<MatchFinishedEvent>())
                .Subscribe(new IntegrationEventGenericHandler<PlayerDisconnectedEvent>());
        }
    }
}