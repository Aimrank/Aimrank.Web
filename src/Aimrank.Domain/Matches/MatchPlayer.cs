namespace Aimrank.Domain.Matches
{
    public class MatchPlayer
    {
        public MatchId MatchId { get; }
        public string SteamId { get; set; }
        public string Name { get; set; }
        public MatchTeam Team { get; set; }
        public int Score { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }

        private MatchPlayer() {}

        public MatchPlayer(
            MatchId matchId,
            string steamId,
            string name,
            MatchTeam team,
            int score,
            int kills,
            int deaths,
            int assists)
        {
            MatchId = matchId;
            SteamId = steamId;
            Name = name;
            Team = team;
            Score = score;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
        }
    }
}