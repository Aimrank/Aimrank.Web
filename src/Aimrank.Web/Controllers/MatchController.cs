using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetMatch;
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
        
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDto>> GetById(Guid id)
        {
            var result = await _aimrankModule.ExecuteQueryAsync(new GetMatchQuery(id));
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}