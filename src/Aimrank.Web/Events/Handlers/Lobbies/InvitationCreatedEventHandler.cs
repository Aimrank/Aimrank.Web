using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Lobbies
{
    public class InvitationCreatedEventHandler : IIntegrationEventHandler<InvitationCreatedEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _lobbyHubContext;
        private readonly IHubContext<GeneralHub, IGeneralClient> _generalHubContext;

        public InvitationCreatedEventHandler(
            IHubContext<LobbyHub, ILobbyClient> lobbyHubContext,
            IHubContext<GeneralHub, IGeneralClient> generalHubContext)
        {
            _lobbyHubContext = lobbyHubContext;
            _generalHubContext = generalHubContext;
        }

        public async Task HandleAsync(InvitationCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            var group = @event.LobbyId.ToString();

            await _lobbyHubContext.Clients.Group(group).InvitationCreated(@event);
            await _generalHubContext.Clients.User(@event.InvitedUserId.ToString()).InvitationCreated(@event);
        }
    }
}