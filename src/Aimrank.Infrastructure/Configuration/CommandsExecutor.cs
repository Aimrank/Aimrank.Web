using Aimrank.Application.Contracts;
using Autofac;
using MediatR;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Configuration
{
    internal static class CommandsExecutor
    {
        internal static async Task Execute(ICommand command)
        {
            await using var scope = AimrankCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }

        internal static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            await using var scope = AimrankCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(command);
        }
    }
}