using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Lobbies.GetLobbyForUser;
using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Hubs.Lobbies
{
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