using Aimrank.Domain.Users;

namespace Aimrank.Domain.Lobbies
{
    public record LobbyMember(UserId UserId, LobbyMemberRole Role);
}