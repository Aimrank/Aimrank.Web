using Aimrank.Application.Commands.SignIn;
using Microsoft.AspNetCore.Http;

namespace Aimrank.Web.ProblemDetails
{
    public class SignInProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public SignInProblemDetails(SignInException exception)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8";
            Title = exception.Message;
            Status = StatusCodes.Status409Conflict;
            Extensions.Add("code", exception.Code);
        }
    }
}