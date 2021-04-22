using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Domain.Matches.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.FinishMatch
{
    internal class MatchFinishedPublishEventHandler
        : INotificationHandler<DomainEventNotification<MatchFinishedDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        public MatchFinishedPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DomainEventNotification<MatchFinishedDomainEvent> notification,
            CancellationToken cancellationToken)
            => _eventBus.Publish(new MatchFinishedEvent(
                notification.DomainEvent.MatchId.Value,
                notification.DomainEvent.ScoreT,
                notification.DomainEvent.ScoreCT,
                notification.DomainEvent.Lobbies.Select(l => l.LobbyId.Value)));
    }
}