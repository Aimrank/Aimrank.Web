using System.Threading;
using System.Threading.Tasks;
using Aimrank.Web.Common.Infrastructure;
using MediatR;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Mediator
{
    internal class UnitOfWorkPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkPipelineBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            await _unitOfWork.CommitAsync(cancellationToken);

            return response;
        }
    }
}