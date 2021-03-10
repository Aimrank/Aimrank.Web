using Aimrank.Application.Queries.Lobbies.GetLobbyForUser;
using Aimrank.Web.GraphQL.Queries.DataLoaders;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.Models
{
    public class LobbyMember
    {
        private readonly Guid _userId;
        
        public bool IsLeader { get; }

        public LobbyMember(LobbyMemberDto dto)
        {
            _userId = dto.UserId;
            IsLeader = dto.IsLeader;
        }

        public Task<User> GetUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_userId, CancellationToken.None);
    }
}