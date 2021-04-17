using Aimrank.Web.Common.Application.Events;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.PlayerDisconnected
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class PlayerDisconnectedEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public Guid MatchId { get; set; }
        public string SteamId { get; set; }
    }
}