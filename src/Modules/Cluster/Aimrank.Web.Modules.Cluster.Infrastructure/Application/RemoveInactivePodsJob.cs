using Aimrank.Web.Modules.Cluster.Application.Commands.RemoveInactivePods;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration;
using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Application
{
    [DisallowConcurrentExecution]
    internal class RemoveInactivePodsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
            => CommandsExecutor.Execute(new RemoveInactivePodsCommand());
    }
}