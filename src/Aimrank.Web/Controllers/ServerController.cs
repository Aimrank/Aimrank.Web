using Aimrank.Modules.Matches.Application.CSGO.Commands.ProcessServerEvent;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Web.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aimrank.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IMatchesModule _matchesModule;

        public ServerController(IMatchesModule matchesModule)
        {
            _matchesModule = matchesModule;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessServerEvent(ProcessServerEventRequest request)
        {
            await _matchesModule.ExecuteCommandAsync(new ProcessServerEventCommand(
                request.MatchId,
                request.Name,
                request.Data));
            
            return Accepted();
        }
    }
}