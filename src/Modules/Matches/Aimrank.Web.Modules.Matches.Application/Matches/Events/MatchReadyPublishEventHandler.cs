using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Matches.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.Events
{
    internal class MatchReadyPublishEventHandler
        : INotificationHandler<DomainEventNotification<MatchReadyDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public MatchReadyPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<MatchReadyDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new MatchReadyEvent(
                notification.DomainEvent.MatchId.Value,
                notification.DomainEvent.Map,
                notification.DomainEvent.Lobbies.Select(l => l.LobbyId.Value)));
    }
}