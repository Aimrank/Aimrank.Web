using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies
{
    public class LobbyMember : ValueObject
    {
        public PlayerId PlayerId { get; }
        public LobbyMemberRole Role { get; }

        public LobbyMember(PlayerId playerId, LobbyMemberRole role)
        {
            PlayerId = playerId;
            Role = role;
        }

        public LobbyMember PromoteToLeader() => new(PlayerId, LobbyMemberRole.Leader);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PlayerId;
            yield return Role;
        }
    }
}