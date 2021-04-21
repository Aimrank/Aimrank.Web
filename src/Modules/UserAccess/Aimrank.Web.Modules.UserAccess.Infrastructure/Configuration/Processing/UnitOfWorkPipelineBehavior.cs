using Aimrank.Web.Common.Infrastructure;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly DomainEventDispatcher _domainEventDispatcher;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkPipelineBehavior(DomainEventDispatcher domainEventDispatcher, IUnitOfWork unitOfWork)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            await _domainEventDispatcher.DispatchAsync();
            await _unitOfWork.CommitAsync(cancellationToken);

            return response;
        }
    }
}