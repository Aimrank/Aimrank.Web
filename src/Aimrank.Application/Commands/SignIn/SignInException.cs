using Aimrank.Application.Commands.RefreshJwt;
using Aimrank.Common.Application;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Application.Commands.SignIn
{
    public class SignInException : ApplicationException
    {
        public override string Code => "invalid_credentials";
        public IEnumerable<AuthenticationErrorDto> Fields { get; }
        
        public SignInException(IEnumerable<AuthenticationErrorDto> errors = null)
            : base("Invalid credentials")
        {
            Fields = errors ?? Enumerable.Empty<AuthenticationErrorDto>();
        }
    }
}