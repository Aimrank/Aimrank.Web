using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents.Matches
{
    public class MatchTimedOutEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchTimedOutEvent(Guid matchId, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            Lobbies = lobbies;
        }
    }
}