using Aimrank.Common.Application.Exceptions;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal class ServerProcessStopException : ApplicationException
    {
        public override string Code => "csgo_server_not_found";
        
        public ServerProcessStopException() : base("Server not found")
        {
        }
    }
}