using Aimrank.Common.Application.Exceptions;
using System.Collections.Generic;

namespace Aimrank.Application.Commands.Users.SignUp
{
    public class SignUpException : ApplicationException
    {
        public override string Code => "failed_to_sign_up";

        public Dictionary<string, List<string>> Errors { get; init; } = new();
        
        public SignUpException() : base("Failed to sign up")
        {
        }
    }
}