using Aimrank.Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Aimrank.Web.ProblemDetails
{
    public class ApplicationProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public ApplicationProblemDetails(ApplicationException exception)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8";
            Status = StatusCodes.Status409Conflict;
            Title = exception.Message;
            Extensions.Add("code", exception.Code);
        }
    }
}