using System;

namespace Aimrank.Application.Queries.Users.GetUserStatsBatch
{
    public class UserStatsQueryResult
    {
        public Guid UserId { get; set; }
        public int Mode { get; set; }
        public string Map { get; set; }
        public int MatchesTotal { get; set; }
        public int MatchesWon { get; set; }
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalHs { get; set; }
    }
}