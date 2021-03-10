using Aimrank.Application.Commands.Matches.TimeoutReadyMatch;
using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Events.Handlers.Matches
{
    public class MatchReadyEventHandler : IIntegrationEventHandler<MatchReadyEvent>
    {
        private readonly IAimrankModule _aimrankModule;
        private readonly ITopicEventSender _topicEventSender;

        public MatchReadyEventHandler(IAimrankModule aimrankModule, ITopicEventSender topicEventSender)
        {
            _aimrankModule = aimrankModule;
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MatchReadyEvent @event, CancellationToken cancellationToken = default)
        {
            #pragma warning disable 4014

            Task.Delay(30000, cancellationToken)
                .ContinueWith(async (task) =>
                {
                    await _aimrankModule.ExecuteCommandAsync(new TimeoutReadyMatchCommand(@event.MatchId));
                }, cancellationToken)
                .ConfigureAwait(false);
            
            #pragma warning restore 4014

            var message = new MatchReadyMessage(
                @event.MatchId,
                @event.Map, 
                DateTime.UtcNow.AddSeconds(30),
                @event.Lobbies);

            foreach (var lobbyId in @event.Lobbies)
            {
                await _topicEventSender.SendAsync($"MatchReady:{lobbyId}", message, cancellationToken);
            }
        }
    }
}