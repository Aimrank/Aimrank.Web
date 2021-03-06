using Aimrank.Common.Domain;

namespace Aimrank.Domain.Matches.Events
{
    public class MatchReadyDomainEvent : IDomainEvent
    {
        public Match Match { get; }
        public string Map { get; }

        public MatchReadyDomainEvent(Match match, string map)
        {
            Match = match;
            Map = map;
        }
    }
}