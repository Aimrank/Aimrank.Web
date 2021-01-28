using Aimrank.Application.Commands.RefreshJwt;
using Aimrank.Common.Application;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Application.Commands.SignUp
{
    public class SignUpException : ApplicationException
    {
        public override string Code => "failed_to_sign_up";
        public IEnumerable<AuthenticationErrorDto> Fields { get; }
        public SignUpException(IEnumerable<AuthenticationErrorDto> errors = null)
            : base("Failed to sign up")
        {
            Fields = errors ?? Enumerable.Empty<AuthenticationErrorDto>();
        }
    }
}