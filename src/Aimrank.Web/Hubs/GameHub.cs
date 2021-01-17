using Microsoft.AspNetCore.SignalR;

namespace Aimrank.Web.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
    }
}