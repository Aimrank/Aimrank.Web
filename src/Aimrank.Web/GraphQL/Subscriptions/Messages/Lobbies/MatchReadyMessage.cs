using System.Collections.Generic;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies
{
    public class MatchReadyMessage
    {
        public Guid MatchId { get; }
        public string Map { get; }
        public DateTime ExpiresAt { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchReadyMessage(Guid matchId, string map, DateTime expiresAt, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            Map = map;
            ExpiresAt = expiresAt;
            Lobbies = lobbies;
        }
    }
}