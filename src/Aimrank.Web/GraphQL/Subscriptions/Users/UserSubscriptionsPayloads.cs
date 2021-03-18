using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Users
{
    public record LobbyInvitationCreatedPayload(Guid LobbyId, Guid InvitingPlayerId, Guid InvitedPlayerId);
    
    public record FriendshipInvitationCreatedPayload(Guid InvitingUserId, Guid InvitedUserId);
}