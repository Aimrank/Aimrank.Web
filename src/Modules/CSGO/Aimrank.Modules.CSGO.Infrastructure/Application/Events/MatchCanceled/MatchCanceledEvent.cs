using Aimrank.Common.Application.Events;
using System;

namespace Aimrank.Modules.CSGO.Infrastructure.Application.Events.MatchCanceled
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class MatchCanceledEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public Guid MatchId { get; set; }
    }
}