using Aimrank.Application.Exceptions;

namespace Aimrank.Web.Configuration.ExecutionContext
{
    public class ExecutionContextNotAvailableException : ApplicationException
    {
        public override string Code => "unauthorized";
        
        public ExecutionContextNotAvailableException() : base("Unauthorized")
        {
        }
    }
}