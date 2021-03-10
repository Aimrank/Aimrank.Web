using FluentValidation;

namespace Aimrank.Application.Commands.Matches.AcceptMatch
{
    internal class AcceptMatchCommandValidator : AbstractValidator<AcceptMatchCommand>
    {
        public AcceptMatchCommandValidator()
        {
            RuleFor(x => x.MatchId).NotEmpty();
        }
    }
}