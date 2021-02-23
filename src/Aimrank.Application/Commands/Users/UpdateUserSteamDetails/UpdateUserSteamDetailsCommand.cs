using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Users.UpdateUserSteamDetails
{
    public class UpdateUserSteamDetailsCommand : ICommand
    {
        public Guid UserId { get; }
        public string SteamId { get; }

        public UpdateUserSteamDetailsCommand(Guid userId, string steamId)
        {
            UserId = userId;
            SteamId = steamId;
        }
    }
}