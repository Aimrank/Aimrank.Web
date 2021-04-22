using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Lobbies.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.Events
{
    internal class LobbyStatusChangedPublishEventHandler
        : INotificationHandler<DomainEventNotification<LobbyStatusChangedDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public LobbyStatusChangedPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<LobbyStatusChangedDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new LobbyStatusChangedEvent(
                notification.DomainEvent.LobbyId.Value,
                (int) notification.DomainEvent.Status));
    }
}