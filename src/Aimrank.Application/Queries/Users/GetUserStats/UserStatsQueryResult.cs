namespace Aimrank.Application.Queries.Users.GetUserStats
{
    public class UserStatsQueryResult
    {
        public int Mode { get; set; }
        public string Map { get; set; }
        public int MatchesTotal { get; set; }
        public int MatchesWon { get; set; }
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalHs { get; set; }
    }
}