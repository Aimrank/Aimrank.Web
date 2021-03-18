using Aimrank.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
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
        public ValueTask<ISourceStream<MatchAcceptedEvent>> MatchAccepted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchAcceptedEvent>($"MatchAccepted:{lobbyId}");
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
        public ValueTask<ISourceStream<MatchStartingEvent>> MatchStarting(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchStartingEvent>($"MatchStarting:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchStartedEvent>> MatchStarted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchStartedEvent>($"MatchStarted:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchTimedOutEvent>> MatchTimedOut(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchTimedOutEvent>($"MatchTimedOut:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchCanceledEvent>> MatchCanceled(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchCanceledEvent>($"MatchCanceled:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchFinishedEvent>> MatchFinished(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchFinishedEvent>($"MatchFinished:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchPlayerLeftEvent>> MatchPlayerLeft(
            Guid playerId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MatchPlayerLeftEvent>($"MatchPlayerLeft:{playerId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<InvitationAcceptedPayload>> LobbyInvitationAccepted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, InvitationAcceptedPayload>($"LobbyInvitationAccepted:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<InvitationCanceledPayload>> LobbyInvitationCanceled(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, InvitationCanceledPayload>($"LobbyInvitationCanceled:{lobbyId}");
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
        public ValueTask<ISourceStream<LobbyStatusChangedEvent>> LobbyStatusChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, LobbyStatusChangedEvent>($"LobbyStatusChanged:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MemberLeftEvent>> LobbyMemberLeft(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MemberLeftEvent>($"LobbyMemberLeft:{lobbyId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MemberRoleChangedEvent>> LobbyMemberRoleChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal);
            return _receiver.SubscribeAsync<string, MemberRoleChangedEvent>($"LobbyMemberRoleChanged:{lobbyId}");
        }
    }
}