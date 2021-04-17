using MediatR;

namespace Aimrank.Web.Modules.CSGO.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}