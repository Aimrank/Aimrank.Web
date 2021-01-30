using Aimrank.Application.Contracts;
using Aimrank.Application;
using Aimrank.Web.Attributes;
using Aimrank.Web.Contracts.Responses;
using Aimrank.Web.Steam;
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteamController : ControllerBase
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IAimrankModule _aimrankModule;

        public SteamController(
            IExecutionContextAccessor executionContextAccessor,
            IAimrankModule aimrankModule)
        {
            _executionContextAccessor = executionContextAccessor;
            _aimrankModule = aimrankModule;
        }

        [Authorize(AuthenticationSchemes = SteamAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet("openid")]
        public async Task<IActionResult> SignInWithSteamSuccess()
        {
            var result = await HttpContext.AuthenticateAsync(SteamAuthenticationDefaults.AuthenticationScheme);
            
            // Check if userId exists
            
            var data = HttpContext.GetSteamData();
            Console.WriteLine(result.Properties?.Items["userId"]);
            Console.WriteLine(data.Id);

            // var command = new UpdateUserSteamDetailsCommand(id, data.Id);
            // await _aimrankModule.ExecuteCommandAsync(command);
            
            return Redirect("/settings");
        }
        
        [JwtAuth]
        [HttpPost("openid")]
        public async Task<ActionResult<SteamSignInResponse>> SignInWithSteam()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(SignInWithSteamSuccess)),
                Items = {{"userId", _executionContextAccessor.UserId.ToString()}}
            };

            var result = Challenge(properties, SteamAuthenticationDefaults.AuthenticationScheme);
            await result.ExecuteResultAsync(ControllerContext);

            return Ok(new SteamSignInResponse
            {
                Location = ControllerContext.HttpContext.Response.Headers["location"].FirstOrDefault()
            });
        }
    }
}