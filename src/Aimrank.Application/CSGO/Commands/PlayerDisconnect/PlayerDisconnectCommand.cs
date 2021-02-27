using System;

namespace Aimrank.Application.CSGO.Commands.PlayerDisconnect
{
    public class PlayerDisconnectCommand : IServerEventCommand
    {
        public Guid MatchId { get; set; }
        public Guid SteamId { get; }

        public PlayerDisconnectCommand(Guid matchId, Guid steamId)
        {
            MatchId = matchId;
            SteamId = steamId;
        }
    }
}