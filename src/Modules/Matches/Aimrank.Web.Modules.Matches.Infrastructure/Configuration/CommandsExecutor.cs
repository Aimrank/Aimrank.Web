using Aimrank.Web.Modules.Matches.Application.Contracts;
using Autofac;
using MediatR;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration
{
    internal static class CommandsExecutor
    {
        internal static async Task Execute(ICommand command)
        {
            await using var scope = MatchesCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }
    }
}