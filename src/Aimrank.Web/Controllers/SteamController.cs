using Aimrank.Application.Commands.Users.UpdateUserSteamDetails;
using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Common.Domain;
using Aimrank.Web.Steam;
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Aimrank.Web.Contracts;

namespace Aimrank.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteamController : Controller
    {
        private readonly IAimrankModule _aimrankModule;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public SteamController(
            IAimrankModule aimrankModule,
            IExecutionContextAccessor executionContextAccessor)
        {
            _aimrankModule = aimrankModule;
            _executionContextAccessor = executionContextAccessor;
        }

        [Authorize(AuthenticationSchemes = SteamAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet("openid")]
        public async Task<IActionResult> SignInWithSteamSuccess()
        {
            var result = await HttpContext.AuthenticateAsync(SteamAuthenticationDefaults.AuthenticationScheme);

            var userId = result.Properties?.Items.FirstOrDefault(x => x.Key == "userId").Value;
            if (userId is null)
            {
                return BadRequest();
            }

            try
            {
                var data = HttpContext.GetSteamData();

                var command = new UpdateUserSteamDetailsCommand(Guid.Parse(userId), data.Id);
                await _aimrankModule.ExecuteCommandAsync(command);
            }
            catch (BusinessRuleValidationException exception)
            {
                TempData["error"] = exception.BrokenRule.Message;
            }
            
            return Redirect("/app/settings");
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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