using System.Collections.Generic;

namespace Aimrank.Modules.UserAccess.Application.Users.GetUserStatsBatch
{
    public class UserStatsModeDto
    {
        public int Mode { get; set; }
        public int MatchesTotal { get; set; }
        public int MatchesWon { get; set; }
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalHs { get; set; }
        public IEnumerable<UserStatsMapDto> Maps { get; set; }
    }
}