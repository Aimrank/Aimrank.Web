using Aimrank.Web.Modules.CSGO.Application.Commands.CreatePod;
using Aimrank.Web.Modules.CSGO.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aimrank.Web.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClusterController : ControllerBase
    {
        private readonly ICSGOModule _csgoModule;

        public ClusterController(ICSGOModule csgoModule)
        {
            _csgoModule = csgoModule;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePod(CreatePodCommand command)
        {
            await _csgoModule.ExecuteCommandAsync(command);
            return Ok();
        }
    }
}