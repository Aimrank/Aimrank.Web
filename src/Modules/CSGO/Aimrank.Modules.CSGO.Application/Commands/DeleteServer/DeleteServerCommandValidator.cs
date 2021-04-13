using FluentValidation;

namespace Aimrank.Modules.CSGO.Application.Commands.DeleteServer
{
    public class DeleteServerCommandValidator : AbstractValidator<DeleteServerCommand>
    {
        public DeleteServerCommandValidator()
        {
            RuleFor(x => x.MatchId).NotEmpty();
        }
    }
}