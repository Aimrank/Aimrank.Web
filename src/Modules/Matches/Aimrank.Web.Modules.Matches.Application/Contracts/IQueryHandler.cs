using MediatR;

namespace Aimrank.Web.Modules.Matches.Application.Contracts
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}