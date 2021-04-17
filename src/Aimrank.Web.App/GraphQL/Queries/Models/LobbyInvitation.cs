using Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyInvitations;
using Aimrank.Web.App.GraphQL.Queries.DataLoaders;
using HotChocolate.Types;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.App.GraphQL.Queries.Models
{
    public class LobbyInvitation
    {
        private readonly Guid _invitingPlayerId;
        private readonly Guid _invitedPlayerId;

        public Guid LobbyId { get; }
        public DateTime CreatedAt { get; }

        public LobbyInvitation(LobbyInvitationDto dto)
        {
            LobbyId = dto.LobbyId;
            CreatedAt = dto.CreatedAt;
            _invitingPlayerId = dto.InvitingPlayerId;
            _invitedPlayerId = dto.InvitedPlayerId;
        }

        public Task<User> GetInvitingUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_invitingPlayerId, CancellationToken.None);

        public Task<User> GetInvitedUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_invitedPlayerId, CancellationToken.None);
    }

    public class LobbyInvitationType : ObjectType<LobbyInvitation>
    {
    }
}