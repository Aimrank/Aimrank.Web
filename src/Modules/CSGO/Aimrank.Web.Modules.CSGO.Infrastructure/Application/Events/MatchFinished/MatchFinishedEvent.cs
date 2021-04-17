using Aimrank.Web.Common.Application.Events;
using System;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Application.Events.MatchFinished
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class MatchFinishedEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public Guid MatchId { get; set; }
    }
}