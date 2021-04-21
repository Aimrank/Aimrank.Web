using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchReadyDomainEvent : DomainEvent
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