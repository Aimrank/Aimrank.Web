using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Collections.Generic;

namespace Aimrank.Domain.Lobbies
{
    public class LobbyMember : ValueObject
    {
        public UserId UserId { get; }
        public LobbyMemberRole Role { get; }

        public LobbyMember(UserId userId, LobbyMemberRole role)
        {
            UserId = userId;
            Role = role;
        }

        public LobbyMember PromoteToLeader() => new(UserId, LobbyMemberRole.Leader);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return Role;
        }
    }
}