using FluentValidation;

namespace Aimrank.Modules.CSGO.Application.Commands.CreatePod
{
    public class CreatePodCommandValidator : AbstractValidator<CreatePodCommand>
    {
        public CreatePodCommandValidator()
        {
            RuleFor(x => x.IpAddress).NotEmpty();
            RuleFor(x => x.MaxServers).GreaterThan(0);
        }
    }
}