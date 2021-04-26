using Aimrank.Web.Modules.Cluster.Application.Contracts;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.AddSteamToken
{
    public class AddSteamTokenCommand : ICommand
    {
        public string Token { get; }

        public AddSteamTokenCommand(string token)
        {
            Token = token;
        }
    }
}