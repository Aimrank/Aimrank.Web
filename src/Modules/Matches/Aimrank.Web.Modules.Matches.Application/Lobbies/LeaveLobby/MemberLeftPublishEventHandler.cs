using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Lobbies.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.LeaveLobby
{
    internal class MemberLeftPublishEventHandler
        : INotificationHandler<DomainEventNotification<MemberLeftDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public MemberLeftPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<MemberLeftDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new MemberLeftEvent(
                notification.DomainEvent.LobbyId,
                notification.DomainEvent.Member.PlayerId));
    }
}