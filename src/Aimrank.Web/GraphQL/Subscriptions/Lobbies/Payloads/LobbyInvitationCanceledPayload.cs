using HotChocolate;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record LobbyInvitationCanceledPayload(
        [GraphQLNonNullType] LobbyInvitationCanceledRecord Record) : SubscriptionPayloadBase;

    public record LobbyInvitationCanceledRecord(Guid LobbyId, Guid InvitedPlayerId);
}