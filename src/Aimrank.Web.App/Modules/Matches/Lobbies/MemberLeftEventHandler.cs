using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.Modules.Matches.Lobbies
{
    public class MemberLeftEventHandler : IIntegrationEventHandler<MemberLeftEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public MemberLeftEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(MemberLeftEvent @event, CancellationToken cancellationToken = default)
            => await _lobbyEventSender.SendAsync($"LobbyMemberLeft", @event.LobbyId,
                new LobbyMemberLeftPayload(new LobbyMemberLeftRecord(@event.LobbyId, @event.PlayerId)),
                cancellationToken);
    }
}