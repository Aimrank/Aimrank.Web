using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System;

namespace Aimrank.Domain.Lobbies
{
    public class LobbyInvitation : ValueObject
    {
        public UserId InvitingUserId { get; }
        public UserId InvitedUserId { get; }
        public DateTime CreatedAt { get; }

        public LobbyInvitation(UserId invitingUserId, UserId invitedUserId, DateTime createdAt)
        {
            InvitingUserId = invitingUserId;
            InvitedUserId = invitedUserId;
            CreatedAt = createdAt;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InvitingUserId;
            yield return InvitedUserId;
            yield return CreatedAt;
        }
    }
}