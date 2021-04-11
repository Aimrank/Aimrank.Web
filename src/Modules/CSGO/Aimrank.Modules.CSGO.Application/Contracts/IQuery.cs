using MediatR;

namespace Aimrank.Modules.CSGO.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}