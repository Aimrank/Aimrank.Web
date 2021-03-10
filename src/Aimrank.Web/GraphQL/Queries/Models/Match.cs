using Aimrank.Application.Queries.Matches.GetFinishedMatches;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Web.GraphQL.Queries.Models
{
    public class Match
    {
        public Guid Id { get; }
        public string Map { get; }
        public int Winner { get; }
        public int ScoreT { get; }
        public int ScoreCT { get; }
        public int Mode { get; }
        public DateTime CreatedAt { get; }
        public DateTime FinishedAt { get; }
        public IEnumerable<MatchPlayer> TeamTerrorists { get; }
        public IEnumerable<MatchPlayer> TeamCounterTerrorists { get; }

        public Match(MatchDto dto)
        {
            Id = dto.Id;
            Map = dto.Map;
            Winner = dto.Winner;
            ScoreT = dto.ScoreT;
            ScoreCT = dto.ScoreCT;
            Mode = dto.Mode;
            CreatedAt = dto.CreatedAt;
            FinishedAt = dto.FinishedAt;
            TeamTerrorists = dto.TeamTerrorists.Select(p => new MatchPlayer(p));
            TeamCounterTerrorists = dto.TeamCounterTerrorists.Select(p => new MatchPlayer(p));
        }
    }

    public class MatchType : ObjectType<Match>
    {
    }
}