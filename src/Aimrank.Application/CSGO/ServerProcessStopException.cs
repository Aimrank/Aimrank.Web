using Aimrank.Common.Application;

namespace Aimrank.Application.CSGO
{
    public class ServerProcessStopException : ApplicationException
    {
        public override string Code => "csgo_server_not_found";
        
        public ServerProcessStopException() : base("Server not found.")
        {
        }
    }
}