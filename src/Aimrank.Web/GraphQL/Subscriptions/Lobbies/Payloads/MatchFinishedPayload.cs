using HotChocolate;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record MatchFinishedPayload(
        [GraphQLNonNullType] MatchFinishedRecord Record) : SubscriptionPayloadBase;

    public record MatchFinishedRecord(Guid MatchId, int ScoreT, int ScoreCT);
}