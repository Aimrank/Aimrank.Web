using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Users
{
    public record LobbyInvitationCreatedRecord(Guid LobbyId, Guid InvitingPlayerId, Guid InvitedPlayerId);
    public record LobbyInvitationCreatedPayload(LobbyInvitationCreatedRecord Record) : SubscriptionPayloadBase;

    public record FriendshipInvitationCreatedRecord(Guid InvitingUserId, Guid InvitedUserId);
    public record FriendshipInvitationCreatedPayload(FriendshipInvitationCreatedRecord Record) : SubscriptionPayloadBase;
}