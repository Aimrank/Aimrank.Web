using System;

namespace Aimrank.Web.App.Configuration.Clients.Cluster
{
    public class ClusterApiException : Exception
    {
        public ClusterApiException(string message) : base(message)
        {
        }
    }
}