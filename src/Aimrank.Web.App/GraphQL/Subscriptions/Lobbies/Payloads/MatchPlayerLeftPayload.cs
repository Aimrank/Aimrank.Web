using HotChocolate;
using System;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record MatchPlayerLeftPayload(
        [GraphQLNonNullType] MatchPlayerLeftRecord Record) : SubscriptionPayloadBase;

    public record MatchPlayerLeftRecord(Guid PlayerId);
}