using Aimrank.Modules.UserAccess.Application.Authentication.Authenticate;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Users.RegisterNewUser;
using Aimrank.Web.Configuration.SessionAuthentication;
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
            var result = await SignInAsync(input, httpContextAccessor);
            return new SignInPayload(result);
        }

        public async Task<SignUpPayload> SignUp(
            [GraphQLNonNullType] RegisterNewUserCommand input,
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            await _userAccessModule.ExecuteCommandAsync(input);

            var result = await SignInAsync(
                new AuthenticateCommand(input.Username, input.Password),
                httpContextAccessor);
            
            return new SignUpPayload(result);
        }
        
        private async Task<AuthenticationSuccessRecord> SignInAsync(
            AuthenticateCommand command,
            IHttpContextAccessor httpContextAccessor)
        {
            var result = await _userAccessModule.ExecuteCommandAsync(command);

            if (!result.IsAuthenticated || httpContextAccessor.HttpContext is null)
            {
                throw new AuthenticationException(result.AuthenticationError);
            }

            var identity = new ClaimsIdentity(result.User.Claims, SessionAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContextAccessor.HttpContext.SignInAsync(principal);

            return new AuthenticationSuccessRecord(
                result.User.Id,
                result.User.Username,
                result.User.Email);
        }

        public async Task<SignOutPayload> SignOut([Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is not null)
            {
                await httpContextAccessor.HttpContext.SignOutAsync();
            }
            
            return new SignOutPayload();
        }
    }
}