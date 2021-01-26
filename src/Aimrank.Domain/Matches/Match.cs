using System.Collections.Generic;
using System;

namespace Aimrank.Domain.Matches
{
    public class Match
    {
        public Guid Id { get; set; }
        public int ScoreT { get; set; }
        public int ScoreCT { get; set; }
        public List<MatchPlayer> Scoreboard { get; set; } = new();
        public DateTime CreatedAt { get; set; }

        private Match() {}
        
        public Match(Guid id, int scoreT, int scoreCt, List<MatchPlayer> scoreboard, DateTime createdAt)
        {
            Id = id;
            ScoreT = scoreT;
            ScoreCT = scoreCt;
            Scoreboard = scoreboard;
            CreatedAt = createdAt;
        }
    }
}