using Aimrank.Web.Common.Domain;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchReadyDomainEvent : DomainEvent
    {
        public MatchId MatchId { get; }
        public string Map { get; }
        public IEnumerable<MatchLobby> Lobbies { get; }

        public MatchReadyDomainEvent(MatchId matchId, string map, IEnumerable<MatchLobby> lobbies)
        {
            MatchId = matchId;
            Map = map;
            Lobbies = lobbies;
        }
    }
}