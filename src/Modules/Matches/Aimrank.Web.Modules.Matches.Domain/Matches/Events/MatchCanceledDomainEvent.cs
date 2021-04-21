using Aimrank.Web.Common.Domain;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchCanceledDomainEvent : DomainEvent
    {
        public MatchId MatchId { get; }
        public IEnumerable<MatchLobby> Lobbies { get; }

        public MatchCanceledDomainEvent(MatchId matchId, IEnumerable<MatchLobby> lobbies)
        {
            MatchId = matchId;
            Lobbies = lobbies;
        }
    }
}