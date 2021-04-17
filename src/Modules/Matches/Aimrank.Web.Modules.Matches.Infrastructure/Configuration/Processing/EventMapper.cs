using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Lobbies.Events;
using Aimrank.Web.Modules.Matches.Domain.Matches.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using System.Linq;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing
{
    internal class EventMapper : IEventMapper
    {
        public IIntegrationEvent Map(IDomainEvent @event)
            => @event switch
            {
                MatchReadyDomainEvent e => new MatchReadyEvent(e.Match.Id, e.Map, e.Match.Lobbies.Select(l => l.LobbyId.Value)),
                MatchStartingDomainEvent e => new MatchStartingEvent(e.Match.Id, e.Match.Lobbies.Select(l => l.LobbyId.Value)),
                MatchStartedDomainEvent e => new MatchStartedEvent(e.Match.Id, e.Map, e.Address, (int) e.Match.Mode,
                    e.Match.Players.Select(p => p.PlayerId.Value),
                    e.Match.Lobbies.Select(l => l.LobbyId.Value)),
                MatchFinishedDomainEvent e => new MatchFinishedEvent(e.Match.Id, e.ScoreT, e.ScoreCT,
                    e.Lobbies.Select(l => l.Value)),
                MatchPlayerLeftDomainEvent e => new MatchPlayerLeftEvent(e.Player.PlayerId, e.Lobbies.Select(l => l.LobbyId.Value)),
                LobbyStatusChangedDomainEvent e => new LobbyStatusChangedEvent(e.Lobby.Id, (int) e.Lobby.Status),
                MemberLeftDomainEvent e => new MemberLeftEvent(e.Lobby.Id, e.Member.PlayerId),
                MemberRoleChangedDomainEvent e => new MemberRoleChangedEvent(e.Lobby.Id, e.Member.PlayerId,
                    (int) e.Member.Role),
                _ => null
            };
    }
}