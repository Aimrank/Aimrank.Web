using MediatR;

namespace Aimrank.Modules.Matches.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}