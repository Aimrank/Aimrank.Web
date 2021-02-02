using Aimrank.Application.Commands.ProcessLobbies;
using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Configuration.Processing
{
    [DisallowConcurrentExecution]
    internal class ProcessLobbiesJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessLobbiesCommand());
        }
    }
}