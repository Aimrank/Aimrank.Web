using Aimrank.Common.Application.Exceptions;

namespace Aimrank.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class EmailNotConfirmedException : ApplicationException
    {
        public override string Code => "email_not_confirmed";
        
        public EmailNotConfirmedException() : base("You must confirm your email address")
        {
        }
    }
}