using System.Collections.Generic;

namespace Aimrank.Web.Modules.Cluster.Application.Entities
{
    public class Pod
    {
        public string IpAddress { get; set; }
        public int MaxServers { get; set; }
        public ICollection<Server> Servers { get; set; }
    }
}