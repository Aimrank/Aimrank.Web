using FluentValidation;

namespace Aimrank.Modules.CSGO.Application.Commands.StartServer
{
    public class StartServerCommandValidator : AbstractValidator<StartServerCommand>
    {
        public StartServerCommandValidator()
        {
            RuleFor(x => x.Map).NotEmpty();
            RuleFor(x => x.MatchId).NotEmpty();
            RuleFor(x => x.Whitelist).NotEmpty();
        }
    }
}