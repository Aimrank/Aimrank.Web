using Aimrank.Common.Application.Exceptions;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal class ServerReservationException : ApplicationException
    {
        public override string Code => "csgo_server_reservation_failed";
        
        public ServerReservationException() : base("Could not create server reservation")
        {
        }
    }
}