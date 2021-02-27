using System;

namespace Aimrank.Application.CSGO.Commands.PlayerDisconnect
{
    public class PlayerDisconnectCommand : IServerEventCommand
    {
        public Guid MatchId { get; set; }
        public string SteamId { get; }

        public PlayerDisconnectCommand(Guid matchId, string steamId)
        {
            MatchId = matchId;
            SteamId = steamId;
        }
    }
}