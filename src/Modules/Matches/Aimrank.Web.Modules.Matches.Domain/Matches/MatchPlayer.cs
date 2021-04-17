using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System;

namespace Aimrank.Web.Modules.Matches.Domain.Matches
{
    public class MatchPlayer : Entity
    {
        public PlayerId PlayerId { get; }
        public string SteamId { get; }
        public MatchTeam Team { get; }
        public MatchPlayerStats Stats { get; private set; }
        public int RatingStart { get; private set; }
        public int RatingEnd { get; private set; }
        public bool IsLeaver { get; private set; }

        private MatchPlayer() {}

        internal MatchPlayer(PlayerId playerId, string steamId, MatchTeam team, int rating)
        {
            PlayerId = playerId;
            SteamId = steamId;
            Team = team;
            Stats = new MatchPlayerStats(0, 0, 0, 0);
            RatingStart = rating;
            IsLeaver = false;
        }

        internal void UpdateStats(MatchPlayerStats stats) => Stats = stats;
        
        internal void MarkAsLeaver()
        {
            if (IsLeaver)
            {
                return;
            }
            
            IsLeaver = true;
        }

        internal void CalculateNewRating(double score, int enemyRating)
        {
            if (IsLeaver)
            {
                RatingEnd = RatingStart - GetK();
            }
            else
            {
                var estimated = Math.Round(1.0 / (1 + Math.Pow(10, (enemyRating - RatingStart) / 400.0)), 2);
                RatingEnd = (int) Math.Round(RatingStart + GetK() * (score - estimated));
            }
        }

        private int GetK() => RatingStart switch
        {
            < 1600 => 50,
            < 2200 => 32,
            _ => 16
        };
    }
}