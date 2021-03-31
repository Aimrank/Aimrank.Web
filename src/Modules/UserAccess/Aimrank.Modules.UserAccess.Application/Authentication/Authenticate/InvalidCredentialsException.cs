using Aimrank.Common.Application.Exceptions;

namespace Aimrank.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class InvalidCredentialsException : ApplicationException
    {
        public override string Code => "invalid_credentials";
        
        public InvalidCredentialsException() : base("Invalid credentials")
        {
        }
    }
}