using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Lobbies.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.LeaveLobby
{
    internal class MemberRoleChangedPublishEventHandler
        : INotificationHandler<DomainEventNotification<MemberRoleChangedDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public MemberRoleChangedPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<MemberRoleChangedDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new MemberRoleChangedEvent(
                notification.DomainEvent.LobbyId,
                notification.DomainEvent.Member.PlayerId,
                (int) notification.DomainEvent.Member.Role));
    }
}