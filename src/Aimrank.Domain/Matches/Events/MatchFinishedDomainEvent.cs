using Aimrank.Common.Domain;
using Aimrank.Domain.Lobbies;
using System.Collections.Generic;

namespace Aimrank.Domain.Matches.Events
{
    public class MatchFinishedDomainEvent : IDomainEvent
    {
        public Match Match { get; }
        public IEnumerable<LobbyId> Lobbies { get; }

        public MatchFinishedDomainEvent(Match match, IEnumerable<LobbyId> lobbies)
        {
            Match = match;
            Lobbies = lobbies;
        }
    }
}