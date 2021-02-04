using FluentValidation;

namespace Aimrank.Web.Contracts.Requests.Validators
{
    public class ProcessServerEventRequestValidator : AbstractValidator<ProcessServerEventRequest>
    {
        public ProcessServerEventRequestValidator()
        {
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}