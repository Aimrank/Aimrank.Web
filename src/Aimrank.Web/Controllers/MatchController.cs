using Aimrank.Application.Commands.Matches.AcceptMatch;
using Aimrank.Application.Contracts;
using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("{id}/accept")]
        public async Task<IActionResult> Accept(Guid id)
        {
            await _aimrankModule.ExecuteCommandAsync(new AcceptMatchCommand(id));
            return Ok();
        }
    }
}