using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Matches.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.Events
{
    internal class MatchStartedPublishEventHandler
        : INotificationHandler<DomainEventNotification<MatchStartedDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public MatchStartedPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<MatchStartedDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new MatchStartedEvent(
                notification.DomainEvent.MatchId.Value,
                notification.DomainEvent.Map,
                notification.DomainEvent.Address,
                (int) notification.DomainEvent.Mode,
                notification.DomainEvent.Players.Select(p => p.PlayerId.Value),
                notification.DomainEvent.Lobbies.Select(l => l.LobbyId.Value)));
    }
}