using Aimrank.Common.Domain;

namespace Aimrank.Domain.Matches.Events
{
    public class MatchStartedDomainEvent : IDomainEvent
    {
        public Match Match { get; }
        public string Map { get; }
        public string Address { get; }

        public MatchStartedDomainEvent(Match match, string map, string address)
        {
            Match = match;
            Map = map;
            Address = address;
        }
    }
}