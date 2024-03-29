using HotChocolate;
using System;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record LobbyMemberRoleChangedPayload(
        [GraphQLNonNullType] LobbyMemberRoleChangedRecord Record) : SubscriptionPayloadBase;

    public record LobbyMemberRoleChangedRecord(Guid PlayerId, int Role);
}