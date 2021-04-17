using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Matches.MarkPlayerAsLeaver
{
    public class MarkPlayerAsLeaverCommand : ICommand
    {
        public Guid MatchId { get; }
        public string SteamId { get; }

        public MarkPlayerAsLeaverCommand(Guid matchId, string steamId)
        {
            MatchId = matchId;
            SteamId = steamId;
        }
    }
}