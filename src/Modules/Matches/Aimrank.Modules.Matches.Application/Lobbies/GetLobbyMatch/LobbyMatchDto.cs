using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.GetLobbyMatch
{
    public class LobbyMatchDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Map { get; set; }
        public int Mode { get; set; }
        public int Status { get; set; }
    }
}