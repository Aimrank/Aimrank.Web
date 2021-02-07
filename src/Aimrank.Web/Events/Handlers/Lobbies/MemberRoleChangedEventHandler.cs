using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Lobbies
{
    public class MemberRoleChangedEventHandler : IIntegrationEventHandler<MemberRoleChangedEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;

        public MemberRoleChangedEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(MemberRoleChangedEvent @event, CancellationToken cancellationToken = default)
            => await _hubContext.Clients.Group(@event.LobbyId.ToString()).MemberRoleChanged(@event);
    }
}