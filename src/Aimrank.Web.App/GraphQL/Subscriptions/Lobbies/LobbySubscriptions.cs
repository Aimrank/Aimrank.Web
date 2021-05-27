using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies
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
            => SubscribeAuthenticated<MatchAcceptedPayload>(
                $"MatchAccepted:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchReadyPayload>> MatchReady(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<MatchReadyPayload>(
                $"MatchReady:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchStartedPayload>> MatchStarted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<MatchStartedPayload>(
                $"MatchStarted:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchTimedOutPayload>> MatchTimedOut(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<MatchTimedOutPayload>(
                $"MatchTimedOut:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchCanceledPayload>> MatchCanceled(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<MatchCanceledPayload>(
                $"MatchCanceled:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchFinishedPayload>> MatchFinished(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<MatchFinishedPayload>(
                $"MatchFinished:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<MatchPlayerLeftPayload>> MatchPlayerLeft(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<MatchPlayerLeftPayload>(
                $"MatchPlayerLeft:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyInvitationAcceptedPayload>> LobbyInvitationAccepted(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<LobbyInvitationAcceptedPayload>(
                $"LobbyInvitationAccepted:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyConfigurationChangedPayload>> LobbyConfigurationChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<LobbyConfigurationChangedPayload>(
                $"LobbyConfigurationChanged:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyStatusChangedPayload>> LobbyStatusChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<LobbyStatusChangedPayload>(
                $"LobbyStatusChanged:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyMemberLeftPayload>> LobbyMemberLeft(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<LobbyMemberLeftPayload>(
                $"LobbyMemberLeft:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyMemberRoleChangedPayload>> LobbyMemberRoleChanged(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<LobbyMemberRoleChangedPayload>(
                $"LobbyMemberRoleChanged:{lobbyId}:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyMemberKickedPayload>> LobbyMemberKicked(
            Guid lobbyId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<LobbyMemberKickedPayload>(
                $"LobbyMemberKicked:{lobbyId}:{principal.GetUserId()}", principal);

        private ValueTask<ISourceStream<TMessage>> SubscribeAuthenticated<TMessage>(string topic,
            ClaimsPrincipal principal)
            => principal.GetUserId() == Guid.Empty
                ? new ValueTask<ISourceStream<TMessage>>(new EmptySourceStream<TMessage>())
                : _receiver.SubscribeAsync<string, TMessage>(topic);
    }
}