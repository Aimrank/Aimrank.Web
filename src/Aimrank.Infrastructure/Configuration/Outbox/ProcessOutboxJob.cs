using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Configuration.Outbox
{
    [DisallowConcurrentExecution]
    internal class ProcessOutboxJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessOutboxCommand());
        }
    }
}