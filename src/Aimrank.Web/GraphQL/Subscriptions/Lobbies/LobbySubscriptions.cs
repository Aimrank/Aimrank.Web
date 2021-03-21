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
    public class LobbySubscriptions
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
            => _receiver.SubscribeAsync<string, MatchAcceptedPayload>(
                $"MatchAccepted:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchReadyPayload>> MatchReady(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, MatchReadyPayload>(
                $"MatchReady:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchStartingPayload>> MatchStarting(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, MatchStartingPayload>(
                $"MatchStarting:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchStartedPayload>> MatchStarted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, MatchStartedPayload>(
                $"MatchStarted:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchTimedOutPayload>> MatchTimedOut(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, MatchTimedOutPayload>(
                $"MatchTimedOut:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchCanceledPayload>> MatchCanceled(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, MatchCanceledPayload>(
                $"MatchCanceled:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchFinishedPayload>> MatchFinished(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, MatchFinishedPayload>(
                $"MatchFinished:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchPlayerLeftPayload>> MatchPlayerLeft(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, MatchPlayerLeftPayload>(
                $"MatchPlayerLeft:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyInvitationAcceptedPayload>> LobbyInvitationAccepted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, LobbyInvitationAcceptedPayload>(
                $"LobbyInvitationAccepted:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyConfigurationChangedPayload>> LobbyConfigurationChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, LobbyConfigurationChangedPayload>(
                $"LobbyConfigurationChanged:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyStatusChangedPayload>> LobbyStatusChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, LobbyStatusChangedPayload>(
                $"LobbyStatusChanged:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyMemberLeftPayload>> LobbyMemberLeft(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, LobbyMemberLeftPayload>(
                $"LobbyMemberLeft:{lobbyId}:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyMemberRoleChangedPayload>> LobbyMemberRoleChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, LobbyMemberRoleChangedPayload>(
                $"LobbyMemberRoleChanged:{lobbyId}:{principal.GetUserId()}");
    }
}