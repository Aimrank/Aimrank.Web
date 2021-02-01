using Aimrank.Domain.Users;

namespace Aimrank.Domain.Matches
{
    public class MatchPlayer
    {
        public UserId Id { get; }
        public string SteamId { get; }
        public MatchTeam Team { get; }
        public int Kills { get; private set; }
        public int Assists { get; private set; }
        public int Deaths { get; private set; }
        public int Score { get; private set; }
        
        private MatchPlayer() {}

        public MatchPlayer(UserId id, string steamId, MatchTeam team)
        {
            Id = id;
            Team = team;
            SteamId = steamId;
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