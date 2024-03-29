using Aimrank.Web.Common.Application.Exceptions;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationErrors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error is not null)
                .ToList();

            if (validationErrors.Count == 0)
            {
                return await next();
            }

            var errors = new Dictionary<string, List<string>>();

            foreach (var error in validationErrors)
            {
                if (!errors.ContainsKey(error.PropertyName))
                {
                    errors[error.PropertyName] = new List<string>();
                }
                
                errors[error.PropertyName].Add(error.ErrorMessage);
            }
            
            throw new InvalidCommandException(errors);
        }
    }
}