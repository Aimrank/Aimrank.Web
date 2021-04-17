using FluentValidation;

namespace Aimrank.Web.Modules.Matches.Application.Matches.AcceptMatch
{
    internal class AcceptMatchCommandValidator : AbstractValidator<AcceptMatchCommand>
    {
        public AcceptMatchCommandValidator()
        {
            RuleFor(x => x.MatchId).NotEmpty();
        }
    }
}