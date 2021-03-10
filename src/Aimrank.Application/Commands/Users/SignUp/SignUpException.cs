using Aimrank.Common.Application.Exceptions;
using System.Collections.Generic;

namespace Aimrank.Application.Commands.Users.SignUp
{
    public class SignUpException : ApplicationException
    {
        public override string Code => "failed_to_sign_up";

        public Dictionary<string, List<string>> Errors { get; }
        
        public SignUpException(Dictionary<string, List<string>> errors = null)
            : base("Failed to sign up")
        {
            Errors = errors ?? new Dictionary<string, List<string>>();
        }
    }
}