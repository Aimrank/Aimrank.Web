using FluentValidation;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.DeleteServer
{
    public class DeleteServerCommandValidator : AbstractValidator<DeleteServerCommand>
    {
        public DeleteServerCommandValidator()
        {
            RuleFor(x => x.MatchId).NotEmpty();
        }
    }
}