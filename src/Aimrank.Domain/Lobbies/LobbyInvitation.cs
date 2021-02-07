using Aimrank.Domain.Users;
using System;

namespace Aimrank.Domain.Lobbies
{
    public record LobbyInvitation(UserId InvitingUserId, UserId InvitedUserId, DateTime CreatedAt);
}