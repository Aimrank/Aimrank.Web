using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyMembers;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies
{
    public class LobbyEventSender
    {
        private readonly ITopicEventSender _topicEventSender;
        private readonly IMatchesModule _matchesModule;

        public LobbyEventSender(ITopicEventSender topicEventSender, IMatchesModule matchesModule)
        {
            _topicEventSender = topicEventSender;
            _matchesModule = matchesModule;
        }
        
        public async Task SendAsync<TMessage>(string topic, Guid lobbyId, TMessage message, CancellationToken cancellationToken = default)
        {
            var members = await _matchesModule.ExecuteQueryAsync(new GetLobbyMembersQuery(lobbyId));

            foreach (var memberId in members)
            {
                await _topicEventSender.SendAsync($"{topic}:{lobbyId}:{memberId}", message, cancellationToken);
            }
        }
    }
}