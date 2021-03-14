using Aimrank.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Modules.UserAccess.Application.Users.UpdateSteamDetails
{
    public class UpdateSteamDetailsCommand : ICommand
    {
        public Guid UserId { get; }
        public string SteamId { get; }

        public UpdateSteamDetailsCommand(Guid userId, string steamId)
        {
            UserId = userId;
            SteamId = steamId;
        }
    }
}