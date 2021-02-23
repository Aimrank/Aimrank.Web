namespace Aimrank.Application.Queries.Matches.GetFinishedMatches
{
    public class FinishedMatchesFilter
    {
        public int? Mode { get; }
        public string Map { get; }

        public FinishedMatchesFilter(int? mode, string map)
        {
            Mode = mode;
            Map = map;
        }
    }
}