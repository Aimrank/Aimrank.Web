using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetUserDetails;
using Aimrank.Application.Queries.GetUsers;
using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailsDto>>> Browse([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Ok(Enumerable.Empty<UserDetailsDto>());
            }
            
            var result = await _aimrankModule.ExecuteQueryAsync(new GetUsersQuery(name));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetails(Guid id)
        {
            var result = await _aimrankModule.ExecuteQueryAsync(new GetUserDetailsQuery(id));
            if (result is null)
            {
                return NotFound();
            }
            
            return result;
        }
    }
}