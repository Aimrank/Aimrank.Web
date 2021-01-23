using System;

namespace Aimrank.Web.Data
{
    public class MatchPlayer
    {
        public string SteamId { get; set; }
        public string Name { get; set; }
        public Guid MatchId { get; set; }
        public int Team { get; set; } // 2 - ct, 3 - t
        public int Score { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }

        private MatchPlayer() {}

        public MatchPlayer(string steamId, string name, Guid matchId, int team, int score, int kills, int deaths, int assists)
        {
            SteamId = steamId;
            Name = name;
            MatchId = matchId;
            Team = team;
            Score = score;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
        }
    }
}