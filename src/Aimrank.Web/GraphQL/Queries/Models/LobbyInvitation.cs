using Aimrank.Application.Queries.Lobbies.GetLobbyInvitations;
using Aimrank.Web.GraphQL.Queries.DataLoaders;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.Models
{
    public class LobbyInvitation
    {
        private readonly Guid _invitingUserId;
        private readonly Guid _invitedUserId;
        
        public DateTime CreatedAt { get; }

        public LobbyInvitation(LobbyInvitationDto dto)
        {
            _invitingUserId = dto.InvitingUserId;
            _invitedUserId = dto.InvitedUserId;
            CreatedAt = dto.CreatedAt;
        }

        public Task<User> GetInvitingUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_invitingUserId, CancellationToken.None);

        public Task<User> GetInvitedUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_invitedUserId, CancellationToken.None);
    }
}