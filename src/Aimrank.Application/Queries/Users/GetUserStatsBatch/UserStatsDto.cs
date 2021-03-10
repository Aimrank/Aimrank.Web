using System.Collections.Generic;

namespace Aimrank.Application.Queries.Users.GetUserStatsBatch
{
    public class UserStatsDto
    {
        public int MatchesTotal { get; set; }
        public int MatchesWon { get; set; }
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalHs { get; set; }
        public IEnumerable<UserStatsModeDto> Modes { get; set; }
    }
}