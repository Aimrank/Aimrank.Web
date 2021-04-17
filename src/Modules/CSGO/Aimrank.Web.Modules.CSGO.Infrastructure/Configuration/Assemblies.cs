using Aimrank.Web.Modules.CSGO.Application.Contracts;
using System.Reflection;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(ICSGOModule).Assembly;
        public static readonly Assembly Infrastructure = typeof(Assemblies).Assembly;
    }
}