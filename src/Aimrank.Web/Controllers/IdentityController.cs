using Aimrank.Application.Commands.Users.RefreshJwt;
using Aimrank.Application.Commands.Users.SignIn;
using Aimrank.Application.Commands.Users.SignUp;
using Aimrank.Application.Contracts;
using Aimrank.Web.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aimrank.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IAimrankModule _aimrankModule;

        public IdentityController(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<AuthenticationSuccessDto>> SignUp(SignUpRequest request)
        {
            var command = new SignUpCommand(request.Username, request.Email, request.Password);
            var result = await _aimrankModule.ExecuteCommandAsync(command);
            return result;
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<AuthenticationSuccessDto>> SignIn(SignInRequest request)
        {
            var command = new SignInCommand(request.UsernameOrEmail, request.Password);
            var result = await _aimrankModule.ExecuteCommandAsync(command);
            return result;
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthenticationSuccessDto>> RefreshToken(RefreshJwtRequest request)
        {
            var command = new RefreshJwtCommand(request.RefreshToken, request.Jwt);
            var result = await _aimrankModule.ExecuteCommandAsync(command);
            return result;
        }
    }
}