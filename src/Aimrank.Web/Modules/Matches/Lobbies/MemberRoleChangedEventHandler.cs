using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Lobbies
{
    public class MemberRoleChangedEventHandler : IIntegrationEventHandler<MemberRoleChangedEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public MemberRoleChangedEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(MemberRoleChangedEvent @event, CancellationToken cancellationToken = default)
            => await _lobbyEventSender.SendAsync($"LobbyMemberRoleChanged", @event.LobbyId,
                new LobbyMemberRoleChangedPayload(new LobbyMemberRoleChangedRecord(@event.PlayerId, @event.Role)),
                cancellationToken);
    }
}