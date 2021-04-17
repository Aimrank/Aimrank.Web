using Aimrank.Web.Modules.CSGO.Application.Contracts;

namespace Aimrank.Web.Modules.CSGO.Application.Commands.CreatePod
{
    public class CreatePodCommand : ICommand
    {
        public string IpAddress { get; }
        public int MaxServers { get; }

        public CreatePodCommand(string ipAddress, int maxServers)
        {
            IpAddress = ipAddress;
            MaxServers = maxServers;
        }
    }
}