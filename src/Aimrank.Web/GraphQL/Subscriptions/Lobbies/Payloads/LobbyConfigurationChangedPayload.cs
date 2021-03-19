using HotChocolate;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record LobbyConfigurationChangedPayload(
        [GraphQLNonNullType] LobbyConfigurationChangedRecord Record) : SubscriptionPayloadBase;

    public record LobbyConfigurationChangedRecord(
        Guid LobbyId,
        [GraphQLNonNullType] string Map,
        [GraphQLNonNullType] string Name,
        int Mode);
}