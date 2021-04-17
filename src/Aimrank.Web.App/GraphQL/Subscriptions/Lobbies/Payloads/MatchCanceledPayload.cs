using HotChocolate;
using System;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record MatchCanceledPayload(
        [GraphQLNonNullType] MatchCanceledRecord Record) : SubscriptionPayloadBase;

    public record MatchCanceledRecord(Guid MatchId);
}