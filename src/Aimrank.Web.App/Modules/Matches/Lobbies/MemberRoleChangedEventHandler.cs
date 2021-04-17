using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.Modules.Matches.Lobbies
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