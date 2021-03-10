using System;

namespace Aimrank.Application.Queries.Matches.GetFinishedMatches
{
    public record FinishedMatchesFilter
    {
        public Guid UserId { get; }
        public int? Mode { get; }
        public string Map { get; }

        public FinishedMatchesFilter(Guid userId, int? mode, string map)
        {
            UserId = userId;
            Mode = mode;
            Map = map;
        }
    }
}