using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetLobbiesForMatch;
using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents;
using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers
{
    public class MatchStartingEventHandler : IIntegrationEventHandler<MatchStartingEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;
        private readonly IAimrankModule _aimrankModule;

        public MatchStartingEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext, IAimrankModule aimrankModule)
        {
            _hubContext = hubContext;
            _aimrankModule = aimrankModule;
        }

        public async Task HandleAsync(MatchStartingEvent @event, CancellationToken cancellationToken = default)
        {
            var lobbies = await _aimrankModule.ExecuteQueryAsync(new GetLobbiesForMatchQuery(@event.MatchId));

            await _hubContext.Clients.Groups(lobbies.Select(l => l.ToString())).MatchStarting(@event);
        }
    }
}