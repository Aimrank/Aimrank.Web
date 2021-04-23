using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Autofac;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration
{
    internal static class CommandsExecutor
    {
        internal static async Task Execute(ICommand command)
        {
            await using var scope = ClusterCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            var logger = scope.Resolve<ILogger>();

            try
            {
                await mediator.Send(command);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }
    }
}