using MediatR;

namespace Aimrank.Web.Modules.Matches.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}