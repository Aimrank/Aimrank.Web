using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Matches;
using Aimrank.Web.Hubs.Lobbies;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Matches
{
    public class MatchPlayerLeftEventHandler : IIntegrationEventHandler<MatchPlayerLeftEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;

        public MatchPlayerLeftEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(MatchPlayerLeftEvent @event, CancellationToken cancellationToken = default)
            => await _hubContext.Clients.User(@event.UserId.ToString()).MatchPlayerLeft(@event);
    }
}