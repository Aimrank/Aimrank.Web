using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents.Matches
{
    public class MatchCanceledEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchCanceledEvent(Guid matchId, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            Lobbies = lobbies;
        }
    }
}