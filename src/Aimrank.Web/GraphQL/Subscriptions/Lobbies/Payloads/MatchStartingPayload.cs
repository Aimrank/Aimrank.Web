using HotChocolate;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record MatchStartingPayload(
        [GraphQLNonNullType] MatchStartingRecord Record) : SubscriptionPayloadBase;

    public record MatchStartingRecord(Guid MatchId);
}