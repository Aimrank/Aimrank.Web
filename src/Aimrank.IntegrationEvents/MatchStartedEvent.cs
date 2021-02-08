using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents
{
    public class MatchStartedEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public string Map { get; }
        public string Address { get; }
        public IEnumerable<Guid> Players { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchStartedEvent(
            Guid matchId,
            string map,
            string address,
            IEnumerable<Guid> players,
            IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            Map = map;
            Address = address;
            Players = players;
            Lobbies = lobbies;
        }
    }
}