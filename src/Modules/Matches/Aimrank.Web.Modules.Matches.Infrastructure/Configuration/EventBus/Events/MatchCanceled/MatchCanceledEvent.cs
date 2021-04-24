using Aimrank.Web.Common.Application.Events;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.MatchCanceled
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class MatchCanceledEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
        public Guid MatchId { get; set; }
    }
}