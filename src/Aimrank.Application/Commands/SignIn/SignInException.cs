using Aimrank.Common.Application;

namespace Aimrank.Application.Commands.SignIn
{
    public class SignInException : ApplicationException
    {
        public override string Code => "invalid_credentials";
        
        public SignInException() : base("Invalid credentials")
        {
        }
    }
}