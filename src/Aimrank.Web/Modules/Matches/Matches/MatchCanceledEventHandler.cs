using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Matches
{
    public class MatchCanceledEventHandler :  IIntegrationEventHandler<MatchCanceledEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MatchCanceledEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MatchCanceledEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchCanceledPayload(new MatchCanceledRecord(@event.MatchId));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _topicEventSender.SendAsync($"MatchCanceled:{lobbyId}", payload, cancellationToken);
            }
        }
    }
}