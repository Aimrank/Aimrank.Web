using Aimrank.Web.Common.Domain;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchStartingDomainEvent : DomainEvent
    {
        public MatchId MatchId { get; }
        public IEnumerable<MatchLobby> Lobbies { get; }

        public MatchStartingDomainEvent(MatchId matchId, IEnumerable<MatchLobby> lobbies)
        {
            MatchId = matchId;
            Lobbies = lobbies;
        }
    }
}