namespace Aimrank.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class AuthenticationResult
    {
        public UserDto User { get; private init; }
        public bool IsAuthenticated { get; private init; }
        public string AuthenticationError { get; private init; }

        public static AuthenticationResult Success(UserDto user)
            => new AuthenticationResult
            {
                IsAuthenticated = true,
                User = user
            };

        public static AuthenticationResult Error(string authenticationError)
            => new AuthenticationResult
            {
                IsAuthenticated = false,
                AuthenticationError = authenticationError
            };
    }
}