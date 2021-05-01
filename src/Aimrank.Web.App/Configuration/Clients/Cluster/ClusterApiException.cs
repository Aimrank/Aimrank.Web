using System.Collections.Generic;
using System;

namespace Aimrank.Web.App.Configuration.Clients.Cluster
{
    public class ClusterApiException : Exception
    {
        public string Code => "cluster_error";
        
        public Dictionary<string, List<string>> Errors { get; }
        
        public ClusterApiException(ClusterApiError error) : base(error.Title)
        {
            Errors = error.Errors;
        }
    }
}