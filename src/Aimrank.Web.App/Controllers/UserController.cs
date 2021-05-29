using Aimrank.Web.App.Contracts;
using Aimrank.Web.Common.Application.Exceptions;
using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.UserAccess.Application.Authentication.AuthenticateUser;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Users.ConfirmEmailAddress;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aimrank.Web.App.Controllers
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
                
                var identity = new ClaimsIdentity(result.User.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                
                await HttpContext.SignInAsync(principal, new AuthenticationProperties{IsPersistent = true});
                
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