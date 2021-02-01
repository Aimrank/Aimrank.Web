using Aimrank.Common.Domain;
using System.Collections.Generic;
using System;

namespace Aimrank.Domain.Matches
{
    public class Match : Entity
    {
        public MatchId Id { get; }
        public int ScoreT { get; private set; }
        public int ScoreCT { get; private set; }
        public string Map { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime FinishedAt { get; private set; }
        public MatchStatus Status { get; private set; }
        public List<MatchPlayer> Players { get; private set; }

        private Match() {}

        public Match(MatchId id, string map, List<MatchPlayer> players)
        {
            Id = id;
            Map = map;
            Players = players;
            Status = MatchStatus.Started;
            CreatedAt = DateTime.UtcNow;
        }

        public void Finish(int scoreT, int scoreCT)
        {
            ScoreT = scoreT;
            ScoreCT = scoreCT;
            Status = MatchStatus.Finished;
            FinishedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            Status = MatchStatus.Canceled;
            FinishedAt = DateTime.UtcNow;
        }
    }
}