using Aimrank.Common.Domain;
using Microsoft.AspNetCore.Http;

namespace Aimrank.Web.ProblemDetails
{
    public class BusinessRuleValidationProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationProblemDetails(BusinessRuleValidationException exception)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8";
            Title = exception.BrokenRule.Message;
            Status = StatusCodes.Status409Conflict;
            Extensions.Add("code", exception.BrokenRule.Code);
        }
    }
}