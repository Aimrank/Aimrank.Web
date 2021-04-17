namespace Aimrank.Web.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class AuthenticationResult
    {
        public AuthenticatedUserDto User { get; }

        public AuthenticationResult(AuthenticatedUserDto user)
        {
            User = user;
        }
    }
}