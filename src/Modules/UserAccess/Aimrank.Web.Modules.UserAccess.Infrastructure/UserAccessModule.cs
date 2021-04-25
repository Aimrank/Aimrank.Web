using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration;
using Autofac;
using MediatR;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure
{
    internal class UserAccessModule : IUserAccessModule
    {
        public async Task ExecuteCommandAsync(ICommand command)
        {
            await using var scope = UserAccessCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            await using var scope = UserAccessCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            await using var scope = UserAccessCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(query);
        }
    }
}