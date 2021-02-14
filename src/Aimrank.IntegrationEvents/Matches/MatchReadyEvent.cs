using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents.Matches
{
    public class MatchReadyEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public string Map { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchReadyEvent(Guid matchId, string map, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            Map = map;
            Lobbies = lobbies;
        }
    }
}