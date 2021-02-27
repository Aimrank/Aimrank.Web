using System;

namespace Aimrank.IntegrationEvents.Matches
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