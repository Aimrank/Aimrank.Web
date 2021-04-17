using System;

namespace Aimrank.Web.Modules.Matches.Application.Matches.GetFinishedMatches
{
    public record FinishedMatchesFilter
    {
        public Guid PlayerId { get; }
        public int? Mode { get; }
        public string Map { get; }

        public FinishedMatchesFilter(Guid playerId, int? mode, string map)
        {
            PlayerId = playerId;
            Mode = mode;
            Map = map;
        }
    }
}