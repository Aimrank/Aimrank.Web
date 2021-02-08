using Aimrank.Common.Application.Events;
using Aimrank.Common.Domain;
using Aimrank.Domain.Lobbies.Events;
using Aimrank.Domain.Matches.Events;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.IntegrationEvents;
using System.Linq;

namespace Aimrank.Infrastructure.Configuration.Processing
{
    internal class EventMapper : IEventMapper
    {
        public IIntegrationEvent Map(IDomainEvent @event)
            => @event switch
            {
                MatchStartingDomainEvent e => new MatchStartingEvent(e.Match.Id, e.Match.Map, e.Match.Address,
                    e.Match.Players.Select(p => p.UserId.Value),
                    e.Match.Lobbies.Select(l => l.LobbyId.Value)),
                MatchStartedDomainEvent e => new MatchStartedEvent(e.Match.Id, e.Match.Map, e.Match.Address,
                    e.Match.Players.Select(p => p.UserId.Value),
                    e.Match.Lobbies.Select(l => l.LobbyId.Value)),
                MatchFinishedDomainEvent e => new MatchFinishedEvent(e.Match.Id, e.Match.ScoreT, e.Match.ScoreCT,
                    e.Lobbies.Select(l => l.Value)),
                LobbyConfigurationChangedDomainEvent e => new LobbyConfigurationChangedEvent(e.Lobby.Id,
                    e.Lobby.Configuration.Map, e.Lobby.Configuration.Name, (int) e.Lobby.Configuration.Mode),
                LobbyStatusChangedDomainEvent e => new LobbyStatusChangedEvent(e.Lobby.Id, (int) e.Lobby.Status),
                MemberLeftDomainEvent e => new MemberLeftEvent(e.Lobby.Id, e.Member.UserId),
                MemberRoleChangedDomainEvent e => new MemberRoleChangedEvent(e.Lobby.Id, e.Member.UserId,
                    (int) e.Member.Role),
                _ => null
            };
    }
}