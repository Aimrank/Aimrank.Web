using Aimrank.Web.Common.Application.Events;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Events.Events.PlayerDisconnected
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class PlayerDisconnectedEvent : IIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public Guid MatchId { get; }
        public string SteamId { get; }

        public PlayerDisconnectedEvent(Guid id, DateTime occurredOn, Guid matchId, string steamId)
        {
            Id = id;
            OccurredOn = occurredOn;
            MatchId = matchId;
            SteamId = steamId;
        }
    }
}