using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Matches.IntegrationEvents.Matches
{
    public class MatchAcceptedEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public Guid PlayerId { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchAcceptedEvent(Guid matchId, Guid playerId, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            PlayerId = playerId;
            Lobbies = lobbies;
        }
    }
}