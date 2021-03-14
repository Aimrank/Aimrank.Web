using System;

namespace Aimrank.Modules.Matches.Application.CSGO.Commands.PlayerDisconnect
{
    public class PlayerDisconnectCommand : IServerEventCommand
    {
        public Guid MatchId { get; }
        public string SteamId { get; }

        public PlayerDisconnectCommand(Guid matchId, string steamId)
        {
            MatchId = matchId;
            SteamId = steamId;
        }
    }
}