using Aimrank.Application.Commands.ProcessLobbies;
using MediatR;
using Quartz;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Configuration.Processing
{
    [DisallowConcurrentExecution]
    internal class ProcessLobbiesJob : IJob
    {
        private readonly IMediator _mediator;

        public ProcessLobbiesJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new ProcessLobbiesCommand());
        }
    }
}