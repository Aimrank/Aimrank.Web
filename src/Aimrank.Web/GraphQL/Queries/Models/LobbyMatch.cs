using Aimrank.Application.Queries.Lobbies.GetLobbyMatch;
using System;

namespace Aimrank.Web.GraphQL.Queries.Models
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
}