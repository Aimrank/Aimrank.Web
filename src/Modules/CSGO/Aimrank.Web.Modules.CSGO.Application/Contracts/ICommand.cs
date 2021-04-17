using MediatR;

namespace Aimrank.Web.Modules.CSGO.Application.Contracts
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}