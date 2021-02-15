using Aimrank.Application.Commands.Matches.TimeoutReadyMatch;
using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Matches;
using Aimrank.Web.Hubs.Lobbies.Messages;
using Aimrank.Web.Hubs.Lobbies;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Events.Handlers.Matches
{
    public class MatchReadyEventHandler : IIntegrationEventHandler<MatchReadyEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;
        private readonly IAimrankModule _aimrankModule;

        public MatchReadyEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext, IAimrankModule aimrankModule)
        {
            _hubContext = hubContext;
            _aimrankModule = aimrankModule;
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

            var message = new MatchReadyEventMessage(
                @event.MatchId,
                @event.Map, 
                DateTime.UtcNow.AddSeconds(30),
                @event.Lobbies);

            await _hubContext.Clients.Groups(@event.Lobbies.Select(l => l.ToString())).MatchReady(message);
        }
    }
}