using Aimrank.Web.Modules.Cluster.Application.Commands.RemoveInactivePods;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration;
using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Application
{
    [DisallowConcurrentExecution]
    internal class RemoveInactivePodsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new RemoveInactivePodsCommand());
        }
    }
}