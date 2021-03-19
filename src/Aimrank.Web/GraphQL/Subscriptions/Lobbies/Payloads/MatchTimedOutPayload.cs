using HotChocolate;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record MatchTimedOutPayload(
        [GraphQLNonNullType] MatchTimedOutRecord Record) : SubscriptionPayloadBase;

    public record MatchTimedOutRecord(Guid MatchId);
}