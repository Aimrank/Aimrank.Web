using HotChocolate;
using System;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record LobbyMemberLeftPayload(
        [GraphQLNonNullType] LobbyMemberLeftRecord Record) : SubscriptionPayloadBase;

    public record LobbyMemberLeftRecord(Guid LobbyId, Guid PlayerId);
}