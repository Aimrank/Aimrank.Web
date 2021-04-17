using FluentValidation;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.DeleteAndStopServer
{
    public class DeleteAndStopServerCommandValidator : AbstractValidator<DeleteAndStopServerCommand>
    {
        public DeleteAndStopServerCommandValidator()
        {
            RuleFor(x => x.MatchId).NotEmpty();
        }
    }
}