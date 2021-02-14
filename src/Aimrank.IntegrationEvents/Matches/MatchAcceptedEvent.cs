using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents.Matches
{
    public class MatchAcceptedEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public Guid UserId { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchAcceptedEvent(Guid matchId, Guid userId, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            UserId = userId;
            Lobbies = lobbies;
        }
    }
}