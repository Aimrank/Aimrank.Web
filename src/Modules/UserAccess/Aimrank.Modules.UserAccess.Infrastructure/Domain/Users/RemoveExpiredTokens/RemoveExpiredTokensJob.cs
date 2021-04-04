using Aimrank.Modules.UserAccess.Infrastructure.Configuration;
using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Infrastructure.Domain.Users.RemoveExpiredTokens
{
    internal class RemoveExpiredTokensJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new RemoveExpiredTokensCommand());
        }
    }
}