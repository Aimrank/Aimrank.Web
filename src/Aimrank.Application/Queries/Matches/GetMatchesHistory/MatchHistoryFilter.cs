namespace Aimrank.Application.Queries.Matches.GetMatchesHistory
{
    public class MatchHistoryFilter
    {
        public int? Mode { get; }
        public string Map { get; }

        public MatchHistoryFilter(int? mode, string map)
        {
            Mode = mode;
            Map = map;
        }
    }
}