using Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyForUser;
using HotChocolate.Types;
using System.Collections.Generic;

namespace Aimrank.Web.App.GraphQL.Queries.Models
{
    public class LobbyConfiguration
    {
        public IEnumerable<string> Maps { get; }
        public string Name { get; }
        public int Mode { get; }

        public LobbyConfiguration(LobbyConfigurationDto dto)
        {
            Maps = dto.Maps.Split(',');
            Name = dto.Name;
            Mode = dto.Mode;
        }
    }

    public class LobbyConfigurationType : ObjectType<LobbyConfiguration>
    {
        protected override void Configure(IObjectTypeDescriptor<LobbyConfiguration> descriptor)
        {
            descriptor.Field(f => f.Maps).Type<NonNullType<ListType<NonNullType<StringType>>>>();
            descriptor.Field(f => f.Name).Type<NonNullType<StringType>>();
        }
    }
}