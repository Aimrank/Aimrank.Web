using System;

namespace Aimrank.Modules.Matches.Application.Matches.GetFinishedMatches
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