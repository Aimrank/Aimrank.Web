using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.Matches.GetMatchesHistory
{
    public class MatchHistoryDto
    {
        public Guid Id { get; set; }
        public string Map { get; set; }
        public int ScoreT { get; set; }
        public int ScoreCT { get; set; }
        public int Mode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public List<MatchHistoryPlayerDto> TeamTerrorists { get; set; }
        public List<MatchHistoryPlayerDto> TeamCounterTerrorists { get; set; }
    }
}