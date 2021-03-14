using Aimrank.Modules.Matches.Application.Lobbies.GetLobbyForUser;

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
}