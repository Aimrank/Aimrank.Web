using Aimrank.Web.Modules.Cluster.Application.Commands.CreatePod;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aimrank.Web.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClusterController : ControllerBase
    {
        private readonly IClusterModule _clusterModule;

        public ClusterController(IClusterModule clusterModule)
        {
            _clusterModule = clusterModule;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePod(CreatePodCommand command)
        {
            await _clusterModule.ExecuteCommandAsync(command);
            return Ok();
        }
    }
}