using Aimrank.Application.Commands.Users.SignUp;
using Microsoft.AspNetCore.Http;

namespace Aimrank.Web.ProblemDetails
{
    public class SignUpProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public SignUpProblemDetails(SignUpException exception)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8";
            Title = exception.Message;
            Status = StatusCodes.Status409Conflict;
            Extensions.Add("code", exception.Code);
            Extensions.Add("errors", exception.Errors);
        }
    }
}