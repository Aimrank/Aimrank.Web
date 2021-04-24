using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration;
using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Domain.Users.RemoveExpiredTokens
{
    internal class RemoveExpiredTokensJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
            => CommandsExecutor.Execute(new RemoveExpiredTokensCommand());
    }
}