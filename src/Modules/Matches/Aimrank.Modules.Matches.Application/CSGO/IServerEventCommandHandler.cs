using Aimrank.Modules.Matches.Application.Contracts;

namespace Aimrank.Modules.Matches.Application.CSGO
{
    internal interface IServerEventCommandHandler<in TCommand> : ICommandHandler<TCommand>
        where TCommand : class, IServerEventCommand
    {
    }
}