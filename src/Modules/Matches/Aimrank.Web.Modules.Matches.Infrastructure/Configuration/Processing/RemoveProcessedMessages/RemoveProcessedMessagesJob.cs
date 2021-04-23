using System.Threading.Tasks;
using Quartz;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.RemoveProcessedMessages
{
    [DisallowConcurrentExecution]
    internal class RemoveProcessedMessagesJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
            => CommandsExecutor.Execute(new RemoveProcessedMessagesCommand());
    }
}