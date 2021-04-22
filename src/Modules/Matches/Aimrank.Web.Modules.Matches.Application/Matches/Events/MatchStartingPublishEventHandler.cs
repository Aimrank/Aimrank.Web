using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Matches.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.Events
{
    internal class MatchStartingPublishEventHandler
        : INotificationHandler<DomainEventNotification<MatchStartingDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public MatchStartingPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<MatchStartingDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new MatchStartingEvent(
                notification.DomainEvent.MatchId.Value,
                notification.DomainEvent.Lobbies.Select(l => l.LobbyId.Value)));
    }
}