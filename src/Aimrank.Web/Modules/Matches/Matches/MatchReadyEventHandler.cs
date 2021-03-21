using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Application.Matches.TimeoutReadyMatch;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.Matches.Matches
{
    public class MatchReadyEventHandler : IIntegrationEventHandler<MatchReadyEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;
        private readonly IMatchesModule _matchesModule;

        public MatchReadyEventHandler(LobbyEventSender lobbyEventSender, IMatchesModule matchesModule)
        {
            _lobbyEventSender = lobbyEventSender;
            _matchesModule = matchesModule;
        }

        public async Task HandleAsync(MatchReadyEvent @event, CancellationToken cancellationToken = default)
        {
            #pragma warning disable 4014

            Task.Delay(30000, cancellationToken)
                .ContinueWith(async (task) =>
                {
                    await _matchesModule.ExecuteCommandAsync(new TimeoutReadyMatchCommand(@event.MatchId));
                }, cancellationToken)
                .ConfigureAwait(false);
            
            #pragma warning restore 4014

            var payload = new MatchReadyPayload(
                new MatchReadyRecord(
                    @event.MatchId,
                    @event.Map,
                    DateTime.UtcNow.AddSeconds(30)));

            foreach (var lobbyId in @event.Lobbies)
            {
                await _lobbyEventSender.SendAsync($"MatchReady", lobbyId, payload, cancellationToken);
            }
        }
    }
}