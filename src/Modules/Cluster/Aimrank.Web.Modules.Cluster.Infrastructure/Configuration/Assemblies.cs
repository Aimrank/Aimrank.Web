using Aimrank.Web.Modules.Cluster.Application.Contracts;
using System.Reflection;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IClusterModule).Assembly;
        public static readonly Assembly Infrastructure = typeof(Assemblies).Assembly;
    }
}