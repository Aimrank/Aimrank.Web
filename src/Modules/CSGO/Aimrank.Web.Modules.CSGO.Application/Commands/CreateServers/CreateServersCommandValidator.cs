using FluentValidation;

namespace Aimrank.Web.Modules.CSGO.Application.Commands.CreateServers
{
    public class CreateServersCommandValidator : AbstractValidator<CreateServersCommand>
    {
        public CreateServersCommandValidator()
        {
            RuleFor(x => x.MatchIds).NotEmpty();
        }
    }
}