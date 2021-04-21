using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchStartedDomainEvent : DomainEvent
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