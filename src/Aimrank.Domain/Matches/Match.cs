using Aimrank.Common.Domain;
using System.Collections.Generic;
using System;

namespace Aimrank.Domain.Matches
{
    public class Match : Entity
    {
        public MatchId Id { get; }
        public int ScoreT { get; set; }
        public int ScoreCT { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<MatchPlayer> Players { get; set; }

        private Match() {}

        public Match(MatchId id, int scoreT, int scoreCt, DateTime createdAt, List<MatchPlayer> players)
        {
            Id = id;
            ScoreT = scoreT;
            ScoreCT = scoreCt;
            CreatedAt = createdAt;
            Players = players;
        }
    }
}