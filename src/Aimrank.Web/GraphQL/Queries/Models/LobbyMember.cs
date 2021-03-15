using Aimrank.Modules.Matches.Application.Lobbies.GetLobbyForUser;
using Aimrank.Web.GraphQL.Queries.DataLoaders;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.Models
{
    public class LobbyMember
    {
        private readonly Guid _playerId;
        
        public bool IsLeader { get; }

        public LobbyMember(LobbyMemberDto dto)
        {
            _playerId = dto.PlayerId;
            IsLeader = dto.IsLeader;
        }

        public Task<User> GetUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_playerId, CancellationToken.None);
    }
}