using HotChocolate;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record MatchReadyPayload(
        [GraphQLNonNullType] MatchReadyRecord Record) : SubscriptionPayloadBase;

    public record MatchReadyRecord(
        Guid MatchId,
        [GraphQLNonNullType] string Map,
        DateTime ExpiresAt);
}