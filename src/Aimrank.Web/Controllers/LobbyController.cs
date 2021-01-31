using Aimrank.Application.Commands.ChangeLobbyMap;
using Aimrank.Application.Commands.CreateLobby;
using Aimrank.Application.Commands.JoinLobby;
using Aimrank.Application.Commands.LeaveLobby;
using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetLobby;
using Aimrank.Application.Queries.GetOpenedLobbies;
using Aimrank.Web.Attributes;
using Aimrank.Web.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Controllers
{
    [JwtAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly IAimrankModule _aimrankModule;

        public LobbyController(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LobbyDto>>> GetOpened()
        {
            var lobbies = await _aimrankModule.ExecuteQueryAsync(new GetOpenedLobbiesQuery());
            return Ok(lobbies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LobbyDto>> Get(Guid id)
        {
            var lobby = await _aimrankModule.ExecuteQueryAsync(new GetLobbyQuery(id));
            if (lobby is null)
            {
                return NotFound();
            }
            
            return lobby;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var lobbyId = Guid.NewGuid();
            await _aimrankModule.ExecuteCommandAsync(new CreateLobbyCommand(lobbyId));
            return CreatedAtAction(nameof(Get), new {Id = lobbyId}, null);
        }

        [HttpPost("{id}/members")]
        public async Task<IActionResult> Join(Guid id)
        {
            await _aimrankModule.ExecuteCommandAsync(new JoinLobbyCommand(id));
            return Ok();
        }

        [HttpDelete("{id}/members")]
        public async Task<IActionResult> Leave(Guid id)
        {
            await _aimrankModule.ExecuteCommandAsync(new LeaveLobbyCommand(id));
            return Ok();
        }

        [HttpPost("{id}/map")]
        public async Task<IActionResult> ChangeMap(Guid id, ChangeLobbyMapRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new ChangeLobbyMapCommand(id, request.Name));
            return Ok();
        }
    }
}