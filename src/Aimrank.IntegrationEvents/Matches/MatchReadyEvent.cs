using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents.Matches
{
    public class MatchReadyEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public string Map { get; }
        public DateTime ExpiresAt { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchReadyEvent(Guid matchId, string map, DateTime expiresAt, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            Map = map;
            ExpiresAt = expiresAt;
            Lobbies = lobbies;
        }
    }
}