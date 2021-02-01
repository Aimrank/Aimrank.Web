using Aimrank.Domain.Users;

namespace Aimrank.Domain.Matches
{
    public class MatchPlayer
    {
        public UserId UserId { get; }
        public string SteamId { get; }
        public MatchTeam Team { get; }
        public int Kills { get; private set; }
        public int Assists { get; private set; }
        public int Deaths { get; private set; }
        public int Score { get; private set; }
        
        private MatchPlayer() {}

        public MatchPlayer(UserId userId, string steamId, MatchTeam team)
        {
            UserId = userId;
            SteamId = steamId;
            Team = team;
        }

        public void UpdateStats(int kills, int assists, int deaths, int score)
        {
            Kills = kills;
            Assists = assists;
            Deaths = deaths;
            Score = score;
        }
    }
}