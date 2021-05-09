using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.Infrastructure
{
    internal class MatchesModule : IMatchesModule
    {
        public async Task ExecuteCommandAsync(ICommand command)
        {
            using var scope = MatchesCompositionRoot.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(command);
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            using var scope = MatchesCompositionRoot.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using var scope = MatchesCompositionRoot.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(query);
        }
    }
}