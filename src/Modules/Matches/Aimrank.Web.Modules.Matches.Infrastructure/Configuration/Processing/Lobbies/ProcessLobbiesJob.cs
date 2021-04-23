using Aimrank.Web.Modules.Matches.Application.Lobbies.ProcessLobbies;
using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Lobbies
{
    [DisallowConcurrentExecution]
    internal class ProcessLobbiesJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
            => CommandsExecutor.Execute(new ProcessLobbiesCommand());
    }
}