using HotChocolate;
using System;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record LobbyStatusChangedPayload(
        [GraphQLNonNullType] LobbyStatusChangedRecord Record) : SubscriptionPayloadBase;

    public record LobbyStatusChangedRecord(Guid LobbyId, int Status);
}