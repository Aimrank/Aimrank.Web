using Aimrank.Application.Commands.ProcessServerEvent;
using Aimrank.Application.Contracts;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        private readonly IAimrankModule _aimrankModule;

        public GameHub(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        public async Task PublishEvent(string content)
        {
            await _aimrankModule.ExecuteCommandAsync(new ProcessServerEventCommand(content));
        }
    }
}