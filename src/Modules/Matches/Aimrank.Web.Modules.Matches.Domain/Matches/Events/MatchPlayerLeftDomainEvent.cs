using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchPlayerLeftDomainEvent : DomainEvent
    {
        public PlayerId PlayerId { get; }
        public IEnumerable<MatchLobby> Lobbies { get; }

        public MatchPlayerLeftDomainEvent(PlayerId playerId, IEnumerable<MatchLobby> lobbies)
        {
            PlayerId = playerId;
            Lobbies = lobbies;
        }
    }
}