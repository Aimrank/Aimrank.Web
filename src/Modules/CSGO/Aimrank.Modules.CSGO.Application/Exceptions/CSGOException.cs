using System;

namespace Aimrank.Modules.CSGO.Application.Exceptions
{
    public class CSGOException : Exception
    {
        public CSGOException(string message) : base(message)
        {
        }
    }
}