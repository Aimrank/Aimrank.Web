using HotChocolate;
using System;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record LobbyMemberKickedPayload(
        [GraphQLNonNullType] LobbyMemberKickedRecord Record) : SubscriptionPayloadBase;

    public record LobbyMemberKickedRecord(Guid LobbyId, Guid PlayerId);
}