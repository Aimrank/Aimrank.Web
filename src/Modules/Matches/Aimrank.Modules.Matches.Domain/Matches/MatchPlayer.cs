using Aimrank.Common.Domain;
using Aimrank.Modules.Matches.Domain.Matches.Events;
using System;

namespace Aimrank.Modules.Matches.Domain.Matches
{
    public class MatchPlayer : Entity
    {
        public Guid UserId { get; }
        public string SteamId { get; }
        public MatchTeam Team { get; }
        public MatchPlayerStats Stats { get; private set; }
        public int RatingStart { get; private set; }
        public int RatingEnd { get; private set; }
        public bool IsLeaver { get; private set; }

        private MatchPlayer() {}

        internal MatchPlayer(Guid userId, string steamId, MatchTeam team, int rating)
        {
            UserId = userId;
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
            
            AddDomainEvent(new MatchPlayerLeftDomainEvent(this));
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