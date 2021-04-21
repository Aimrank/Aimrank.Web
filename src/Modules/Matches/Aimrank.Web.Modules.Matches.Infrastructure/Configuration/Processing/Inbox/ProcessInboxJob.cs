using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox
{
    [DisallowConcurrentExecution]
    internal class ProcessInboxJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessInboxCommand());
        }
    }
}