using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using System.Reflection;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IMatchesModule).Assembly;
        public static readonly Assembly Domain = typeof(Match).Assembly;
        public static readonly Assembly Infrastructure = typeof(Assemblies).Assembly;
        public static readonly Assembly IntegrationEvents = typeof(MatchFinishedEvent).Assembly;
    }
}