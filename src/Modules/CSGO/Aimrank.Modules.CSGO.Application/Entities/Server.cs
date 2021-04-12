using System;

namespace Aimrank.Modules.CSGO.Application.Entities
{
    public class Server
    {
        public Pod Pod { get; set; }
        public Guid MatchId { get; set; }
        public SteamToken SteamToken { get; set; }
        public bool IsAccepted { get; set; }
    }
}