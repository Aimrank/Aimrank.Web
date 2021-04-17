using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Application.Players.GetPlayerStatsBatch
{
    public class PlayerStatsModeDto
    {
        public int Mode { get; set; }
        public int MatchesTotal { get; set; }
        public int MatchesWon { get; set; }
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalHs { get; set; }
        public IEnumerable<PlayerStatsMapDto> Maps { get; set; }
    }
}