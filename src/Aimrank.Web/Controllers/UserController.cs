using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetUserDetails;
using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Controllers
{
    [JwtAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAimrankModule _aimrankModule;

        public UserController(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetails(Guid id)
        {
            var result = await _aimrankModule.ExecuteQueryAsync(new GetUserDetailsQuery(id.ToString()));
            if (result is null)
            {
                return NotFound();
            }
            
            return result;
        }
    }
}