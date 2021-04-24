using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Players;
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
        public IEnumerable<MatchPlayerDto> Players { get; }

        public MatchStartedDomainEvent(MatchId matchId, string map, MatchMode mode, string address,
            IEnumerable<MatchLobby> lobbies, IEnumerable<MatchPlayerDto> players)
        {
            MatchId = matchId;
            Map = map;
            Mode = mode;
            Address = address;
            Lobbies = lobbies;
            Players = players;
        }

        public class MatchPlayerDto
        {
            public PlayerId PlayerId { get; init; }
            public string SteamId { get; init; }
            public MatchTeam Team { get; init; }
            public MatchPlayerStats Stats { get; init; }
            public int RatingStart { get; init; }
            public int RatingEnd { get; init; }
            public bool IsLeaver { get; init; }
        
            public static MatchPlayerDto FromMatchPlayer(MatchPlayer player)
                => new MatchPlayerDto
                {
                    PlayerId = player.PlayerId,
                    SteamId = player.SteamId,
                    Team = player.Team,
                    Stats = player.Stats,
                    RatingStart = player.RatingStart,
                    RatingEnd = player.RatingEnd,
                    IsLeaver = player.IsLeaver
                };
        }
    }
}