using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetMatch;
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