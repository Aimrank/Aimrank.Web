using Aimrank.Common.Application.Exceptions;

namespace Aimrank.Web.GraphQL.Mutations.Users
{
    public class AuthenticationException : ApplicationException
    {
        public override string Code => "invalid_credentials";
        
        public AuthenticationException(string message)
            : base(string.IsNullOrEmpty(message) ? "Invalid credentials" : message)
        {
        }
    }
}