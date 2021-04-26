using FluentValidation;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.AddSteamToken
{
    internal class AddSteamTokenCommandValidator : AbstractValidator<AddSteamTokenCommand>
    {
        public AddSteamTokenCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty().MaximumLength(255);
        }
    }
}