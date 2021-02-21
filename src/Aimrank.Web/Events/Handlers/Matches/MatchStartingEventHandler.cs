﻿using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Matches;
using Aimrank.Web.Hubs.Lobbies;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Matches
{
    public class MatchStartingEventHandler : IIntegrationEventHandler<MatchStartingEvent>
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _hubContext;

        public MatchStartingEventHandler(IHubContext<LobbyHub, ILobbyClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(MatchStartingEvent @event, CancellationToken cancellationToken = default)
            => await _hubContext.Clients.Groups(@event.Lobbies.Select(l => l.ToString())).MatchStarting(@event);
    }
}