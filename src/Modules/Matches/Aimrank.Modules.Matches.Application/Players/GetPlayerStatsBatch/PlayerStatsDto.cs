using System.Collections.Generic;

namespace Aimrank.Modules.Matches.Application.Players.GetPlayerStatsBatch
{
    public class PlayerStatsDto
    {
        public int MatchesTotal { get; set; }
        public int MatchesWon { get; set; }
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalHs { get; set; }
        public IEnumerable<PlayerStatsModeDto> Modes { get; set; }
    }
}