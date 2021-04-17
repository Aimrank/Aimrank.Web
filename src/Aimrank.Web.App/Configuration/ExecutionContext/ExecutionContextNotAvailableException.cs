using Aimrank.Web.Common.Application.Exceptions;

namespace Aimrank.Web.App.Configuration.ExecutionContext
{
    public class ExecutionContextNotAvailableException : ApplicationException
    {
        public override string Code => "unauthorized";
        
        public ExecutionContextNotAvailableException() : base("Unauthorized")
        {
        }
    }
}