using Aimrank.Web.Common.Application;
using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Application.Players.CreateOrUpdatePlayer;
using Aimrank.Web.App.Contracts;
using Aimrank.Web.App.Steam;
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteamController : Controller
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IMatchesModule _matchesModule;

        public SteamController(
            IExecutionContextAccessor executionContextAccessor,
            IMatchesModule matchesModule)
        {
            _executionContextAccessor = executionContextAccessor;
            _matchesModule = matchesModule;
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

                var command = new CreateOrUpdatePlayerCommand(Guid.Parse(userId), data.Id);
                await _matchesModule.ExecuteCommandAsync(command);
            }
            catch (BusinessRuleValidationException exception)
            {
                TempData["error"] = exception.BrokenRule.Message;
            }
            
            return Redirect("/app/settings");
        }
        
        [Authorize]
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