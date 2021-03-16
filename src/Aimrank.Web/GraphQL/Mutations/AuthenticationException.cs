using Aimrank.Common.Application.Exceptions;

namespace Aimrank.Web.GraphQL.Mutations
{
    public class AuthenticationException : ApplicationException
    {
        public override string Code => "invalid_credentials";
        
        public AuthenticationException() : base("Invalid credentials")
        {
        }
    }
}