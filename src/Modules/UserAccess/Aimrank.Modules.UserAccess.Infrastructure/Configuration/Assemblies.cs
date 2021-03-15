using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Domain.Users;
using System.Reflection;

namespace Aimrank.Modules.UserAccess.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(IUserAccessModule).Assembly;
        public static readonly Assembly Domain = typeof(User).Assembly;
        public static readonly Assembly Infrastructure = typeof(Assemblies).Assembly;
    }
}