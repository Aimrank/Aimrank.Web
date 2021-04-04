using Aimrank.Modules.UserAccess.Application.Contracts;
using Autofac;
using MediatR;
using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Infrastructure.Configuration
{
    internal static class CommandsExecutor
    {
        internal static async Task Execute(ICommand command)
        {
            await using var scope = UserAccessCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }
        
        internal static async Task Execute<TResult>(ICommand<TResult> command)
        {
            await using var scope = UserAccessCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }
    }
}