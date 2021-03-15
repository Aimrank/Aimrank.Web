using Aimrank.Modules.Matches.Application.Lobbies.GetLobbyInvitations;
using Aimrank.Web.GraphQL.Queries.DataLoaders;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.Models
{
    public class LobbyInvitation
    {
        private readonly Guid _invitingPlayerId;
        private readonly Guid _invitedPlayerId;
        
        public DateTime CreatedAt { get; }

        public LobbyInvitation(LobbyInvitationDto dto)
        {
            _invitingPlayerId = dto.InvitingPlayerId;
            _invitedPlayerId = dto.InvitedPlayerId;
            CreatedAt = dto.CreatedAt;
        }

        public Task<User> GetInvitingUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_invitingPlayerId, CancellationToken.None);

        public Task<User> GetInvitedUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_invitedPlayerId, CancellationToken.None);
    }
}