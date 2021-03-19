using HotChocolate;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record MatchStartedPayload(
        [GraphQLNonNullType] MatchStartedRecord Record) : SubscriptionPayloadBase;

    public record MatchStartedRecord(
        Guid MatchId,
        [GraphQLNonNullType] string Map,
        [GraphQLNonNullType] string Address,
        int Mode,
        IEnumerable<Guid> Players);
}