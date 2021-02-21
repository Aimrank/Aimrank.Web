using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System;

namespace Aimrank.Domain.Matches
{
    public class MatchPlayer : Entity
    {
        public UserId UserId { get; }
        public string SteamId { get; }
        public MatchTeam Team { get; }
        public MatchPlayerStats Stats { get; private set; }
        public int RatingStart { get; private set; }
        public int RatingEnd { get; private set; }

        private MatchPlayer() {}

        internal MatchPlayer(UserId userId, string steamId, MatchTeam team, int rating)
        {
            UserId = userId;
            SteamId = steamId;
            Team = team;
            RatingStart = rating;
        }

        internal void UpdateStats(MatchPlayerStats stats) => Stats = stats;

        internal void CalculateNewRating(double score, int enemyRating)
        {
            var estimated = Math.Round(1.0 / (1 + Math.Pow(10, (enemyRating - RatingStart) / 400.0)), 2);

            RatingEnd = (int) Math.Round(RatingStart + GetK() * (score - estimated));
        }

        private int GetK() => RatingStart switch
        {
            < 1600 => 50,
            < 2200 => 32,
            _ => 16
        };
    }
}