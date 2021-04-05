using Aimrank.Modules.UserAccess.Application.Authentication.Authenticate;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Users.ChangePassword;
using Aimrank.Modules.UserAccess.Application.Users.RegisterNewUser;
using Aimrank.Modules.UserAccess.Application.Users.RequestEmailConfirmation;
using Aimrank.Modules.UserAccess.Application.Users.RequestPasswordReminder;
using Aimrank.Modules.UserAccess.Application.Users.ResetPassword;
using Aimrank.Web.Configuration.SessionAuthentication;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using HotChocolate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aimrank.Web.GraphQL.Mutations.Users
{
    [ExtendObjectType("Mutation")]
    public class UserMutations
    {
        private readonly IUserAccessModule _userAccessModule;

        public UserMutations(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        public async Task<SignInPayload> SignIn(
            [GraphQLNonNullType] AuthenticateCommand input,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                throw new InvalidCredentialsException();
            }
            
            var result = await _userAccessModule.ExecuteCommandAsync(input);

            var identity = new ClaimsIdentity(result.User.Claims, SessionAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContextAccessor.HttpContext.SignInAsync(principal);

            return new SignInPayload(new AuthenticationSuccessRecord(
                result.User.Id,
                result.User.Username,
                result.User.Email));
        }

        public async Task<SignUpPayload> SignUp([GraphQLNonNullType] RegisterNewUserCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new SignUpPayload();
        }

        public async Task<SignOutPayload> SignOut([Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is not null)
            {
                await httpContextAccessor.HttpContext.SignOutAsync();
            }
            
            return new SignOutPayload();
        }
        
        public async Task<RequestEmailConfirmationPayload> RequestEmailConfirmation(
            [GraphQLNonNullType] RequestEmailConfirmationCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new RequestEmailConfirmationPayload();
        }

        public async Task<RequestPasswordReminderPayload> RequestPasswordReminder(
            [GraphQLNonNullType] RequestPasswordReminderCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new RequestPasswordReminderPayload();
        }

        [Authorize]
        public async Task<ChangePasswordPayload> ChangePassword([GraphQLNonNullType] ChangePasswordCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new ChangePasswordPayload();
        }

        public async Task<ResetPasswordPayload> ResetPassword([GraphQLNonNullType] ResetPasswordCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new ResetPasswordPayload();
        }
    }
}