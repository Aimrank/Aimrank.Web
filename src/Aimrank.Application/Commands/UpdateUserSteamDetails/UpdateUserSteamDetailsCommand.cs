using Aimrank.Application.Contracts;

namespace Aimrank.Application.Commands.UpdateUserSteamDetails
{
    public class UpdateUserSteamDetailsCommand : ICommand
    {
        public string UserId { get; }
        public string SteamId { get; }

        public UpdateUserSteamDetailsCommand(string userId, string steamId)
        {
            UserId = userId;
            SteamId = steamId;
        }
    }
}