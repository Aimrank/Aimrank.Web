using Aimrank.Common.Application.Events;
using Aimrank.Common.Domain;
using Aimrank.Domain.Matches.Events;
using Aimrank.IntegrationEvents;
using System.Linq;
using System;

namespace Aimrank.Infrastructure.Configuration.Processing
{
    internal class EventMapper : IEventMapper
    {
        public IntegrationEvent Map(IDomainEvent @event)
            => @event switch
            {
                MatchStartingDomainEvent e => new MatchStartingEvent(
                    Guid.NewGuid(), 
                    e.Match.Id.Value,
                    e.Match.Address,
                    e.Match.Map,
                    e.Match.Players.Select(p => p.UserId.Value),
                    DateTime.UtcNow),
                MatchStartedDomainEvent e => new MatchStartedEvent(
                    Guid.NewGuid(), 
                    e.Match.Id.Value,
                    e.Match.Address,
                    e.Match.Map,
                    e.Match.Players.Select(p => p.UserId.Value),
                    DateTime.UtcNow),
                MatchFinishedDomainEvent e => new MatchFinishedEvent(
                    Guid.NewGuid(),
                    e.Match.Id.Value,
                    e.Match.ScoreT,
                    e.Match.ScoreCT,
                    DateTime.UtcNow),
                _ => null
            };
    }
}