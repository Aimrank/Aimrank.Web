﻿using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents;
using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers
{
    public class MatchStartingEventHandler : IIntegrationEventHandler<MatchStartingEvent>
    {
        private readonly IHubContext<GeneralHub, IGeneralClient> _hubContext;

        public MatchStartingEventHandler(IHubContext<GeneralHub, IGeneralClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(MatchStartingEvent @event, CancellationToken cancellationToken = default)
            => await _hubContext.Clients.All.MatchStarting(@event);
    }
}