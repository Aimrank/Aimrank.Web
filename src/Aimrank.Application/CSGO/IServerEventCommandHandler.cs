using Aimrank.Application.Contracts;

namespace Aimrank.Application.CSGO
{
    public interface IServerEventCommandHandler<in TCommand> : ICommandHandler<TCommand>
        where TCommand : class, IServerEventCommand
    {
    }
}