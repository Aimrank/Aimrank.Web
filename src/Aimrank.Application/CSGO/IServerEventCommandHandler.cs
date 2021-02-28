using Aimrank.Application.Contracts;

namespace Aimrank.Application.CSGO
{
    internal interface IServerEventCommandHandler<in TCommand> : ICommandHandler<TCommand>
        where TCommand : class, IServerEventCommand
    {
    }
}