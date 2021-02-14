using Aimrank.Common.Application.Exceptions;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal class ServerProcessStartException : ApplicationException
    {
        public override string Code => "csgo_server_start_failed";
        
        public ServerProcessStartException() : base("Could not start CS:GO server")
        {
        }
    }
}