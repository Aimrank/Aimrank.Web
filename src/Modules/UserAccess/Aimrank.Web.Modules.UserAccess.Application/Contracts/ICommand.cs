using MediatR;

namespace Aimrank.Web.Modules.UserAccess.Application.Contracts
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}