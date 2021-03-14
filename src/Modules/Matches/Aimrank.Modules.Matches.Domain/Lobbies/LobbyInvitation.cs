using Aimrank.Common.Domain;
using System.Collections.Generic;
using System;

namespace Aimrank.Modules.Matches.Domain.Lobbies
{
    public class LobbyInvitation : ValueObject
    {
        public Guid InvitingUserId { get; }
        public Guid InvitedUserId { get; }
        public DateTime CreatedAt { get; }

        public LobbyInvitation(Guid invitingUserId, Guid invitedUserId, DateTime createdAt)
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