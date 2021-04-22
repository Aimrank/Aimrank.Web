using Aimrank.Web.Common.Domain;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchStartedDomainEvent : DomainEvent
    {
        public MatchId MatchId { get; }
        public string Map { get; }
        public MatchMode Mode { get; }
        public string Address { get; }
        public IEnumerable<MatchLobby> Lobbies { get; }
        public IEnumerable<MatchPlayer> Players { get; }

        public MatchStartedDomainEvent(MatchId matchId, string map, MatchMode mode, string address, IEnumerable<MatchLobby> lobbies, IEnumerable<MatchPlayer> players)
        {
            MatchId = matchId;
            Map = map;
            Mode = mode;
            Address = address;
            Lobbies = lobbies;
            Players = players;
        }
    }
}