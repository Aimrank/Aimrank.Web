using System.Collections.Generic;
using Aimrank.Common.Domain;

namespace Aimrank.Modules.Matches.Domain.Matches.Events
{
    public class MatchPlayerLeftDomainEvent : IDomainEvent
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