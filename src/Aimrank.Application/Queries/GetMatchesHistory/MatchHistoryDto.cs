using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.GetMatchesHistory
{
    public class MatchHistoryDto
    {
        public Guid Id { get; set; }
        public string Map { get; set; }
        public int ScoreT { get; set; }
        public int ScoreCT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public List<MatchHistoryPlayerDto> TeamTerrorists { get; set; }
        public List<MatchHistoryPlayerDto> TeamCounterTerrorists { get; set; }
    }

    public class MatchHistoryPlayerDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Kills { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int Score { get; set; }
    }
    
}