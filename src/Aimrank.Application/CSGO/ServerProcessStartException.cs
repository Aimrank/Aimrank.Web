using Aimrank.Common.Application;

namespace Aimrank.Application.CSGO
{
    public class ServerProcessStartException : ApplicationException
    {
        public override string Code => "csgo_server_start_failed";
        
        public ServerProcessStartException() : base("Could not start CS:GO server")
        {
        }
    }
}