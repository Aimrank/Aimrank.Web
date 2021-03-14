using Aimrank.Common.Domain;
using System.Collections.Generic;
using System;

namespace Aimrank.Modules.Matches.Domain.Lobbies
{
    public class LobbyMember : ValueObject
    {
        public Guid UserId { get; }
        public LobbyMemberRole Role { get; }

        public LobbyMember(Guid userId, LobbyMemberRole role)
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