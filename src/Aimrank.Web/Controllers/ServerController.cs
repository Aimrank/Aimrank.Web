using Aimrank.Application.Commands.ExecuteServerCommand;
using Aimrank.Application.Commands.StartServer;
using Aimrank.Application.Commands.StopServer;
using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetServerProcesses;
using Aimrank.Web.Attributes;
using Aimrank.Web.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Controllers
{
    [JwtAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IAimrankModule _aimrankModule;

        public ServerController(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var processes = await _aimrankModule.ExecuteQueryAsync(new GetServerProcessesQuery());

            return Ok(processes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var process = await _aimrankModule.ExecuteQueryAsync(new GetServerProcessQuery(id));
            if (process is null)
            {
                return NotFound();
            }

            return Ok(process);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateServerRequest request)
        {
            var command = new StartServerCommand(Guid.NewGuid(), request.Map, request.Whitelist);

            await _aimrankModule.ExecuteCommandAsync(command);
            
            return CreatedAtAction(nameof(Get), new {Id = command.ServerId}, null);
        }

        [HttpPost("{id}/command")]
        public async Task<IActionResult> ExecuteCommand(Guid id, ExecuteCommandRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new ExecuteServerCommandCommand(id, request.Command));
            
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _aimrankModule.ExecuteCommandAsync(new StopServerCommand(id));

            return Accepted();
        }
    }
}