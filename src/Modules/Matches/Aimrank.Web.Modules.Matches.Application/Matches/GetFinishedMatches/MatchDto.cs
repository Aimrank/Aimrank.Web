using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Matches.GetFinishedMatches
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public string Map { get; set; }
        public int Winner { get; set; }
        public int ScoreT { get; set; }
        public int ScoreCT { get; set; }
        public int Mode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public List<MatchPlayerDto> TeamTerrorists { get; set; }
        public List<MatchPlayerDto> TeamCounterTerrorists { get; set; }
    }
}