using Aimrank.Web.Contracts.Requests;
using Aimrank.Web.Server;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
                    Status = Enum.GetName(typeof(ServerProcessStatus), p.Status)
                }));
            }

            return Ok(processes);
        }
        
        [HttpPost]
        public IActionResult Create(CreateServerRequest request)
        {
            var result = _serverProcessManager.StartServer(request.Id);
            if (result)
            {
                return CreatedAtAction(nameof(Get), new {request.Id}, null);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _serverProcessManager.StopServer(id);
            if (result)
            {
                return NoContent();
            }
            
            return NotFound();
        }
    }
}