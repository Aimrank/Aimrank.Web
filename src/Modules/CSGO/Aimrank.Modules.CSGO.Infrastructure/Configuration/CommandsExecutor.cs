using Aimrank.Modules.CSGO.Application.Contracts;
using Autofac;
using MediatR;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration
{
    internal static class CommandsExecutor
    {
        internal static async Task Execute(ICommand command)
        {
            await using var scope = CSGOCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }
        
        internal static async Task Execute<TResult>(ICommand<TResult> command)
        {
            await using var scope = CSGOCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }
    }
}