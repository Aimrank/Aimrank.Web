using System.Collections.Generic;
using System;

namespace Aimrank.Web.Hubs.Lobbies.Messages
{
    public class MatchReadyEventMessage
    {
        public Guid MatchId { get; init; }
        public string Map { get; init; }
        public DateTime ExpiresAt { get; init; }
        public IEnumerable<Guid> Lobbies { get; init; }
    }
}