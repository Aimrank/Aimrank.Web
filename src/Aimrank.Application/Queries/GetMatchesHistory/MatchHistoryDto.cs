using System;

namespace Aimrank.Application.Queries.GetMatchesHistory
{
    public class MatchHistoryDto
    {
        public Guid Id { get; init; }
        public int ScoreT { get; init; }
        public int ScoreCT { get; init; }
        public string TeamNameT { get; init; }
        public string TeamNameCT { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}