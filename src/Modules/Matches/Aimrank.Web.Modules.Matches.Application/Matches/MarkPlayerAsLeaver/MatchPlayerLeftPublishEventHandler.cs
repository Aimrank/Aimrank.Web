using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Matches.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.MarkPlayerAsLeaver
{
    internal class MatchPlayerLeftPublishEventHandler
        : INotificationHandler<DomainEventNotification<MatchPlayerLeftDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public MatchPlayerLeftPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<MatchPlayerLeftDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new MatchPlayerLeftEvent(
                notification.DomainEvent.PlayerId.Value,
                notification.DomainEvent.Lobbies.Select(l => l.LobbyId.Value)));
    }
}