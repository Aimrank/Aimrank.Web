using System.Collections.Generic;
using System;

namespace Aimrank.Web.Hubs.Lobbies.Messages
{
    public class MatchReadyEventMessage
    {
        public Guid MatchId { get; }
        public string Map { get; }
        public DateTime ExpiresAt { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchReadyEventMessage(Guid matchId, string map, DateTime expiresAt, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            Map = map;
            ExpiresAt = expiresAt;
            Lobbies = lobbies;
        }
    }
}