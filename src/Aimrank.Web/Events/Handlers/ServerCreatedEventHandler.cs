using Aimrank.Application.Events;
using Aimrank.Common.Application;
using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Events.Handlers
{
    public class ServerCreatedEventHandler : IIntegrationEventHandler<ServerCreatedEvent>
    {
        private readonly IHubContext<GameHub, IGameClient> _hubContext;

        public ServerCreatedEventHandler(IHubContext<GameHub, IGameClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task HandleAsync(ServerCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Created server: {@event.Address} with map: {@event.Map}");
            // await _hubContext.Clients.All.ServerCreated(@event);
            return Task.CompletedTask;
        }
    }
}