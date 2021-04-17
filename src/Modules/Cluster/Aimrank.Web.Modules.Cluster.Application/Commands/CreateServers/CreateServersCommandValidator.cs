using FluentValidation;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.CreateServers
{
    public class CreateServersCommandValidator : AbstractValidator<CreateServersCommand>
    {
        public CreateServersCommandValidator()
        {
            RuleFor(x => x.MatchIds).NotEmpty();
        }
    }
}