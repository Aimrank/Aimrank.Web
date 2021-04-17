using Aimrank.Web.Common.Application.Exceptions;

namespace Aimrank.Web.App.GraphQL.Mutations.Users
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