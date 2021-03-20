using System.Collections.Generic;
using System;

namespace Aimrank.Modules.Matches.IntegrationEvents.Matches
{
    public class MatchPlayerLeftEvent : IntegrationEventBase
    {
        public Guid PlayerId { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchPlayerLeftEvent(Guid playerId, IEnumerable<Guid> lobbies)
        {
            PlayerId = playerId;
            Lobbies = lobbies;
        }
    }
}