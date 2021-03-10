using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Lobbies;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Lobbies
{
    public class LobbyStatusChangedEventHandler : IIntegrationEventHandler<LobbyStatusChangedEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public LobbyStatusChangedEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(LobbyStatusChangedEvent @event, CancellationToken cancellationToken = default)
            => await _topicEventSender.SendAsync($"LobbyStatusChanged:{@event.LobbyId}", @event, cancellationToken);
    }
}