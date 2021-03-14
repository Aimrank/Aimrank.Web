using Aimrank.Common.Application.Events;
using Aimrank.Common.Domain;
using Aimrank.Modules.Matches.Domain.Lobbies.Events;
using Aimrank.Modules.Matches.Domain.Matches.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using System.Linq;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration.Processing
{
    internal class EventMapper : IEventMapper
    {
        public IIntegrationEvent Map(IDomainEvent @event)
            => @event switch
            {
                MatchReadyDomainEvent e => new MatchReadyEvent(e.Match.Id, e.Map, e.Match.Lobbies.Select(l => l.LobbyId.Value)),
                MatchStartingDomainEvent e => new MatchStartingEvent(e.Match.Id, e.Match.Lobbies.Select(l => l.LobbyId.Value)),
                MatchStartedDomainEvent e => new MatchStartedEvent(e.Match.Id, e.Map, e.Address, (int) e.Match.Mode,
                    e.Match.Players.Select(p => p.UserId),
                    e.Match.Lobbies.Select(l => l.LobbyId.Value)),
                MatchFinishedDomainEvent e => new MatchFinishedEvent(e.Match.Id, e.ScoreT, e.ScoreCT,
                    e.Lobbies.Select(l => l.Value)),
                MatchPlayerLeftDomainEvent e => new MatchPlayerLeftEvent(e.Player.UserId),
                LobbyStatusChangedDomainEvent e => new LobbyStatusChangedEvent(e.Lobby.Id, (int) e.Lobby.Status),
                MemberLeftDomainEvent e => new MemberLeftEvent(e.Lobby.Id, e.Member.UserId),
                MemberRoleChangedDomainEvent e => new MemberRoleChangedEvent(e.Lobby.Id, e.Member.UserId,
                    (int) e.Member.Role),
                _ => null
            };
    }
}