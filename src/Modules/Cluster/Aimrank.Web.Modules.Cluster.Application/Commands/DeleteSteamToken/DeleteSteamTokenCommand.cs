using Aimrank.Web.Modules.Cluster.Application.Contracts;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.DeleteSteamToken
{
    public class DeleteSteamTokenCommand : ICommand
    {
        public string Token { get; }

        public DeleteSteamTokenCommand(string token)
        {
            Token = token;
        }
    }
}