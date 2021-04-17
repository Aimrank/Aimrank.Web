using Aimrank.Web.Modules.Matches.Application.Lobbies.ProcessLobbies;
using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing
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