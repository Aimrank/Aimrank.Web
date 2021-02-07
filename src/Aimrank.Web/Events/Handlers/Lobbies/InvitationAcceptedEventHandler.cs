using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Lobbies
{
    public class InvitationAcceptedEventHandler : IIntegrationEventHandler<InvitationAcceptedEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;

        public InvitationAcceptedEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(InvitationAcceptedEvent @event, CancellationToken cancellationToken = default)
            => await _hubContext.Clients.Group(@event.LobbyId.ToString()).InvitationAccepted(@event);
    }
}