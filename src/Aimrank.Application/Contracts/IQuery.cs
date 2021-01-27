using MediatR;

namespace Aimrank.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}