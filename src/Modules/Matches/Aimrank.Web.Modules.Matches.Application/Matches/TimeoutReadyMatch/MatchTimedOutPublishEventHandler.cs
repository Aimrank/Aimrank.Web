using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Matches.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.TimeoutReadyMatch
{
    internal class MatchTimedOutPublishEventHandler
        : INotificationHandler<DomainEventNotification<MatchTimedOutDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public MatchTimedOutPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<MatchTimedOutDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new MatchTimedOutEvent(
                notification.DomainEvent.MatchId.Value,
                notification.DomainEvent.Lobbies.Select(l => l.LobbyId.Value)));
    }
}