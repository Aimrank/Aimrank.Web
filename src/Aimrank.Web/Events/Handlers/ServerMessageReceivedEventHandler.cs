using Aimrank.Application.Events;
using Aimrank.Application;
using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Aimrank.Web.Events.Handlers
{
    public class ServerMessageReceivedEventHandler : IIntegrationEventHandler<ServerMessageReceivedEvent>
    {
        private readonly IHubContext<GameHub, IGameClient> _hubContext;

        public ServerMessageReceivedEventHandler(IHubContext<GameHub, IGameClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(ServerMessageReceivedEvent @event)
        {
            await _hubContext.Clients.All.EventReceived(@event.Content);
        }
    }
}