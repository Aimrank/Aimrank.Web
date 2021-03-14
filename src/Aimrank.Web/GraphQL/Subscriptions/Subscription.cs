using Aimrank.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies;
using Aimrank.Web.GraphQL.Subscriptions.Messages.Users;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions
{
    [Authorize]
    public class Subscription
    {
        private readonly ITopicEventReceiver _topicEventReceiver;

        public Subscription(ITopicEventReceiver topicEventReceiver)
        {
            _topicEventReceiver = topicEventReceiver;
        }

        // User
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyInvitationCreatedMessage>> LobbyInvitationCreated(Guid userId)
            => _topicEventReceiver.SubscribeAsync<string, LobbyInvitationCreatedMessage>(
                $"LobbyInvitationCreated:{userId}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<FriendshipInvitationCreatedMessage>> FriendshipInvitationCreated(Guid userId)
            => _topicEventReceiver.SubscribeAsync<string, FriendshipInvitationCreatedMessage>(
                $"FriendshipInvitationCreated:{userId}");
        
        // Lobby

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchAcceptedEvent>> MatchAccepted(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, MatchAcceptedEvent>($"MatchAccepted:{lobbyId}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchReadyMessage>> MatchReady(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, MatchReadyMessage>($"MatchReady:{lobbyId}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchStartingEvent>> MatchStarting(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, MatchStartingEvent>($"MatchStarting:{lobbyId}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchStartedEvent>> MatchStarted(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, MatchStartedEvent>($"MatchStarted:{lobbyId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchTimedOutEvent>> MatchTimedOut(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, MatchTimedOutEvent>($"MatchTimedOut:{lobbyId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchCanceledEvent>> MatchCanceled(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, MatchCanceledEvent>($"MatchCanceled:{lobbyId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchFinishedEvent>> MatchFinished(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, MatchFinishedEvent>($"MatchFinished:{lobbyId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchPlayerLeftEvent>> MatchPlayerLeft(Guid userId)
            => _topicEventReceiver.SubscribeAsync<string, MatchPlayerLeftEvent>($"MatchPlayerLeft:{userId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<InvitationAcceptedMessage>> LobbyInvitationAccepted(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, InvitationAcceptedMessage>($"LobbyInvitationAccepted:{lobbyId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<InvitationCanceledMessage>> LobbyInvitationCanceled(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, InvitationCanceledMessage>($"LobbyInvitationCanceled:{lobbyId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyConfigurationChangedMessage>> LobbyConfigurationChanged(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, LobbyConfigurationChangedMessage>($"LobbyConfigurationChanged:{lobbyId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyStatusChangedEvent>> LobbyStatusChanged(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, LobbyStatusChangedEvent>($"LobbyStatusChanged:{lobbyId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MemberLeftEvent>> LobbyMemberLeft(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, MemberLeftEvent>($"LobbyMemberLeft:{lobbyId}");
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MemberRoleChangedEvent>> LobbyMemberRoleChanged(Guid lobbyId)
            => _topicEventReceiver.SubscribeAsync<string, MemberRoleChangedEvent>($"LobbyMemberRoleChanged:{lobbyId}");
    }
}