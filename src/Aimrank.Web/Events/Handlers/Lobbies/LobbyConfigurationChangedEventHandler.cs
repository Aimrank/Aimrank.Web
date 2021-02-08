using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.Web.Hubs.Lobbies;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Lobbies
{
    public class LobbyConfigurationChangedEventHandler : IIntegrationEventHandler<LobbyConfigurationChangedEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;

        public LobbyConfigurationChangedEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(LobbyConfigurationChangedEvent @event,
            CancellationToken cancellationToken = default)
            => await _hubContext.Clients.Group(@event.LobbyId.ToString()).LobbyConfigurationChanged(@event);
    }
}