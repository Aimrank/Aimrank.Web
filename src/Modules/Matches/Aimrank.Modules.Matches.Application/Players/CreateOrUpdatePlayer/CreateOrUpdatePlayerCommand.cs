using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Players.CreateOrUpdatePlayer
{
    public class CreateOrUpdatePlayerCommand : ICommand
    {
        public Guid PlayerId { get; }
        public string SteamId { get; }

        public CreateOrUpdatePlayerCommand(Guid playerId, string steamId)
        {
            PlayerId = playerId;
            SteamId = steamId;
        }
    }
}