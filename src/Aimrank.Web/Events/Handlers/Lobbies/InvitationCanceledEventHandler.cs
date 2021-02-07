using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Lobbies
{
    public class InvitationCanceledEventHandler : IIntegrationEventHandler<InvitationCanceledEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;

        public InvitationCanceledEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(InvitationCanceledEvent @event, CancellationToken cancellationToken = default)
            => await _hubContext.Clients.Group(@event.LobbyId.ToString()).InvitationCanceled(@event);
    }
}