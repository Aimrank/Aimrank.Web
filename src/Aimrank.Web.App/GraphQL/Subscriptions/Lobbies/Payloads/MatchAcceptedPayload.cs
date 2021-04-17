using HotChocolate;
using System;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record MatchAcceptedPayload(
        [GraphQLNonNullType] MatchAcceptedRecord Record) : SubscriptionPayloadBase;

    public record MatchAcceptedRecord(Guid MatchId, Guid PlayerId);
}