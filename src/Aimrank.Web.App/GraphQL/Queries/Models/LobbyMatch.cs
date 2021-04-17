using Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyMatch;
using HotChocolate.Types;
using System;

namespace Aimrank.Web.App.GraphQL.Queries.Models
{
    public class LobbyMatch
    {
        public Guid Id { get; }
        public string Address { get; }
        public string Map { get; }
        public int Mode { get; }
        public int Status { get; }

        public LobbyMatch(LobbyMatchDto dto)
        {
            Id = dto.Id;
            Address = dto.Address;
            Map = dto.Map;
            Mode = dto.Mode;
            Status = dto.Status;
        }
    }

    public class LobbyMatchType : ObjectType<LobbyMatch>
    {
        protected override void Configure(IObjectTypeDescriptor<LobbyMatch> descriptor)
        {
            descriptor.Field(f => f.Address).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Map).Type<NonNullType<StringType>>();
        }
    }
}