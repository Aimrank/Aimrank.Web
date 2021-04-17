using FluentValidation;

namespace Aimrank.Modules.CSGO.Application.Commands.DeleteAndStopServer
{
    public class DeleteAndStopServerCommandValidator : AbstractValidator<DeleteAndStopServerCommand>
    {
        public DeleteAndStopServerCommandValidator()
        {
            RuleFor(x => x.MatchId).NotEmpty();
        }
    }
}