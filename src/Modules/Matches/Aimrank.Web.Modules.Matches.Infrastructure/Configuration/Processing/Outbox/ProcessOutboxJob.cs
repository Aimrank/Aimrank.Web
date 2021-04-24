using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox
{
    [DisallowConcurrentExecution]
    internal class ProcessOutboxJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
            => CommandsExecutor.Execute(new ProcessOutboxCommand());
    }
}