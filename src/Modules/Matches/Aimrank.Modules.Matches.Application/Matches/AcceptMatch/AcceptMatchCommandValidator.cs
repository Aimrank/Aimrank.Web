using FluentValidation;

namespace Aimrank.Modules.Matches.Application.Matches.AcceptMatch
{
    internal class AcceptMatchCommandValidator : AbstractValidator<AcceptMatchCommand>
    {
        public AcceptMatchCommandValidator()
        {
            RuleFor(x => x.MatchId).NotEmpty();
        }
    }
}