using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Matches.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.CancelMatch
{
    internal class MatchCanceledPublishEventHandler
        : INotificationHandler<DomainEventNotification<MatchCanceledDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public MatchCanceledPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<MatchCanceledDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new MatchCanceledEvent(
                notification.DomainEvent.MatchId.Value,
                notification.DomainEvent.Lobbies.Select(l => l.LobbyId.Value)));
    }
}