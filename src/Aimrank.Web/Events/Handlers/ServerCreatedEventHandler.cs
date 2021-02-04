using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents;
using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers
{
    public class ServerCreatedEventHandler : IIntegrationEventHandler<ServerCreatedEvent>
    {
        private readonly IHubContext<GeneralHub, IGeneralClient> _hubContext;

        public ServerCreatedEventHandler(IHubContext<GeneralHub, IGeneralClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(ServerCreatedEvent @event, CancellationToken cancellationToken = default)
            => await _hubContext.Clients.All.ServerCreated(@event);
    }
}