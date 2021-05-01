using System.Collections.Generic;

namespace Aimrank.Web.App.Configuration.Clients.Cluster
{
    public record ClusterApiError(string Title, Dictionary<string, List<string>> Errors);
}