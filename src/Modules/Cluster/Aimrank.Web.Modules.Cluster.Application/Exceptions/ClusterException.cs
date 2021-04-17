using System;

namespace Aimrank.Web.Modules.Cluster.Application.Exceptions
{
    public class ClusterException : Exception
    {
        public ClusterException(string message) : base(message)
        {
        }
    }
}