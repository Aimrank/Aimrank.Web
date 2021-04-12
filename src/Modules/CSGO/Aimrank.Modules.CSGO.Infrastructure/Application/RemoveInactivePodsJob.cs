using Aimrank.Modules.CSGO.Application.Commands.RemoveInactivePods;
using Aimrank.Modules.CSGO.Infrastructure.Configuration;
using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Infrastructure.Application
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