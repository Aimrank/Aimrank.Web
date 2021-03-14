using System;

namespace Aimrank.Modules.Matches.IntegrationEvents.Matches
{
    public class MatchPlayerLeftEvent : IntegrationEventBase
    {
        public Guid UserId { get; }

        public MatchPlayerLeftEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}