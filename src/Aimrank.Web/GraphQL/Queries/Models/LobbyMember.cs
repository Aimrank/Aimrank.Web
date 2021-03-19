using Aimrank.Modules.Matches.Application.Lobbies.GetLobbyForUser;
using Aimrank.Web.GraphQL.Queries.DataLoaders;
using HotChocolate.Types;
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
    
    public class LobbyMemberType : ObjectType<LobbyMember>
    {
        protected override void Configure(IObjectTypeDescriptor<LobbyMember> descriptor)
        {
            descriptor.Field(f => f.GetUser(default))
                .Type<NonNullType<UserType>>();
        }
    }
}