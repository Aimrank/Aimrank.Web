using Aimrank.Web.Contracts.Requests;
using Aimrank.Web.Server;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly ServerProcessManager _serverProcessManager;

        public ServerController(ServerProcessManager serverProcessManager)
        {
            _serverProcessManager = serverProcessManager;
        }

        public IActionResult Get()
        {
            var processes = _serverProcessManager.GetProcesses();
            if (processes.Any())
            {
                return Ok(processes.Select(p => new
                {
                    p.Id,
                    p.Configuration.Port
                }));
            }

            return Ok(processes);
        }
        
        [HttpPost]
        public IActionResult Create(CreateServerRequest request)
        {
            var result = _serverProcessManager.StartServer(request.Id, request.Token);
            if (result)
            {
                return CreatedAtAction(nameof(Get), new {request.Id}, null);
            }

            return BadRequest();
        }

        [HttpPost("{id}/command")]
        public async Task<IActionResult> ExecuteCommand(Guid id, ExecuteCommandRequest request)
        {
            await _serverProcessManager.ExecuteCommandAsync(id, request.Command);
            
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _serverProcessManager.StopServerAsync(id);
            if (result)
            {
                return NoContent();
            }
            
            return NotFound();
        }
    }
}