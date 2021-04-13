using FluentValidation;

namespace Aimrank.Modules.CSGO.Application.Commands.CreateServers
{
    public class CreateServersCommandValidator : AbstractValidator<CreateServersCommand>
    {
        public CreateServersCommandValidator()
        {
            RuleFor(x => x.MatchIds).NotEmpty();
        }
    }
}