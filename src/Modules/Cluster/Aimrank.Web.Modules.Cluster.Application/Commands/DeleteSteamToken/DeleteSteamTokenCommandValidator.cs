using FluentValidation;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.DeleteSteamToken
{
    internal class DeleteSteamTokenCommandValidator : AbstractValidator<DeleteSteamTokenCommand>
    {
        public DeleteSteamTokenCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}