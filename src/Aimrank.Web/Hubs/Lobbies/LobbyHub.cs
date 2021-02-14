using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetLobbyForUser;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.IntegrationEvents.Matches;
using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Hubs.Lobbies
{
    public interface ILobbyClient
    {
        Task Disconnect();
        Task MatchAccepted(MatchAcceptedEvent @event);
        Task MatchReady(MatchReadyEvent @event);
        Task MatchStarting(MatchStartingEvent @event);
        Task MatchTimedOut(MatchTimedOutEvent @event);
        Task MatchStarted(MatchStartedEvent @event);
        Task MatchFinished(MatchFinishedEvent @event);
        Task InvitationAccepted(InvitationAcceptedEvent @event);
        Task InvitationCanceled(InvitationCanceledEvent @event);
        Task InvitationCreated(InvitationCreatedEvent @event);
        Task LobbyConfigurationChanged(LobbyConfigurationChangedEvent @event);
        Task LobbyStatusChanged(LobbyStatusChangedEvent @event);
        Task MemberLeft(MemberLeftEvent @event);
        Task MemberRoleChanged(MemberRoleChangedEvent @event);
    }
    
    [JwtAuth]
    public class LobbyHub : Hub<ILobbyClient>
    {
        private readonly IAimrankModule _aimrankModule;

        public LobbyHub(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        public override async Task OnConnectedAsync()
        {
            var lobby = await _aimrankModule.ExecuteQueryAsync(new GetLobbyForUserQuery());
            if (lobby is null)
            {
                await Clients.Client(Context.ConnectionId).Disconnect();
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, lobby.Id.ToString());
            }
                
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}