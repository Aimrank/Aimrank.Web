using Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyForUser;
using Aimrank.Web.App.GraphQL.Queries.DataLoaders;
using HotChocolate.Types;
using HotChocolate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.App.GraphQL.Queries.Models
{
    public class Lobby
    {
        private readonly IEnumerable<LobbyMember> _members;

        public Guid Id { get; }
        public int Status { get; }
        public LobbyConfiguration Configuration { get; }
        
        public Lobby(LobbyDto dto)
        {
            Id = dto.Id;
            Status = dto.Status;
            Configuration = new LobbyConfiguration(dto.Configuration);
            _members = dto.Members.Select(m => new LobbyMember(m));
        }

        public Task<LobbyMatch> GetMatch([DataLoader] LobbyMatchDataLoader loader)
            => loader.LoadAsync(Id, CancellationToken.None);

        public IEnumerable<LobbyMember> GetMembers() => _members;
    }

    public class LobbyType : ObjectType<Lobby>
    {
        protected override void Configure(IObjectTypeDescriptor<Lobby> descriptor)
        {
            descriptor.Field(f => f.Configuration)
                .Type<NonNullType<LobbyConfigurationType>>();
            descriptor.Field(f => f.GetMembers())
                .Type<ListType<NonNullType<LobbyMemberType>>>();
        }
    }
}