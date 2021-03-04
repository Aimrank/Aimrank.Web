using FluentValidation;

namespace Aimrank.Web.Contracts.CSGO.Validators
{
    public class ProcessServerEventRequestValidator : AbstractValidator<ProcessServerEventRequest>
    {
        public ProcessServerEventRequestValidator()
        {
            RuleFor(x => x.MatchId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}