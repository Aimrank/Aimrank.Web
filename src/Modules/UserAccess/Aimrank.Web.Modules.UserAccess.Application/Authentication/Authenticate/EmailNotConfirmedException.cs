using Aimrank.Web.Common.Application.Exceptions;

namespace Aimrank.Web.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class EmailNotConfirmedException : ApplicationException
    {
        public override string Code => "email_not_confirmed";
        
        public EmailNotConfirmedException() : base("You must confirm your email address")
        {
        }
    }
}