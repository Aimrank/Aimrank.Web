using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Processing.RemoveProcessedMessages
{
    [DisallowConcurrentExecution]
    internal class RemoveProcessedMessagesJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
            => CommandsExecutor.Execute(new RemoveProcessedMessagesCommand());
    }
}