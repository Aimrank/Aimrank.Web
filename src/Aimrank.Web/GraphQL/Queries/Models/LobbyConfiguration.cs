using Aimrank.Modules.Matches.Application.Lobbies.GetLobbyForUser;
using HotChocolate.Types;

namespace Aimrank.Web.GraphQL.Queries.Models
{
    public class LobbyConfiguration
    {
        public string Map { get; }
        public string Name { get; }
        public int Mode { get; }

        public LobbyConfiguration(LobbyConfigurationDto dto)
        {
            Map = dto.Map;
            Name = dto.Name;
            Mode = dto.Mode;
        }
    }

    public class LobbyConfigurationType : ObjectType<LobbyConfiguration>
    {
        protected override void Configure(IObjectTypeDescriptor<LobbyConfiguration> descriptor)
        {
            descriptor.Field(f => f.Map).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Name).Type<NonNullType<StringType>>();
        }
    }
}