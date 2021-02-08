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
    public class MatchStartedEventHandler : IIntegrationEventHandler<MatchStartedEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;
        private readonly IAimrankModule _aimrankModule;

        public MatchStartedEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext, IAimrankModule aimrankModule)
        {
            _hubContext = hubContext;
            _aimrankModule = aimrankModule;
        }

        public async Task HandleAsync(MatchStartedEvent @event, CancellationToken cancellationToken = default)
        {
            var lobbies = await _aimrankModule.ExecuteQueryAsync(new GetLobbiesForMatchQuery(@event.MatchId));

            await _hubContext.Clients.Groups(lobbies.Select(l => l.ToString())).MatchStarted(@event);
        }
    }
}