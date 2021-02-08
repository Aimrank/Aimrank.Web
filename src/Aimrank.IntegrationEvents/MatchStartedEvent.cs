using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents
{
    public class MatchStartedEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public string Address { get; }
        public string Map { get; }
        public IEnumerable<Guid> Players { get; }

        public MatchStartedEvent(Guid matchId, string address, string map, IEnumerable<Guid> players)
        {
            MatchId = matchId;
            Address = address;
            Map = map;
            Players = players;
        }
    }
}