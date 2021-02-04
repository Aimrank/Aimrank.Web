using Aimrank.Common.Application.Events;
using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents
{
    public class MatchStartedEvent : IntegrationEvent
    {
        public Guid MatchId { get; }
        public string Address { get; }
        public string Map { get; }
        public IEnumerable<Guid> Players { get; }

        public MatchStartedEvent(
            Guid id,
            Guid matchId,
            string address,
            string map,
            IEnumerable<Guid> players,
            DateTime occurredAt)
            : base(id, occurredAt)
        {
            MatchId = matchId;
            Address = address;
            Map = map;
            Players = players;
        }
    }
}