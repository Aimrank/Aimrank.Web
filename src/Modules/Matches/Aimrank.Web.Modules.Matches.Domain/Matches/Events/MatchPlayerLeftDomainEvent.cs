using Aimrank.Web.Common.Domain;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchPlayerLeftDomainEvent : DomainEvent
    {
        public MatchPlayer Player { get; }
        public IEnumerable<MatchLobby> Lobbies { get; }

        public MatchPlayerLeftDomainEvent(MatchPlayer player, IEnumerable<MatchLobby> lobbies)
        {
            Player = player;
            Lobbies = lobbies;
        }
    }
}