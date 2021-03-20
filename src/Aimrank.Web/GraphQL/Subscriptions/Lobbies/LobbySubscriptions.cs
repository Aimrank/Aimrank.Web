using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies
{
    [ExtendObjectType("Subscription")]
    public class LobbySubscriptions : AuthenticatedSubscriptions
    {
        private readonly ITopicEventReceiver _receiver;
        
        public LobbySubscriptions(ITopicEventReceiver receiver)
        {
            _receiver = receiver;
        }
        
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchAcceptedPayload>> MatchAccepted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchAcceptedPayload>($"MatchAccepted:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchReadyPayload>> MatchReady(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchReadyPayload>($"MatchReady:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchStartingPayload>> MatchStarting(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchStartingPayload>($"MatchStarting:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchStartedPayload>> MatchStarted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchStartedPayload>($"MatchStarted:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchTimedOutPayload>> MatchTimedOut(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchTimedOutPayload>($"MatchTimedOut:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchCanceledPayload>> MatchCanceled(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchCanceledPayload>($"MatchCanceled:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchFinishedPayload>> MatchFinished(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchFinishedPayload>($"MatchFinished:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchPlayerLeftPayload>> MatchPlayerLeft(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchPlayerLeftPayload>($"MatchPlayerLeft:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyInvitationAcceptedPayload>> LobbyInvitationAccepted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, LobbyInvitationAcceptedPayload>($"LobbyInvitationAccepted:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyInvitationCanceledPayload>> LobbyInvitationCanceled(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, LobbyInvitationCanceledPayload>($"LobbyInvitationCanceled:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyConfigurationChangedPayload>> LobbyConfigurationChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, LobbyConfigurationChangedPayload>($"LobbyConfigurationChanged:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyStatusChangedPayload>> LobbyStatusChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, LobbyStatusChangedPayload>($"LobbyStatusChanged:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyMemberLeftPayload>> LobbyMemberLeft(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, LobbyMemberLeftPayload>($"LobbyMemberLeft:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyMemberRoleChangedPayload>> LobbyMemberRoleChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, LobbyMemberRoleChangedPayload>($"LobbyMemberRoleChanged:{lobbyId}");
        }
    }
}