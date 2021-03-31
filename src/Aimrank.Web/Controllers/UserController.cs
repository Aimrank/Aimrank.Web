using Aimrank.Common.Application.Exceptions;
using Aimrank.Common.Domain;
using Aimrank.Modules.UserAccess.Application.Authentication.AuthenticateUser;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Users.ConfirmEmailAddress;
using Aimrank.Web.Configuration.SessionAuthentication;
using Aimrank.Web.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aimrank.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserAccessModule _userAccessModule;

        public UserController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        [HttpGet("verification")]
        public async Task<IActionResult> ConfirmEmailAddress([FromQuery] ConfirmEmailAddressRequest request)
        {
            try
            {
                await _userAccessModule.ExecuteCommandAsync(new ConfirmEmailAddressCommand(request.UserId, request.Token));
                
                var result = await _userAccessModule.ExecuteCommandAsync(new AuthenticateUserCommand(request.UserId));
                
                var identity = new ClaimsIdentity(result.User.Claims, SessionAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                
                await HttpContext.SignInAsync(principal);
                
                return Redirect("/app");
            }
            catch (BusinessRuleValidationException exception)
            { 
                TempData["error"] = exception.BrokenRule.Message;
            }
            catch (ApplicationException exception)
            {
                TempData["error"] = exception.Message;
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}