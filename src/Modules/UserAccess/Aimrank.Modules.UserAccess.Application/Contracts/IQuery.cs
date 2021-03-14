using MediatR;

namespace Aimrank.Modules.UserAccess.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}