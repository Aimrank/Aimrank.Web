using Aimrank.Common.Domain;
using Aimrank.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System;

namespace Aimrank.Modules.Matches.Domain.Lobbies
{
    public class LobbyInvitation : ValueObject
    {
        public PlayerId InvitingPlayerId { get; }
        public PlayerId InvitedPlayerId { get; }
        public DateTime CreatedAt { get; }

        public LobbyInvitation(PlayerId invitingPlayerId, PlayerId invitedPlayerId, DateTime createdAt)
        {
            InvitingPlayerId = invitingPlayerId;
            InvitedPlayerId = invitedPlayerId;
            CreatedAt = createdAt;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InvitingPlayerId;
            yield return InvitedPlayerId;
            yield return CreatedAt;
        }
    }
}