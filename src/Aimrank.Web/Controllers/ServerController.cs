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

        [HttpGet]
        public IActionResult GetAll()
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

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var process = _serverProcessManager.GetProcesses().FirstOrDefault(p => p.Id == id);
            if (process is null)
            {
                return NotFound();
            }

            return Ok(new
            {
                process.Id,
                process.Configuration.Port
            });
        }
        
        [HttpPost]
        public IActionResult Create(CreateServerRequest request)
        {
            var serverId = Guid.NewGuid();
            
            var result = _serverProcessManager.StartServer(serverId, request.Token, request.Whitelist);
            if (result)
            {
                return CreatedAtAction(nameof(Get), new {serverId}, null);
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