using Aimrank.Common.Application.Events;
using System;

namespace Aimrank.Modules.CSGO.IntegrationEvents.External
{
    public class MatchFinishedEvent : IIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurredAt { get; }
        public Guid MatchId { get; }
    }
}