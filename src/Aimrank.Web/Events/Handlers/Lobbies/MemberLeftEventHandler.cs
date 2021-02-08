using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.Web.Hubs.Lobbies;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Lobbies
{
    public class MemberLeftEventHandler : IIntegrationEventHandler<MemberLeftEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;

        public MemberLeftEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(MemberLeftEvent @event, CancellationToken cancellationToken = default)
            => await _hubContext.Clients.Group(@event.LobbyId.ToString()).MemberLeft(@event);
    }
}