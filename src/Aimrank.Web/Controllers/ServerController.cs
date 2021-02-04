using Aimrank.Application.Commands.ProcessServerEvent;
using Aimrank.Application.Contracts;
using Aimrank.Web.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aimrank.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IAimrankModule _aimrankModule;

        public ServerController(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessServerEvent(ProcessServerEventRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new ProcessServerEventCommand(
                request.ServerId,
                request.Name,
                request.Data));
            
            return Accepted();
        }
    }
}