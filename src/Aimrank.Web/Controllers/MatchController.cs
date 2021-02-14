using Aimrank.Application.Commands.Matches.AcceptMatch;
using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetMatchesHistory;
using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Controllers
{
    [JwtAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IAimrankModule _aimrankModule;

        public MatchController(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchHistoryDto>>> Browse([FromQuery] Guid? userId)
        {
            var result = await _aimrankModule.ExecuteQueryAsync(new GetMatchesHistoryQuery(userId));
            return Ok(result);
        }

        [HttpPost("{id}/accept")]
        public async Task<IActionResult> Accept(Guid id)
        {
            await _aimrankModule.ExecuteCommandAsync(new AcceptMatchCommand(id));
            return Ok();
        }
    }
}