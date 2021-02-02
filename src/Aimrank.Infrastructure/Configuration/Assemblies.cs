using Aimrank.Application.Contracts;
using Aimrank.Domain.Matches;
using System.Reflection;

namespace Aimrank.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IAimrankModule).Assembly;
        public static readonly Assembly Domain = typeof(Match).Assembly;
        public static readonly Assembly Infrastructure = typeof(Assemblies).Assembly;
    }
}