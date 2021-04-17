using System;

namespace Aimrank.Web.Modules.CSGO.Application.Exceptions
{
    public class CSGOException : Exception
    {
        public CSGOException(string message) : base(message)
        {
        }
    }
}