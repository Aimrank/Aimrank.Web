using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents.Matches
{
    public class MatchStartingEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchStartingEvent(Guid matchId, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            Lobbies = lobbies;
        }
    }
}